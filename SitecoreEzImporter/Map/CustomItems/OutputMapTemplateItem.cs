using EzImporter.Extensions;
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
        {
            try
            {
                Sitecore.Diagnostics.Debugger.IDNameProvider.AddBinding(TemplateId, $"{nameof(OutputMapTemplateItem)}TemplateId");
            }
            catch { }
        }

        public OutputMapTemplateItem(Item item) : base(item)
        {
        }

        public Item TargetTemplate => ItemExtensions.GetLinkItem(InnerItem, "TargetTemplate");

        public Item ItemNameField => ItemExtensions.GetLinkItem(InnerItem, "ItemNameField");
    }
}