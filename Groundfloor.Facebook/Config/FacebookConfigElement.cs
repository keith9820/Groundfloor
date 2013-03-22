using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Groundfloor.Facebook.Config
{
    public class FacebookConfigElement : ConfigurationElement
    {
        public FacebookConfigElement() { }
        public FacebookConfigElement(string key, string appId, string appSecret, string pageurl)
        {
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("appId", IsRequired = true)]
        public string appId
        {
            get { return (string)this["appId"]; }
            set { this["appId"] = value; }
        }

        [ConfigurationProperty("appSecret", IsRequired = true)]
        public string appSecret
        {
            get { return (string)this["appSecret"]; }
            set { this["appSecret"] = value; }
        }

        [ConfigurationProperty("pageId")]
        public string pageId
        {
            get
            {
                return string.Format(this["pageId"].ToString(), this.appId);
            }
            set { this["pageId"] = value; }
        }

        [ConfigurationProperty("adminId", IsRequired = true)]
        public string adminId
        {
            get { return (string)this["adminId"]; }
            set { this["adminId"] = value; }
        }
        [ConfigurationProperty("pageName", IsRequired = true)]
        public string pageName
        {
            get { return (string)this["pageName"]; } //todo:  fetch page name from graph API
            set { this["pageName"] = value; }
        }
        public string pageUrl
        {
            get 
            {
                //todo:  pages when in sandbox mode?
                //return string.Format("http://www.facebook.com/pages/{0}/{1}?sk=app_{2}", pageName, pageId, appId);
                return string.Format("http://www.facebook.com/{0}/?sk=app_{1}&app={2}", pageName, appId, key);
            }
        }

        [ConfigurationProperty("accessToken")]
        public string accessToken
        {
            get { return (string)this["accessToken"]; }
            set { this["accessToken"] = value; }
        }
    }
}
