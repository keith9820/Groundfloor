using System.Configuration;
using Groundfloor.MvcRouteConfig.Elements;

namespace Groundfloor.MvcRouteConfig
{
    public class RouteSection : ConfigurationSection
    {
        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteCollection Routes
        {
            get { return base["routes"] as RouteCollection; }
        }
    }
}