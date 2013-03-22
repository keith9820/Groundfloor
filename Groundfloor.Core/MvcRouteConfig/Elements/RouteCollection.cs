using System;
using System.Configuration;

namespace Groundfloor.MvcRouteConfig.Elements
{
    [ConfigurationCollection(typeof(RouteElement))]
    public class RouteCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((RouteElement)element).Name;
        }

        public RouteElement this[int index]
        {
            get { return (RouteElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
    }
}