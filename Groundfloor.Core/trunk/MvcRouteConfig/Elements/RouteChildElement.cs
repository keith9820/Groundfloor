using System.Collections.Generic;
using System.Configuration;

namespace Groundfloor.MvcRouteConfig.Elements
{
    public class RouteChildElement : ConfigurationElement
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
    }
}
