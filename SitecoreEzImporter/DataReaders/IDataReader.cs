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

        /// <summary>
        /// Reads column names from document specified in <paramref name="args"/>.
        /// <para>In most scenarios first met row is assumed to contain column names.</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        string[] GetColumnNames(ImportItemsArgs args);
    }
}
