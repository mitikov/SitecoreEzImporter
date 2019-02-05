using EzImporter.Configuration;
using Sitecore.Data.Fields;

namespace EzImporter.FieldUpdater
{
    public abstract class FieldUpdateManager
    {
        public abstract void UpdateField(Field field, string importValue, IImportOptions importOptions);
    }
}