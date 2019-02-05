using EzImporter.Pipelines.ImportItems;

namespace EzImporter.DataReaders
{
    public interface IDataReader
    {
        /// <summary>
        /// Populates <see cref="ImportItemsArgs.ImportData"/> set from provided args.
        /// </summary>
        /// <param name="args"></param>
        void ReadData(ImportItemsArgs args);
        string[] GetColumnNames(ImportItemsArgs args);
    }
}
