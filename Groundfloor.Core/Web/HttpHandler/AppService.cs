using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using System.Diagnostics;
using Groundfloor.Web.Config;

namespace Groundfloor.Web.HttpHandler
{
    public class AppServiceMethod
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Action<HttpContext> Action { get; set; }
    }
 
    public class AppService: IHttpHandler
    {
        protected static Dictionary<string, AppServiceMethod> commands;
        protected static AppServicesSection serviceConfig;

        protected string CRLF = "\n";
        protected bool isHTML = false;
        protected string format = "html";

        protected virtual void Init(HttpContext currentContext)
        {
            serviceConfig = AppServicesSettings.AppServicesConfiguration;

            commands = new Dictionary<string, AppServiceMethod>();
            AddMethodIfAllowed("list", ListCommands);
            AddMethodIfAllowed("recycle", RecycleApp);
            AddMethodIfAllowed("clear-cache", ClearCache);
            AddMethodIfAllowed("list-cache", ListCacheKeys);
            AddMethodIfAllowed("list-config", ListConfiguration);
            AddMethodIfAllowed("list-assemblies", ListAssemblies);
            AddMethodIfAllowed("list-events", ListEvents);
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (commands == null)
                Init(context);

            var status = ValidateRequest(context);
            string method = context.Request.Params["method"].Default("list").ToLower();

            switch (status)
            {
                case RequestStatus.Valid:
                    try {
                        format = context.Request.Params["format"].Default("html").ToLower();
                        context.Response.ContentType = "text/" + format;
                        if (format == "html")
                        {
                            isHTML = true;
                            CRLF = "<br>";
                            string url = "~/".ToAbsoluteUrl();
                            if (context.Request.UrlReferrer != null)
                            {
                                url = context.Request.UrlReferrer.AbsoluteUri;
                            }
                            context.Response.Write("&#171; <a href='{0}'>back</a><hr>", url);
                        }
                        commands[method].Action(context);
                        context.Response.StatusCode = 200;
                    }
                    catch{
                        context.Response.StatusCode = 500;
                    }
                    break;
                default:
                    context.Response.StatusCode = 400; 
                    context.Response.Write(status.ToString());
                    break;
            }
            context.Response.End();
        }

        protected void AddMethodIfAllowed(string method, Action<HttpContext> action, string description=null)
        {
            AddMethodIfAllowed(new AppServiceMethod { Name = method, Action = action, Description = description });
        }
        protected void AddMethodIfAllowed(AppServiceMethod method)
        {
            if (MethodIsAllowed(method.Name))
                commands.Add(method.Name, method);
        }

        private bool MethodIsAllowed(string method)
        {
            var elem = serviceConfig.CommandElements[method];
            return (elem != null && elem.enabled);
        }

        private RequestStatus ValidateRequest(HttpContext currentContext)
        {
            string securityToken = currentContext.Request.Params["token"];
            if (securityToken.isEmpty())
                return RequestStatus.Unauthenticated;

            RequestStatus stat = TokenIsValid(securityToken);
            if (stat != RequestStatus.Valid)
                return stat;

            string method = currentContext.Request.Params["method"].Default("list").ToLower();

            if (!commands.ContainsKey(method))
                return RequestStatus.MethodUnknown;

            return RequestStatus.Valid;
        }

        public RequestStatus TokenIsValid(string securityToken)
        {
            string token = "";
                
            try
            {
                token = securityToken.Decrypt64(serviceConfig.secret);
            }
            catch
            {
                return RequestStatus.BadRequest;
            }

            DateTime dtUTC;
            if (DateTime.TryParse(token, out dtUTC))
            {
                if (dtUTC.AddSeconds(serviceConfig.requestTTLSeconds) < DateTime.UtcNow)
                {
                    return RequestStatus.Expired;
                }
                else
                {
                    return RequestStatus.Valid;
                }
            }
            return RequestStatus.BadRequest;
        }

        #region Action<T> methods
		protected virtual void ListCommands(HttpContext currentContext)
        {
            var format = currentContext.Response.ContentType.Substring(5);
            foreach (string key in commands.Keys)
            {
                var item = commands[key];

                if (isHTML)
                {
                    if (item.Name == "list") //don't hyperlink the list command (this is the page you're on)
                    {
                        currentContext.Response.Write("list<br/>");
                    }
                    else
                    {
                        string url = currentContext.Request.Url.AbsoluteUri;
                        if (url.Contains("method="))
                        {
                            url = url.Replace("method=list", "method=" + item.Name);
                        }
                        else
                        {
                            url += "&method=" + item.Name;
                        }
                        currentContext.Response.Write(string.Format("<a href='{0}'>{1}</a>{2}<br>", url, item.Name, item.Description.FormatIfNotEmpty("<br/>\t{0}")));
                    }
                }
                else
                {
                    currentContext.Response.Write(string.Format("{0}{1}\n", item.Name, item.Description.FormatIfNotEmpty("\n\t{0}")));
                }
            }

            var expy = currentContext.Request.Params["token"].Decrypt64(serviceConfig.secret);
            currentContext.Response.Write(CRLF + CRLF + "token expires " + expy);
            currentContext.Response.StatusCode = 200;
        }

        protected virtual void ClearCache(HttpContext currentContext)
        {
            string cacheKey = currentContext.Request.Params["cache-key"];
            var response = currentContext.Response;

            if (cacheKey.isNotEmpty())
            {
                currentContext.Cache.Remove(cacheKey);
                response.Write(string.Format("[{0}] removed from cache {1}", cacheKey, DateTime.Now));
            }
            else
            {
                //else clear all
                List<string> keys = new List<string>();

                // retrieve application Cache enumerator    
                IDictionaryEnumerator enumerator = currentContext.Cache.GetEnumerator();

                // copy all keys that currently exist in Cache    
                while (enumerator.MoveNext())
                {
                    keys.Add(enumerator.Key.ToString());
                }

                // delete every key from cache    
                for (int i = 0; i < keys.Count; i++)
                {
                    currentContext.Cache.Remove(keys[i]);
                }
                response.Write(string.Format("Cache cleared {0}", DateTime.Now));
            }
            response.StatusCode = 200;
        }

        protected virtual void ListCacheKeys(HttpContext currentContext)
        {
            IDictionaryEnumerator enumerator = currentContext.Cache.GetEnumerator();

            // copy all keys that currently exist in Cache    
            while (enumerator.MoveNext())
            {
                string key = enumerator.Key.ToString();
                currentContext.Response.Write(string.Format("{0} [{1}]" + CRLF, key, currentContext.Cache[key]));
            }
            currentContext.Response.StatusCode = 200;
        }

        protected virtual void RecycleApp(HttpContext currentContext)
        {
            //how about adding app_offline.htm?
            string configFile = currentContext.Request.PhysicalApplicationPath + @"\web.config";
            File.SetLastWriteTimeUtc(configFile, DateTime.UtcNow);

            currentContext.Response.StatusCode = 200;
            currentContext.Response.Write("Application Recycled " + DateTime.Now.ToString() );
        }

        protected virtual void ListConfiguration(HttpContext currentContext)
        {
            currentContext.Response.ContentType = "text/html";
            string configFile = currentContext.Request.PhysicalApplicationPath + @"\web.config";
            string output = File.ReadAllText(configFile).AsHTML();

            if (isHTML)
                currentContext.Response.Write("<pre>");
            currentContext.Response.Write(CRLF + output + CRLF);
            if (isHTML)
                currentContext.Response.Write("</pre>");

            currentContext.Response.StatusCode = 200;
        }

        protected virtual void ListAssemblies(HttpContext currentContext)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in loadedAssemblies.OrderBy(a => a.FullName).ToList())
            {
                currentContext.Response.Write(assembly.ToString() + CRLF);
            }
            currentContext.Response.StatusCode = 200;
        }

        protected virtual void ListEvents(HttpContext currentContext)
        {
            EventLog log = new EventLog("application");
            var entries = log.Entries;
            foreach (EventLogEntry entry in entries)
            {
                if (entry.EntryType == EventLogEntryType.Error | entry.EntryType == EventLogEntryType.Warning)
                    currentContext.Response.Write(entry.Message + CRLF);
            }
            currentContext.Response.StatusCode = 200;
        }
	    #endregion    
    }

    public enum RequestStatus
    {
        Valid,
        Expired,
        Unauthenticated,
        MethodUnknown,
        NotAllowed,
        Denied,
        BadRequest
    }
}
