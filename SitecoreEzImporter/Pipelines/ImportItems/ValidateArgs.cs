using Sitecore.Abstractions;
using Sitecore.Diagnostics;

namespace EzImporter.Pipelines.ImportItems
{
    public class ValidateArgs : ImportItemsProcessor
    {
        private readonly BaseLog _log;

        public ValidateArgs(BaseLog log)
        {
            Assert.ArgumentNotNull(log, nameof(log));

            _log = log;
        }

        public override void Process(ImportItemsArgs args)
        {
            _log.Info("EzImporter:Validating input...", this);
            if (args.FileStream == null)
            {
                _log.Error("EzImporter: Input file not found.", this);
                args.ErrorDetail = "FileStream = null";
                args.AbortPipeline();
            }            
        }
    }
}