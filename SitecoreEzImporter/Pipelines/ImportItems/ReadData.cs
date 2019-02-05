using Sitecore.Abstractions;
using Sitecore.Diagnostics;

namespace EzImporter.Pipelines.ImportItems
{
    public class ReadData : ImportItemsProcessor
    {
        private readonly BaseLog _log;

        public ReadData(BaseLog log)
        {
            Assert.ArgumentNotNull(log, nameof(log));

            _log = log;
        }

        public override void Process(ImportItemsArgs args)
        {
            DataReaders.IDataReader reader;
            if (args.FileExtension == "csv")
            {
                reader = new DataReaders.CsvDataReader(_log);
            }
            else if (args.FileExtension == "xlsx" ||
                     args.FileExtension == "xls")
            {
                reader = new DataReaders.XlsxDataReader(_log);
            }
            else
            {
                _log.Info("EzImporter: Unsupported file format supplied. DataImporter accepts *.CSV and *.XLSX files",
                    this);
                return;
            }
            reader.ReadData(args);
            args.Statistics.InputDataRows = args.ImportData.Rows.Count;
        }
    }
}