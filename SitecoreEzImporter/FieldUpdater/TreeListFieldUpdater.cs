﻿using EzImporter.Configuration;
using System;
using Sitecore.Data;

namespace EzImporter.FieldUpdater
{
    /// <summary>
    /// Locates imported values under field source selection options, and appends IDs of found items to field value.
    /// <para>Imported value might be a set of values separated by <see cref="IImportOptions.MultipleValuesImportSeparator"/>.</para>
    /// </summary>
    public class TreeListFieldUpdater : IFieldUpdater
    {
        public void UpdateField(Sitecore.Data.Fields.Field field, string importValue, IImportOptions importOptions)
        {
            try
            {
                var separator = new[] {importOptions.MultipleValuesImportSeparator};
                var selectionSource = field.Item.Database.SelectSingleItem(field.Source);
                var importValues = importValue != null
                    ? importValue.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    : new string[] {};
                var idListValue = "";
                foreach (var value in importValues)
                {
                    var query = ID.IsID(value)
                        ? ".//*[@@id='" + ID.Parse(value) + "']"
                        : "." +
                          Sitecore.StringUtil.EnsurePrefix('/',
                              value.Replace(importOptions.TreePathValuesImportSeparator, "/"));
                    var item = selectionSource.Axes.SelectSingleItem(query);
                    if (item != null)
                    {
                        idListValue += "|" + item.ID;
                    }
                    else
                    {
                        if (importOptions.InvalidLinkHandling == InvalidLinkHandling.SetBroken)
                        {
                            idListValue += "|" + value;
                        }
                        else if (importOptions.InvalidLinkHandling == InvalidLinkHandling.SetEmpty)
                        {
                            idListValue += "|";
                        }
                    }
                }
                if (idListValue.StartsWith("|"))
                {
                    idListValue = idListValue.Substring(1);
                }
                field.Value = idListValue;
            }
            catch (Exception ex)
            {
                if (importOptions.InvalidLinkHandling == InvalidLinkHandling.SetBroken)
                {
                    field.Value = importValue;
                }
                else if (importOptions.InvalidLinkHandling == InvalidLinkHandling.SetEmpty)
                {
                    field.Value = string.Empty;
                }
            }
        }
    }
}