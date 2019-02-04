using Sitecore.Data.Items;
using System.Text.RegularExpressions;

namespace EzImporter
{
    /// <summary>
    /// Produces a valid name for <see cref="Sitecore.Data.Items.Item"/>.
    /// <para>Should an empty name be provided, will produce <see cref="UnNamedItem"/>.</para>
    /// <para>Relies on <see cref="ItemUtil.ProposeValidItemName(string)"/> API and applies regexing on top.</para>
    /// </summary>
    public class ItemValidNameHelper
    {
        /// <summary>
        /// Produces valid item name from provided input relying on <see cref="ItemUtil.ProposeValidItemName(string)"/>.
        /// <para>Falls back to <see cref="UnNamedItem"/>in case empy string was provided.</para>
        /// </summary>
        /// <param name="proposedName"></param>
        /// <returns></returns>
        public static string GetValidItemName(string proposedName)
        {
            var newName = proposedName;
            if (string.IsNullOrWhiteSpace(newName))
            {
                return UnNamedItem;
            }
            newName = ItemUtil.ProposeValidItemName(newName);
            newName = Regex.Replace(newName, @"\s+", " ");
            return newName;
        }

        /// <summary>
        /// Returns a default name for item without name.
        /// </summary>
        public static string UnNamedItem => "Unnamed item";
    }
}