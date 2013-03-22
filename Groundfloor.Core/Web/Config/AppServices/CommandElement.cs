using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Groundfloor.Web.Config
{
    public class CommandElement : ConfigurationElement
    {
        public CommandElement() { }
        public CommandElement(string host)
        {
        }

        [ConfigurationProperty("method", IsRequired = true, IsKey = true)]
        public string method
        {
            get { return (string)this["method"]; }
            set { this["method"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("data", IsRequired = false)]
        public string data
        {
            get { return (string)this["data"]; }
            set { this["data"] = value; }
        }

        [ConfigurationProperty("description", IsRequired = false)]
        public string description
        {
            get { return (string)this["description"]; }
            set { this["description"] = value; }
        }
    }
}
