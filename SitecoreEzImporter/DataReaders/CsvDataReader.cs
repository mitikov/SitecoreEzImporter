using EzImporter.Pipelines.ImportItems;
using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using System;
using System.IO;

namespace EzImporter.DataReaders
{
    public class CsvDataReader : IDataReader
    {
        private readonly BaseLog _log;

        public CsvDataReader(BaseLog log)
        {
            Assert.ArgumentNotNull(log, nameof(log));

            _log = log;
        }

        public void ReadData(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Reading CSV input data...", this);
            try
            {
                var reader = new StreamReader(args.FileStream);
                var insertLineCount = 0;
                var readLineCount = 0;
                do
                {
                    var line = reader.ReadLine();
                    readLineCount++;
                    if (line == null
                        || (readLineCount == 1 && args.ImportOptions.FirstRowAsColumnNames))
                    {
                        continue;
                    }

                    var row = args.ImportData.NewRow();
                    var values = line.Split(args.ImportOptions.CsvDelimiter, StringSplitOptions.None);
                    for (int j = 0; j < args.Map.InputFields.Count; j++)
                    {
                        if (j < values.Length)
                        {
                            row[j] = values[j];
                        }
                        else
                        {
                            row[j] = string.Empty;
                        }
                    }
                    args.ImportData.Rows.Add(row);
                    insertLineCount++;

                } while (!reader.EndOfStream);
                _log.Info($"EzImporter:{insertLineCount} records read from input data.", this);
            }
            catch (Exception ex)
            {
                _log.Error("EzImporter:" + ex.ToString(), this);
            }
        }


        public string[] GetColumnNames(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Reading column names from input CSV file...", this);
            try
            {
                using (var reader = new StreamReader(args.FileStream))
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        return line.Split(args.ImportOptions.CsvDelimiter, StringSplitOptions.None);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("EzImporter:" + ex.ToString(), this);
            }
            return Array.Empty<string>();
        }
    }
}