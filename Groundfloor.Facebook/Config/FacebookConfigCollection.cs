using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;

namespace Groundfloor.Facebook.Config
{
    public class FacebookConfigCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("Default")]
        public string Default
        {
            get { return (string)this["Default"]; }
            set { this["Default"] = value; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            FacebookConfigElement obj = (FacebookConfigElement)element;
            return obj.key;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FacebookConfigElement();
        }

        public FacebookConfigElement this[int index]
        {
            get { return (FacebookConfigElement)BaseGet(index); }
        }
    }
}
