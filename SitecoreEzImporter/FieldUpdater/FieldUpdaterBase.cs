using System;
using EzImporter.Configuration;

namespace EzImporter.FieldUpdater
{
    [Obsolete("Seem not used, considered to be removed")]
    public abstract class FieldUpdaterBase : IFieldUpdater
    {
        public void UpdateField(Sitecore.Data.Fields.Field field, string importValue, IImportOptions importOptions)
        {
            throw new NotImplementedException();
        }
    }
}