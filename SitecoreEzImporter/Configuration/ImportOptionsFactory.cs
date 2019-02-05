using System;

namespace EzImporter.Configuration
{
    /// <summary>
    /// Provides configuration values for import process based on Sitecore settings.
    /// </summary>
    public abstract class ImportOptionsFactory
    {
        public abstract IImportOptions GetDefaultImportOptions();
    }
}