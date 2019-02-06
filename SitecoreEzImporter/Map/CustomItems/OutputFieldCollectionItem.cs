using EzImporter.Helpers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EzImporter.Map.CustomItems
{
    public class OutputFieldCollectionItem:CustomItem
    {
        public static readonly ID TemplateId = new ID("{475A5BBD-3A5A-47CC-A65D-C1B1AC5773F0}");

        static OutputFieldCollectionItem()
            => IDNameProviderHelper.AddBindingWrapped(TemplateId, typeof(OutputFieldCollectionItem), nameof(TemplateId));

        public OutputFieldCollectionItem(Item item) : base(item)
        {
        }
    }
}