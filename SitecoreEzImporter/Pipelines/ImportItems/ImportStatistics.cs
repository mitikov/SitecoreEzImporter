using System.Text;

namespace EzImporter.Pipelines.ImportItems
{
    /// <summary>
    /// Specifies how many <see cref="InputDataRows"/> resulted into <see cref="CreatedItems"/> and <see cref="UpdatedItems"/>.
    /// <para>Carries <see cref="Log"/> to dump import-related messages.</para>
    /// </summary>    
    public class ImportStatistics
    {
        public int InputDataRows { get; set; }
        public int CreatedItems { get; set; }
        public int UpdatedItems { get; set; }
        public readonly StringBuilder Log;

        public ImportStatistics()
        {
            InputDataRows = 0;
            CreatedItems = 0;
            UpdatedItems = 0;
            Log = new StringBuilder();
        }

        public override string ToString()
            => $"{InputDataRows} rows read from input source.\r\n{CreatedItems} items created.\r\n{UpdatedItems} items updated.";
    }
}