using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Groundfloor.Web.Config
{
    public class ScheduledAppSettings
    {
        static ScheduledAppSettings _config = new ScheduledAppSettings();

        public static ScheduledAppSettings AppSettings
        {
            get
            {
                return _config;
            }
        }

        public string this[string key, DateTime date]
        {
            get
            {
                return ScheduledAppSettingsManager.GetValue(key, date);
            }
        }
    }
}
