using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;

namespace Groundfloor.Web.Config
{
    public class AppSettingsCollection : ConfigurationElementCollection
    {
        protected override bool ThrowOnDuplicate
        {
            get
            {
                return false;
            }
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
            AppSettingsElement obj = (AppSettingsElement)element;
            return obj.key;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AppSettingsElement();
        }

        public AppSettingsElement this[int index]
        {
            get { return (AppSettingsElement)BaseGet(index); }
        }
    }
}
