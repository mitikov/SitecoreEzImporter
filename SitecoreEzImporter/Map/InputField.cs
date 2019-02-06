using System.Diagnostics;

namespace EzImporter.Map
{
    /// <summary>
    /// Carries input field name.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class InputField
    {
        public string Name { get; set; }
    }
}
