using System;
using System.Configuration;

namespace Groundfloor.Facebook.Config
{
    public class FacebookConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("facebookConfigurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(FacebookConfigCollection),
                    AddItemName = "add",
                    ClearItemsName = "clear",
                    RemoveItemName = "remove")]
        public FacebookConfigCollection Configurations
        {
            get
            {
                FacebookConfigCollection _facebookconfigs = (FacebookConfigCollection)base["facebookConfigurations"];
                return _facebookconfigs;
            }
        }

    }
}
