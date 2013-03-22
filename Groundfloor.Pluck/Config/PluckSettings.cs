using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Groundfloor.Pluck.Config
{
    public class PluckConfigurationManager
    {
        static PluckConfigurationManager _config = new PluckConfigurationManager();

        public static PluckConfigurationManager PluckSettings
        {
            get
            {
                return _config;
            }
        }

        public PluckConfigElement this[string key]
        {
            get
            {
                return Groundfloor.Pluck.Config.PluckConfigManager.GetInstance(key);
            }
        }
    }
}
