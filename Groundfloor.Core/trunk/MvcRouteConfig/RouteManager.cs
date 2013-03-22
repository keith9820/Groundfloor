//http://www.tomdupont.net/2011/11/configuring-mvc-routes-in-webconfig.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Groundfloor.MvcRouteConfig.Elements;
using RouteCollection = System.Web.Routing.RouteCollection;

namespace Groundfloor.MvcRouteConfig
{
    public class RouteManager
    {
        protected const string Optional = "Optional";

        public void RegisterRoutes(RouteCollection routes)
        {
            RouteSection routesTableSection = GetRouteTableConfigurationSection();

            if (routesTableSection == null || routesTableSection.Routes.Count <= 0)
                return;

            for (int routeIndex = 0; routeIndex < routesTableSection.Routes.Count; routeIndex++)
            {
                var routeElement = routesTableSection.Routes[routeIndex];

                var route = new Route(
                    routeElement.Url,
                    GetDefaults(routeElement),
                    GetConstraints(routeElement),
                    GetDataTokens(routeElement),
                    GetInstanceOfRouteHandler(routeElement));

                routes.Add(routeElement.Name, route);
            }
        }

        private static RouteSection GetRouteTableConfigurationSection()
        {
            try
            {
                return (RouteSection)WebConfigurationManager.GetSection("routeTable");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can't find section <routeTable> in the configuration file", ex);
            }
        }

        protected virtual IRouteHandler GetInstanceOfRouteHandler(RouteElement route)
        {
            if (String.IsNullOrEmpty(route.RouteHandlerType))
                return new MvcRouteHandler();
            
            try
            {
                Type routeHandlerType = Type.GetType(route.RouteHandlerType);
                return Activator.CreateInstance(routeHandlerType) as IRouteHandler;
            }
            catch (Exception ex)
            {
                var message = String.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType);
                throw new ApplicationException(message, ex);
            }
        }

        protected virtual RouteValueDictionary GetConstraints(RouteElement route)
        {
            try
            {
                var dictionary = GetDictionaryFromAttributes(route.Constraints.Attributes);

                for (var i = 0; i < route.Constraints.Count; i++)
                {
                    var constraint = route.Constraints[i];
                    var routeConstraintType = Type.GetType(constraint.Type);

                    IRouteConstraint routeConstraint;
                    if (constraint.Params.Attributes.Count > 0)
                    {
                        var parameters = constraint.Params.Attributes.Values.ToArray();
                        routeConstraint = (IRouteConstraint)Activator.CreateInstance(routeConstraintType, parameters);
                    }
                    else
                        routeConstraint = (IRouteConstraint)Activator.CreateInstance(routeConstraintType);

                    dictionary.Add(constraint.Name, routeConstraint);
                }

                return dictionary;
            }
            catch (Exception ex)
            {
                var message = String.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType);
                throw new ApplicationException(message, ex);
            }
        }

        protected virtual RouteValueDictionary GetDefaults(RouteElement route)
        {
            var dataTokensDictionary = new RouteValueDictionary();

            foreach (var dataToken in route.Defaults.Attributes)
                if (dataToken.Value.Equals(Optional, StringComparison.InvariantCultureIgnoreCase))
                    dataTokensDictionary.Add(dataToken.Key, UrlParameter.Optional);
                else
                    dataTokensDictionary.Add(dataToken.Key, dataToken.Value);

            return dataTokensDictionary;
        }

        protected virtual RouteValueDictionary GetDataTokens(RouteElement route)
        {
            return GetDictionaryFromAttributes(route.DataTokens.Attributes);
        }

        protected RouteValueDictionary GetDictionaryFromAttributes(Dictionary<string, string> attributes)
        {
            var dataTokensDictionary = new RouteValueDictionary();

            foreach (var dataTokens in attributes)
                dataTokensDictionary.Add(dataTokens.Key, dataTokens.Value);

            return dataTokensDictionary;
        }
    }
}