using System.Diagnostics;

namespace EzImporter.Map
{
    [DebuggerDisplay("{SourceColumn}#{TargetFieldName}")]
    public class OutputField
    {
        public string SourceColumn { get; set; }
        public string TargetFieldName { get; set; }
    }
}