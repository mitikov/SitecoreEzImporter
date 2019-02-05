using Sitecore.Abstractions;
using Sitecore.Diagnostics;

namespace EzImporter.Pipelines.ImportItems
{
    /// <summary>
    /// Create named columns in <see cref="ImportItemsArgs.ImportItems"/> table based on <see cref="ImportItemsArgs.Map"/> input field names.
    /// <para>Created columns are text typed.</para>
    /// </summary>
    public class ReadMapInfo : ImportItemsProcessor
    {
        private readonly BaseLog _log;

        public ReadMapInfo(BaseLog log)
        {
            Assert.ArgumentNotNull(log, nameof(log));

            _log = log;
        }

        public override void Process(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Processing import map...", this);
            args.ImportData.Columns.Clear();
            foreach (var column in args.Map.InputFields)
            {
                args.ImportData.Columns.Add(column.Name, typeof(string));
            }
            _log.Info($"EzImporter:{args.Map.InputFields.Count} Columns defined in map.", this);
        }
    }
}