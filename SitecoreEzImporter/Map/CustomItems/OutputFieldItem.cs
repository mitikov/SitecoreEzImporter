using EzImporter.Extensions;
using EzImporter.Helpers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EzImporter.Map.CustomItems
{
    public class OutputFieldItem : CustomItem
    {
        public static readonly ID TemplateId = new ID("{317A4F55-F36E-4E6E-A411-85883BFD4496}");

        static OutputFieldItem()
            => IDNameProviderHelper.AddBindingWrapped(TemplateId, typeof(OutputFieldItem), nameof(TemplateId));

        public OutputFieldItem(Item item) : base(item)
        {
        }

        public Item InputField => InnerItem.GetLinkItem(fieldName: "InputField");
    }
}