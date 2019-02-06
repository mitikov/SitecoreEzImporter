using EzImporter.Extensions;
using EzImporter.Helpers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EzImporter.Map.CustomItems
{
    /// <summary>
    /// Carries links to <see cref="TargetTemplate"/> and <see cref="ItemNameField"/>.
    /// </summary>
    public class OutputMapTemplateItem : CustomItem
    {
        public static readonly ID TemplateId = new ID("{58623B95-07DC-4F00-8D7A-337BF116FB54}");

        static OutputMapTemplateItem() 
            => IDNameProviderHelper.AddBindingWrapped(TemplateId, typeof(OutputFieldItem), nameof(TemplateId));

        public OutputMapTemplateItem(Item item) : base(item)
        {
        }

        public Item TargetTemplate => InnerItem.GetLinkItem("TargetTemplate");

        public Item ItemNameField => InnerItem.GetLinkItem("ItemNameField");
    }
}