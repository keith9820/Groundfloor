using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;

namespace Groundfloor.Pluck.Config
{
    public class PluckConfigCollection : ConfigurationElementCollection
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
            PluckConfigElement obj = (PluckConfigElement)element;
            return obj.key;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PluckConfigElement();
        }

        public PluckConfigElement this[int index]
        {
            get { return (PluckConfigElement)BaseGet(index); }
        }
    }
}
