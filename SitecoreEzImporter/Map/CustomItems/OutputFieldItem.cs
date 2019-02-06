using EzImporter.Extensions;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace EzImporter.Map.CustomItems
{
    public class OutputFieldItem : CustomItem
    {
        public static readonly ID TemplateId = new ID("{317A4F55-F36E-4E6E-A411-85883BFD4496}");

        static OutputFieldItem()
        {
            try
            {
                Sitecore.Diagnostics.Debugger.IDNameProvider.AddBinding(TemplateId, $"{nameof(OutputFieldItem)}TemplateId");
            }
            catch { }
        }

        public OutputFieldItem(Item item) : base(item)
        {            
        }

        public Item InputField => ItemExtensions.GetLinkItem(InnerItem, fieldName: "InputField");
    }
}