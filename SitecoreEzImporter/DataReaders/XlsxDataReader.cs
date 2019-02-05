﻿using Excel;
using EzImporter.Pipelines.ImportItems;
using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using System;
using System.Data;
using System.Linq;

namespace EzImporter.DataReaders
{
    public class XlsxDataReader : IDataReader
    {
        private readonly BaseLog _log;

        public XlsxDataReader(BaseLog log)
        {
            Assert.ArgumentNotNull(log, nameof(log));

            _log = log;
        }

        public void ReadData(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Reading XSLX input data", this);
            try
            {
                IExcelDataReader excelReader = BuildExcelReader(args);

                excelReader.IsFirstRowAsColumnNames = args.ImportOptions.FirstRowAsColumnNames;
                if (!excelReader.IsValid)
                {
                    _log.Error($"EzImporter:Invalid Excel file '{excelReader.ExceptionMessage}'", this);
                    return;
                }
                DataSet result = excelReader.AsDataSet();
                if (result == null)
                {
                    _log.Error("EzImporter:No data could be retrieved from Excel file.", this);
                }
                if (result.Tables == null || result.Tables.Count == 0)
                {
                    _log.Error("EzImporter:No worksheets found in Excel file", this);
                    return;
                }
                var readDataTable = result.Tables[0];
                foreach (var readDataRow in readDataTable.AsEnumerable())
                {
                    var row = args.ImportData.NewRow();
                    for (int i = 0; i < args.Map.InputFields.Count; i++)
                    {
                        if (i < readDataTable.Columns.Count && readDataRow[i] != null)
                        {
                            row[i] = Convert.ToString(readDataRow[i]);
                        }
                        else
                        {
                            row[i] = "";
                        }
                    }
                    args.ImportData.Rows.Add(row);
                }
                _log.Info($"EzImporter:{readDataTable.Rows.Count} records read from input data.", this);
            }
            catch (Exception ex)
            {
                _log.Error("EzImporter:" + ex.ToString(), this);
            }
        }

        internal virtual IExcelDataReader BuildExcelReader(ImportItemsArgs args)
        {
            if (args.FileExtension == "xls")
            {
                // TODO: Do we need that in 2019 ?
                // Reading from a binary Excel file ('97-2003 format; *.xls)
                return ExcelReaderFactory.CreateBinaryReader(args.FileStream, ReadOption.Loose);
            }

            return ExcelReaderFactory.CreateOpenXmlReader(args.FileStream);
        }

        public string[] GetColumnNames(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Reading column names from input XSLX file...", this);
            try
            {
                
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(args.FileStream);

                excelReader.IsFirstRowAsColumnNames = true; //assume first line is data, so we can read it
                if (!excelReader.IsValid)
                {
                    _log.Info($"EzImporter:Invalid Excel file '{excelReader.ExceptionMessage}'", this);
                    return Array.Empty<string>();
                }
                DataSet result = excelReader.AsDataSet();
                if (result == null)
                {
                    _log.Info("EzImporter:No data could be retrieved from Excel file.", this);
                }
                if (result.Tables == null || result.Tables.Count == 0)
                {
                    _log.Info("EzImporter:No worksheets found in Excel file", this);
                    return Array.Empty<string>();
                }
                var readDataTable = result.Tables[0];
                return readDataTable.Columns
                    .Cast<DataColumn>()
                    .Select(c => c.ColumnName).ToArray();
            }
            catch (Exception ex)
            {
                _log.Error("EzImporter:" + ex.ToString(), ex, this);
            }
            return Array.Empty<string>();
        }
    }
}