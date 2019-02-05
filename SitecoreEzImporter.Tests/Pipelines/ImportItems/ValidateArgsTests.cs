using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using EzImporter.Pipelines.ImportItems;
using EzImporter.Tests;
using FluentAssertions;
using Sitecore.Abstractions;
using Xunit;

namespace SitecoreEzImporter.Tests.Pipelines.ImportItems
{
    public class ValidateArgsTests
    {
        [Theory, AutoNSubstituteData]
        public void Process_WhenNoStream_AbortsPipeline(BaseLog log)
        {
            // Arrange
            var validateArgs = new ValidateArgs(log);
            var args = new ImportItemsArgs { FileStream = null };

            // Act
            validateArgs.Process(args);

            // Assert
            args.Aborted.Should().BeTrue();       
        }
    }
}
