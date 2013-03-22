using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;

namespace Groundfloor.Web.Config
{
    [ConfigurationCollection(typeof(CommandElement))]
    public class CommandElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "add";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommandElement)element).method;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandElement();
        }

        public CommandElement this[int index]
        {
            get { return (CommandElement)BaseGet(index); }
        }

        public new CommandElement this[string key]
        {
            get 
            {
                return (CommandElement)BaseGet(key);
            }
        }
    }
}
