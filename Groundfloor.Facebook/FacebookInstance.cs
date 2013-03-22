using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Specialized;
using Groundfloor.Facebook.Config;
using System.Web.Script.Serialization;

namespace Groundfloor.Facebook
{
    public class FacebookInstance
    {
        public bool Connected { get; internal set; }

        public string algorithm { get; internal set; }
        public string app_data { get; set; }
        public string issued_at { get; internal set; }
        public string expires { get; internal set; }
        public string oauth_token { get; internal set; }
        public Page page { get; internal set; }
        public User user { get; internal set; }
        public string AppId { get; internal set; }
        public string AppName { get; internal set; }
        public string PageId { get; internal set; }
        public string PageName { get; internal set; }
        public string PageUrl { get; internal set; }
        public string AdminId { get; internal set; }

        public Dictionary<string,string> HttpParameters { get; internal set; }

        public bool tokenHasExpired
        {
            get
            {
                long l;
                if (long.TryParse(oauth_token, out l))
                {
                    return l.DateFromUnixTime() < DateTime.Now;
                }
                return false;
            }
        }
        public bool isLiked
        {
            get
            {
                return page.liked;
            }
        }
        public bool isAdmin
        {
            get
            {
                return page.admin;
            }
        }
    
        internal FacebookInstance(FacebookConfigElement elem, string json=null)
        {
            Connected = false;

            page = new Page(null);
            user = new User(null);
            HttpParameters = new Dictionary<string,string>();

            AppId = elem.appId;
            
            AppName = elem.key;
            AdminId = elem.adminId;
            PageId = elem.pageId;
            PageName = elem.pageName;
            PageUrl = elem.pageUrl;

            if (json != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
                var fb = jss.Deserialize(json, typeof(object)) as dynamic;

                string code = fb.algorithm;
                if (code.NotEquals("HMAC-SHA256"))
                    throw new ArgumentException("Expected HMAC-SHA256");

                Connected = true;
                try { app_data = fb.app_data; }
                catch { }
                algorithm = fb.algorithm;
                try { expires = fb.expires.ToString(); }
                catch { }
                issued_at = fb.issued_at.ToString();
                try { oauth_token = fb.oauth_token; }
                catch { }
                page = new Page(fb.page);
                user = new User(fb);
            }
        }

        public override string ToString()
        {
            //don't serialize sensitive data
            HttpParameters.Remove("PATH_TRANSLATED");
            HttpParameters.Remove("HTTP_COOKIE");
            HttpParameters.RemoveWhere(x => x.StartsLike("ALL_"));
            HttpParameters.RemoveWhere(x => x.StartsLike("APPL_"));
            HttpParameters.RemoveWhere(x => x.StartsLike("ASP.NET"));
            HttpParameters.RemoveWhere(x => x.StartsLike("AUTH_"));
            HttpParameters.RemoveWhere(x => x.StartsLike("CERT_"));
            HttpParameters.RemoveWhere(x => x.StartsLike("LOCAL_ADDR"));
            HttpParameters.RemoveWhere(x => x.StartsLike("HTTPS_"));
            HttpParameters.RemoveWhere(x => x.StartsLike("INSTANCE_"));

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}
