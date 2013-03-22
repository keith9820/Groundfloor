using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;


namespace Groundfloor.Facebook.Config
{
    public class FacebookConfigManager
    {
        private readonly static FacebookConfigSection _facebookConfigSection = null;

        static FacebookConfigManager()
        {
            _facebookConfigSection = (FacebookConfigSection)System.Configuration.ConfigurationManager.GetSection("FacebookConfiguration");
        }

        public static FacebookConfigElement GetInstance(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = _facebookConfigSection.Configurations.Default;
                Debug.WriteLine("Missing Facebook configuration key, using default value'{0}'", key);
            }
         
            if (String.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException(string.Format("Facebook configuration key is not provided and there is no default key configuration."));
            }

            foreach (FacebookConfigElement _config in _facebookConfigSection.Configurations)
            {
                if (_config.key.Equals(key))
                    return _config;
            }

            return _facebookConfigSection.Configurations[0];
        }
    }
}
