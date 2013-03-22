using System;
using System.Configuration;

namespace Groundfloor.Pluck.Config
{
    public class PluckConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("pluckConfigurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(PluckConfigCollection),
                    AddItemName = "add",
                    ClearItemsName = "clear",
                    RemoveItemName = "remove")]
        public PluckConfigCollection Configurations
        {
            get
            {
                PluckConfigCollection _Pluckconfigs = (PluckConfigCollection)base["pluckConfigurations"];
                return _Pluckconfigs;
            }
        }

    }
}
