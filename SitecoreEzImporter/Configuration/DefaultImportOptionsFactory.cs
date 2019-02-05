using System;

namespace EzImporter.Configuration
{
    public class DefaultImportOptionsFactory : ImportOptionsFactory
    {
        public override IImportOptions GetDefaultImportOptions()
        {
            return new ImportOptions
            {
                ExistingItemHandling = GetEnum(SettingNames.ExistingItemHandling, defaultValue: ExistingItemHandling.AddVersion),
                InvalidLinkHandling = GetEnum(SettingNames.InvalidLinkHandling, defaultValue: InvalidLinkHandling.SetBroken),
                MultipleValuesImportSeparator = GetSetting(SettingNames.MultipleValuesImportSeparator, "|"),
                TreePathValuesImportSeparator = GetSetting(SettingNames.TreePathValuesImportSeparator, @"\"),
                CsvDelimiter = new[] { GetSetting(SettingNames.CsvDelimiter, ",") },
                FirstRowAsColumnNames = GetBoolSetting(SettingNames.FirstRowAsColumnNames, defaultValue: true)
            };
        }

        protected TEnum GetEnum<TEnum>(string settingName, TEnum defaultValue) where TEnum : struct
        {
            var textValue = GetSetting(settingName, defaultValue: string.Empty);

            return Enum.TryParse(textValue, out TEnum parsed) ? parsed : defaultValue;
        }

        internal virtual string GetSetting(string settingName, string defaultValue) => Sitecore.Configuration.Settings.GetSetting(settingName, defaultValue);

        internal virtual bool GetBoolSetting(string settingName, bool defaultValue) => Sitecore.Configuration.Settings.GetBoolSetting(settingName, defaultValue);
    }
}