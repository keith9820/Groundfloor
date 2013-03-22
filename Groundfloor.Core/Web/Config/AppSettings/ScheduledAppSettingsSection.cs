using System;
using System.Configuration;

namespace Groundfloor.Web.Config
{
    public class ScheduledAppSettingsSection : ConfigurationSection
    {
        [ConfigurationProperty("appSettings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AppSettingsCollection),AddItemName = "add",ClearItemsName = "clear",RemoveItemName = "remove")]
        public AppSettingsCollection Configurations
        {
            get
            {
                AppSettingsCollection _settings = (AppSettingsCollection)base["appSettings"];
                return _settings;
            }
        }

    }
}
