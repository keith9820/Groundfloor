using System.Collections.Generic;
using System.Configuration;

namespace Groundfloor.MvcRouteConfig.Elements
{
    [ConfigurationCollection(typeof(ConstraintElement))]
    public class ConstraintCollection : ConfigurationElementCollection
    {
        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

        public Dictionary<string, string> Attributes
        {
            get { return _attributes; }
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            if (_attributes.ContainsKey(name))
                return false;

            _attributes.Add(name, value);
            return true;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConstraintElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConstraintElement)element).Name;
        }

        public ConstraintElement this[int index]
        {
            get { return (ConstraintElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
    }
}