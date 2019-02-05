using EzImporter.Configuration;
using EzImporter.Models;
using EzImporter.Pipelines.ImportItems;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace EzImporter.Controllers
{
    [ServicesController]
    public class ImportController : ServicesApiController
    {
        private readonly BaseFactory _factory;
        private readonly BaseCorePipelineManager _pipelineManager;
        private readonly BaseLog _log;

        #region Constuctors
        public ImportController()
            : this(
                  ServiceLocator.ServiceProvider.GetRequiredService<BaseFactory>(),
                  ServiceLocator.ServiceProvider.GetRequiredService<BaseCorePipelineManager>(),
                  ServiceLocator.ServiceProvider.GetRequiredService<BaseLog>())
        { }

        public ImportController(BaseFactory factory, BaseCorePipelineManager pipelineManager, BaseLog log)
            : base()
        {
            Assert.ArgumentNotNull(factory, nameof(factory));
            Assert.ArgumentNotNull(pipelineManager, nameof(pipelineManager));
            Assert.ArgumentNotNull(log, nameof(log));

            _factory = factory;
            _pipelineManager = pipelineManager;
            _log = log;
        }

        #endregion

        [HttpPost]
        public IHttpActionResult Import(ImportModel importModel)
        {
            var database = _factory.GetDatabase(TextConstants.ContentDatabaseName);
            var languageItem = database.GetItem(importModel.Language);
            var uploadedFile = (MediaItem)database.GetItem(importModel.MediaItemId);
            if (uploadedFile == null)
            {
                return new JsonResult<ImportResultModel>(null, new JsonSerializerSettings(), Encoding.UTF8, this);
            }

            ImportResultModel result;
            try
            {
                var args = new ImportItemsArgs
                {
                    Database = database,
                    FileExtension = uploadedFile.Extension.ToLower(),
                    FileStream = uploadedFile.GetMediaStream(),
                    RootItemId = new ID(importModel.ImportLocationId),
                    TargetLanguage = Sitecore.Globalization.Language.Parse(languageItem.Name),
                    Map = Map.Factory.BuildMapInfo(new ID(importModel.MappingId)),
                    ImportOptions = new ImportOptions
                    {
                        CsvDelimiter = new[] {importModel.CsvDelimiter},
                        MultipleValuesImportSeparator = importModel.MultipleValuesSeparator,
                        TreePathValuesImportSeparator = @"\",
                        FirstRowAsColumnNames = importModel.FirstRowAsColumnNames
                    }
                };
                args.ImportOptions.ExistingItemHandling = (ExistingItemHandling)
                    Enum.Parse(typeof(ExistingItemHandling), importModel.ExistingItemHandling);
                args.ImportOptions.InvalidLinkHandling = (InvalidLinkHandling)
                    Enum.Parse(typeof(InvalidLinkHandling), importModel.InvalidLinkHandling);

                _log.Info(
                    $"EzImporter: mappingId:{importModel.MappingId} mediaItemId:{importModel.MediaItemId} firstRowAsColumnNames:{args.ImportOptions.FirstRowAsColumnNames}",
                    this);
                args.Timer.Start();
                _pipelineManager.Run(TextConstants.PipelineNames.ImportItems, args);
                args.Timer.Stop();
                if (args.Aborted)
                {
                    result = new ImportResultModel
                    {
                        HasError = true,
                        Log = args.Statistics.ToString(),
                        ErrorMessage = args.Message,
                        ErrorDetail = args.ErrorDetail
                    };
                }
                else
                {
                    result = new ImportResultModel
                    {
                        Log = $"{args.Statistics} Duration: {args.Timer.Elapsed:c}"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ImportResultModel
                {
                    HasError = true,
                    ErrorMessage = ex.Message,
                    ErrorDetail = ex.ToString()
                };
            }

            return new JsonResult<ImportResultModel>(result, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public IHttpActionResult DefaultSettings()
        {
            var options = EzImporter.Configuration.Factory.GetDefaultImportOptions();
            var model = new SettingsModel
            {
                CsvDelimiter = options.CsvDelimiter[0],
                ExistingItemHandling = options.ExistingItemHandling.ToString(),
                InvalidLinkHandling = options.InvalidLinkHandling.ToString(),
                MultipleValuesSeparator = options.MultipleValuesImportSeparator,
                FirstRowAsColumnNames = options.FirstRowAsColumnNames
            };
            return new JsonResult<SettingsModel>(model, new JsonSerializerSettings(), Encoding.UTF8, this);
        }
    }
}