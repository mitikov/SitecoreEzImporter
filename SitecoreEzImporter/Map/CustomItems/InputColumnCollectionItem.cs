using EzImporter.Helpers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EzImporter.Map.CustomItems
{
    public class InputColumnCollectionItem : CustomItem
    {
        public static readonly ID TemplateId = new ID("{1A50AD7B-C6C8-4B6D-991B-37885A662DF1}");

        static InputColumnCollectionItem()
            => IDNameProviderHelper.AddBindingWrapped(TemplateId, typeof(InputColumnCollectionItem), nameof(TemplateId));
        
        public InputColumnCollectionItem(Item item) : base(item)
        {            
        }
    }
}