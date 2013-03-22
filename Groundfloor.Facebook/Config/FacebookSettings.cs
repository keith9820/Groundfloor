using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Groundfloor.Facebook.Config
{
    public class FacebookConfigurationManager
    {
        static FacebookConfigurationManager _config = new FacebookConfigurationManager();

        public static FacebookConfigurationManager FacebookSettings
        {
            get
            {
                return _config;
            }
        }

        public FacebookConfigElement this[string key]
        {
            get
            {
                return Groundfloor.Facebook.Config.FacebookConfigManager.GetInstance(key);
            }
        }
    }
}
