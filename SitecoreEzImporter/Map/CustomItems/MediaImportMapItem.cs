using EzImporter.Extensions;
using Sitecore;
using Sitecore.Data.Items;
using System;

namespace EzImporter.Map.CustomItems
{
    [Obsolete("No direct usages, candidate for removal")]
    public class MediaImportMapItem : CustomItem
    {
        public MediaImportMapItem(Item item) : base(item)
        {
        }

        public Item TargetTemplate => ItemExtensions.GetLinkItem(InnerItem, fieldName: "TargetTemplate");

        public string InputFilenameFormat => InnerItem["InputFilenameFormat"];

        public string ItemIdProperty => InnerItem["ItemIdProperty"];

        public string ImageFieldProperty => InnerItem["ImageFieldProperty"];

        public bool UseFileNameForMediaItem => MainUtil.GetBool(InnerItem["UseFileNameForMediaItem"], defaultValue: false);

        public string NewMediaItemNameFormat => InnerItem["NewMediaItemNameFormat"];

        public string AltTextFormat => InnerItem["AltTextFormat"];
    }
}