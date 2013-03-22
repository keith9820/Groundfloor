using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class UriExtensions
    {
        public static Uri AddParam(this Uri uri, string name, string value)
        {
            return AddParam(uri, string.Format("{0}={1}", name, value.Escape()));
        }
        public static Uri AddParam(this Uri uri, string querystring, params object[] args)
        {
            return AddParam(uri, string.Format(querystring, args));
        }
        public static Uri AddParam(this Uri uri, object querystring)
        {
            return AddParam(uri, querystring.ToString());
        }
        public static Uri AddParam(this Uri uri, string querystring)
        {
            string path = uri.ToString();
            if (uri.Query.IsEmpty())
                path += "?";
            else
                path += "&";

            return new Uri(string.Format("{0}{1}", path, querystring));
        }
        public static Uri Append(this Uri uri, object querysting)
        {
            return uri.Append(querysting.ToString().Escape());
        }
        public static Uri Append(this Uri uri, string querysting)
        {
            string path = uri.ToString();
            if (!path.EndsWith("/"))
                path += "/";

            return new Uri(string.Format("{0}{1}", path, querysting.Escape()));
        }
        public static Uri AppendFormat(this Uri uri, string querysting, params object[] args)
        {
            string path = uri.ToString();
            if (!path.EndsWith("/"))
                path += "/";

            return new Uri(path + string.Format(querysting, args).Escape());
        }

        public static string ToPhysicalPath(this Uri uri)
        {
            return System.Web.Hosting.HostingEnvironment.MapPath(uri.LocalPath.DecodeUrl());
        }
    }
}
