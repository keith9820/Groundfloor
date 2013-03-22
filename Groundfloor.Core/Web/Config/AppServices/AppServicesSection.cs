using System;
using System.Configuration;

namespace Groundfloor.Web.Config
{
    public class AppServicesSection : ConfigurationSection
    {
        [ConfigurationProperty("commands")]
        public CommandElementCollection CommandElements
        {
            get
            {
                return (CommandElementCollection)base["commands"];
            }
            set { base["commands"] = value; }
        }

        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
                //return bool.Parse(this["enabled"].ToString());
            }
            set { this["enabled"] = value; }
        }
        [ConfigurationProperty("requestTTLSeconds", DefaultValue = 3600, IsRequired = false)]
        public int requestTTLSeconds
        {
            get
            {
                return (int)this["requestTTLSeconds"];
            }
            set { this["requestTTLSeconds"] = value; }
        }
        [ConfigurationProperty("secret", IsRequired = false)]
        public string secret
        {
            get
            {
                return (string)this["secret"];
            }
            set { this["secret"] = value; }
        }
    }
}
