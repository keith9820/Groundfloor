using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace Groundfloor.Web.Config
{
    public class ScheduledAppSettingsManager
    {
        private readonly static ScheduledAppSettingsSection _settingsSection = null;

        static ScheduledAppSettingsManager()
        {
            _settingsSection = (ScheduledAppSettingsSection)System.Configuration.ConfigurationManager.GetSection("ScheduledAppSettings");
        }

        //find the appsetting with a matching key that incldues the given date
        public static string GetValue(string key)
        {
            return GetValue(key, DateTime.Now);
        }
        public static string GetValue(string key, DateTime date)
        {
            if (String.IsNullOrEmpty(key))
            {
                return null;
            }

            //the "to" attribute can be empty so need to find the best match when only "from" date is given
            // given this scenario:
            //      <add key="CurrentPhase" value="2" from="01/01/2012 00:00" to="" />
            //      <add key="CurrentPhase" value="3" from="02/15/2012 00:00" to="" />
            //      <add key="CurrentPhase" value="4" from="03/15/2012 19:00" to="" />
            // And today is 4/1/2012, need to return the last open-ended date range (the one with from date closest to today)

            List<AppSettingsElement> candidates = new List<AppSettingsElement>();

            foreach (AppSettingsElement _config in _settingsSection.Configurations)
            {
                if (_config.key.Equals(key))
                {
                    DateTime to = _config.to.GetValueOrDefault(DateTime.Now);
                    if (date >= _config.from && date <= to)
                    {
                        candidates.Add(_config);
                    }

                    if (candidates.Count > 0)
                    {
                        return candidates.OrderByDescending(x => x.from).ToList()[0].value;
                    }
                }
            }
            return null;
        }
    }
}
