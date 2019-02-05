using System;

namespace EzImporter.Configuration
{
    public class DefaultImportOptionsFactory : ImportOptionsFactory
    {
        public override IImportOptions GetDefaultImportOptions()
        {
            var value = Sitecore.Configuration.Settings.GetSetting("EzImporter.ExistingItemHandling", "AddVersion");
            ExistingItemHandling existingItemHandling;
            if (!Enum.TryParse<ExistingItemHandling>(value, out existingItemHandling))
            {
                existingItemHandling = EzImporter.ExistingItemHandling.AddVersion;
            }

            var invalidLinkHandlingValue = Sitecore.Configuration.Settings.GetSetting("EzImporter.InvalidLinkHandling",
                "SetBroken");
            InvalidLinkHandling invalidLinkHandling;
            if (!Enum.TryParse<InvalidLinkHandling>(invalidLinkHandlingValue, out invalidLinkHandling))
            {
                invalidLinkHandling = EzImporter.InvalidLinkHandling.SetBroken;
            }

            return new ImportOptions
            {
                ExistingItemHandling = existingItemHandling,
                InvalidLinkHandling = invalidLinkHandling,
                MultipleValuesImportSeparator =
                    GetSetting("EzImporter.MultipleValuesImportSeparator", "|"),
                TreePathValuesImportSeparator =
                    GetSetting("EzImporter.TreePathValuesImportSeparator", @"\"),
                CsvDelimiter = new[]
                {
                    GetSetting("EzImporter.CsvDelimiter", ",")
                },
                FirstRowAsColumnNames = GetBoolSetting("EzImporter.FirstRowAsColumnNames", true)
            };
        }

        protected TEnum GetEnum<TEnum>(string settingName, TEnum defaultValue) where TEnum : struct
        {
            var textValue = GetSetting(settingName, defaultValue: string.Empty);

            if (!Enum.TryParse(textValue, out TEnum parsed))
            {
                parsed = defaultValue;
            }
            return parsed;
        }

        internal virtual string GetSetting(string settingName, string defaultValue) => Sitecore.Configuration.Settings.GetSetting(settingName, defaultValue);

        internal virtual bool GetBoolSetting(string settingName, bool defaultValue) => Sitecore.Configuration.Settings.GetBoolSetting(settingName, defaultValue);
    }
}