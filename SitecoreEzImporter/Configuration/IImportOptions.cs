namespace EzImporter.Configuration
{
    /// <summary>
    /// Defines how to handle invalid item links, or existing items during import.
    /// <para>Specifies separators to be used during import.</para>
    /// </summary>
    public interface IImportOptions
    {
        InvalidLinkHandling InvalidLinkHandling { get; set; }
        ExistingItemHandling ExistingItemHandling { get; set; }

        /// <summary>
        /// Some field types allow multiple values to be set (like mutlilist allows multiple items to be selected).
        /// <para>The property defines how input values are delimited (f.e. by comma, or pipe).</para>
        /// </summary>
        string MultipleValuesImportSeparator { get; set; }
        string TreePathValuesImportSeparator { get; set; }
        string[] CsvDelimiter { get; set; }
        bool FirstRowAsColumnNames { get; set; }
    }
}
