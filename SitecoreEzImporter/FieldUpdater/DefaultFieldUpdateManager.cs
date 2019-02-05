using EzImporter.Configuration;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzImporter.FieldUpdater
{
    public class DefaultFieldUpdateManager : FieldUpdateManager
    {
        public override void UpdateField(Field field, string importValue, IImportOptions importOptions)
        {
            IFieldUpdater updater = FindFieldUpdaterFor(field);
            updater.UpdateField(field, importValue, importOptions);
        }

        protected virtual IFieldUpdater FindFieldUpdaterFor(Field field)
        {
            if (field.Type == "Droplink")
            {
                return new DropLinkFieldUpdater();
            }
            if (field.Type == "Droptree")
            {
                return new DropTreeFieldUpdater();
            }
            if (field.Type == "Checklist")
            {
                return new CheckBoxListFieldUpdater();
            }
            if (field.Type == "Treelist" ||
                field.Type == "TreelistEx")
            {
                return new TreeListFieldUpdater();
            }
            return new TextFieldUpdater();
        }
    }
}