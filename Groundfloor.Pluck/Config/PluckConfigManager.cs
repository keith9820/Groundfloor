using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace Groundfloor.Pluck.Config
{
    public class PluckConfigManager
    {
        private readonly static PluckConfigSection _PluckConfigSection = null;

        static PluckConfigManager()
        {
            _PluckConfigSection = (PluckConfigSection)System.Configuration.ConfigurationManager.GetSection("PluckConfiguration");
        }

        public static PluckConfigElement GetInstance(string key)
        {
            if (!String.IsNullOrEmpty(_PluckConfigSection.Configurations.Default))
                key = _PluckConfigSection.Configurations.Default;

            if (String.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException(string.Format("No Pluck configuration matches the supplied app name '{0}'", key));
                //return _PluckConfigSection.Configurations[0];
            }

            foreach (PluckConfigElement _config in _PluckConfigSection.Configurations)
            {
                if (_config.key.Equals(key))
                    return _config;
            }

            return _PluckConfigSection.Configurations[0];
        }
    }
}
