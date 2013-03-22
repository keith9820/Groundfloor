using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace Groundfloor.Web.Config
{
    public class AppServicesSettings
    {
        //static AppServicesSettings _instance = null;
        //public static AppServicesSettings Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new AppServicesSettings();
        //        return _instance;
        //    }
        //}
        public static AppServicesSection AppServicesConfiguration
        {
            get
            {
                return (AppServicesSection)ConfigurationManager.GetSection("AppServices");
            }
        }
        public static CommandElementCollection CommandsConfiguration
        {
            get
            {
                return AppServicesConfiguration.CommandElements;
            }
        }

        public static IEnumerable<CommandElement> Commands
        {
            get
            {
                foreach (CommandElement element in CommandsConfiguration)
                {
                    if (element != null)
                        yield return element;
                }
            }
        }
    }
}
