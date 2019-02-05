using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace EzImporter.Configuration
{
    /// <summary>
    /// Registers services neeeded for Importer execution.
    /// <para>Does not override/touch any default service, simply adds own <see cref="ImportOptionsFactory"/>.</para>
    /// </summary>
    public class EzImporterServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ImportOptionsFactory, DefaultImportOptionsFactory>();
        }
    }
}