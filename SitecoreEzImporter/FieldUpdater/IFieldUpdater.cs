using EzImporter.Configuration;
using Sitecore.Data.Fields;

namespace EzImporter.FieldUpdater
{
    /// <summary>
    /// Responsible for assigning new value into field following field semantics.
    /// <para>Field might allow multiple values to be picked, thus imported value is cut into parts and each value processed separately.</para>
    /// <para>Fields might define field source - allowed(valid) values for selection; class should verify if attempted value is appropriate to be set.</para>
    /// </summary>
    public interface IFieldUpdater
    {
        void UpdateField(Field field, string importValue, IImportOptions importOptions);
    }
}
