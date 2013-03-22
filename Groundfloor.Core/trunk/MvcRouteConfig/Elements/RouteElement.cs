using System.Configuration;

namespace Groundfloor.MvcRouteConfig.Elements
{
    public class RouteElement : ConfigurationElement
    {
        public RouteElement()
        {
        }

        public RouteElement(string name)
        {
            Name = name;
        }

        public RouteElement(string name, string url, string routeHandlerType)
        {
            Name = name;
            Url = url;
            RouteHandlerType = routeHandlerType;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("routeHandlerType", IsRequired = false)]
        public string RouteHandlerType
        {
            get { return (string)this["routeHandlerType"]; }
            set { this["routeHandlerType"] = value; }
        }

        [ConfigurationProperty("defaults", IsRequired = false)]
        public RouteChildElement Defaults
        {
            get { return (RouteChildElement)this["defaults"]; }
            set { this["defaults"] = value; }
        }

        [ConfigurationProperty("dataTokens", IsRequired = false)]
        public RouteChildElement DataTokens
        {
            get { return (RouteChildElement)this["dataTokens"]; }
            set { this["dataTokens"] = value; }
        }

        [ConfigurationProperty("constraints", IsDefaultCollection = false, IsRequired = false)]
        public ConstraintCollection Constraints
        {
            get { return base["constraints"] as ConstraintCollection; }
        }
    }
}