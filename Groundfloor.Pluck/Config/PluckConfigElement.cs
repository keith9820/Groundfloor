using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Groundfloor.Pluck.Config
{
    public class PluckConfigElement : ConfigurationElement
    {
        public PluckConfigElement() { }
        public PluckConfigElement(string key, string galleryKey, string sharedSecret, string userKey, string userNickname, string userEmail, string apiUrl, string uploadUrl)
        {
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("galleryKey", IsRequired = true)]
        public string galleryKey
        {
            get { return (string)this["galleryKey"]; }
            set { this["galleryKey"] = value; }
        }

        [ConfigurationProperty("sharedSecret", IsRequired = true)]
        public string sharedSecret
        {
            get { return (string)this["sharedSecret"]; }
            set { this["sharedSecret"] = value; }
        }

        [ConfigurationProperty("userKey", IsRequired = true)]
        public string userKey
        {
            get { return (string)this["userKey"]; }
            set { this["userKey"] = value; }
        }

        [ConfigurationProperty("userNickname", IsRequired = true)]
        public string userNickname
        {
            get { return (string)this["userNickname"]; }
            set { this["userNickname"] = value; }
        }

        [ConfigurationProperty("userEmail", IsRequired = true)]
        public string userEmail
        {
            get { return (string)this["userEmail"]; }
            set { this["userEmail"] = value; }
        }

        [ConfigurationProperty("apiUrl", IsRequired = true)]
        public string apiUrl
        {
            get { return (string)this["apiUrl"]; }
            set { this["apiUrl"] = value; }
        }

        [ConfigurationProperty("uploadUrl", IsRequired = true)]
        public string uploadUrl
        {
            get { return (string)this["uploadUrl"]; }
            set { this["uploadUrl"] = value; }
        }
    }
}
