using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Groundfloor.Web.Config
{
    public class AppSettingsElement : ConfigurationElement
    {
        public AppSettingsElement() { }
        public AppSettingsElement(string key, string value, string from, string to)
        {
            this.key = key;
            this.value = value;

            this.from = Convert.ToDateTime(from);
            DateTime dt;
            if (DateTime.TryParse(to, out dt))
                this.to = dt;
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }

        [ConfigurationProperty("from", IsRequired = true)]
        public DateTime from
        {
            get 
            { 
                return DateTime.Parse(this["from"].ToString()); 
            }
            set { this["from"] = value.ToString(); }
        }

        [ConfigurationProperty("to", IsRequired = false)]
        public DateTime? to
        {
            get 
            {
                if (this["to"] != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(this["to"].ToString(), out dt))
                        return dt;
                }
                return null; 
            }
            set 
            {
                this["to"] = value.HasValue ? value.Value.ToString() : "";
            }
        }
    }
}
