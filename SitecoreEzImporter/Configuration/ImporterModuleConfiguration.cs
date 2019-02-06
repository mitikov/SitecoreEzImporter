using System;

namespace EzImporter.Configuration
{
    [Obsolete("No direct usages, candidate for removal")]
    public class ImporterModuleConfiguration
    {
        public static ImporterModuleConfiguration GetConfigurationSettings()
        {
            return new ImporterModuleConfiguration
            {
                MapsLocation = Sitecore.Configuration.Settings.GetSetting("EzImporter.MapsLocation", ""),
                RootItemQuery = Sitecore.Configuration.Settings.GetSetting("EzImporter.RootItemQuery", ""),
                ImportDirectory =
                    Sitecore.Configuration.Settings.GetSetting("EzImporter.ImportDirectory", "~/temp/EzImporter"),
                ImportItemsSubDirectory =
                    Sitecore.Configuration.Settings.GetSetting("EzImporter.ImportItemsSubDirectory", "Items"),
                ImportMediaSubDirectory =
                    Sitecore.Configuration.Settings.GetSetting("EzImporter.ImportMediaSubDirectory", "Items")
            };
        }

        public string MapsLocation { get; set; }
        public string RootItemQuery { get; set; }
        public string ImportDirectory { get; set; }
        public string ImportItemsSubDirectory { get; set; }
        public string ImportMediaSubDirectory { get; set; }
    }
}