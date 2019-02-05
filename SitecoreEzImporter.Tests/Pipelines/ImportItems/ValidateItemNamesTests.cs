using AutoFixture.Xunit2;
using EzImporter.Map;
using EzImporter.Pipelines.ImportItems;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace SitecoreEzImporter.Tests.Pipelines.ImportItems
{
    public class ValidateItemNamesTests
    {
        [Theory, AutoData]
        public void ValidateItemName_WhenValidNameUnchanged_LeavesErrorsEmpty(string validItemName)
        {
            // Arrange
            ValidateItemNames validateItemNames = new ValidateItemNamesEx(validItemName);
            var importItem = new ItemDto(validItemName);

            // Act
            validateItemNames.ValidateName(importItem);

            // Assert
            validateItemNames.Errors.Should().BeEmpty(because: "no corrections to item name was done");
        }

        [Fact]
        public void ValidateItemName_WhenEmptyItemName_SetsError()
        {
            // Arrange
            ValidateItemNames validateItemNames = new ValidateItemNames();
            var importItem = new ItemDto(name: string.Empty);

            // Act
            validateItemNames.ValidateName(importItem);

            // Assert
            validateItemNames.Errors.Should().ContainSingle(because: "item without name met and is recognized as wrong");
        }

        [Theory, AutoData]
        public void ValidateItemName_WhenItemNameChanged_PopulatesErrors(string invalidItemName, string suggestedItemName)
        {
            // Arrange
            var corrections = new Dictionary<string, string>{ { invalidItemName, suggestedItemName } };
            ValidateItemNames validateItemNames = new ValidateItemNamesEx(corrections);
            var importItem = new ItemDto(invalidItemName);

            // Act
            validateItemNames.ValidateName(importItem);

            // Assert
            validateItemNames.Errors.Should().ContainSingle(because: "Item name was corrected as initially considered to be wrong");
        }

        [Theory, AutoData]
        public void Process_WhenImportItemHasInvalidName_AbortsPipeline(string invalidItemName, string suggestedItemName)
        {
            // Arrange
            var corrections = new Dictionary<string, string> { { invalidItemName, suggestedItemName } };
            ValidateItemNames validateItemNames = new ValidateItemNamesEx(corrections);
            var importItemArgs = new ImportItemsArgs();
            importItemArgs.ImportItems.Add(new ItemDto(invalidItemName));

            // Act
            validateItemNames.Process(importItemArgs);

            // Assert
            importItemArgs.Aborted.Should().BeTrue();
        }


        public class ValidateItemNamesEx : ValidateItemNames
        {
            private readonly IDictionary<string, string> _initialProsedPairs;

            public ValidateItemNamesEx(string correctName) : this(new Dictionary<string, string> { { correctName, correctName } })
            { }

            public ValidateItemNamesEx(IDictionary<string, string> initialProsedPairs)
            {
                _initialProsedPairs = initialProsedPairs;
            }

            internal override string ProposeValidItemName(ItemDto item) => _initialProsedPairs[item.Name];

        }

        //[Theory]
        //[InlineData("'")]
        //[InlineData(@"\")]
        //[InlineData("/")]
        //[InlineData("?")]
        //[InlineData("£")]
        //[InlineData("#")]
        //public void ValidateItemNames_Failure(string value)
        //{
        //    var validateItemNamesProcessor = new ValidateItemNames();
        //    var importItem = new ImportItem(value);
        //    validateItemNamesProcessor.ValidateName(importItem);
        //    Assert.Single(validateItemNamesProcessor.Errors);
        //    Assert.Single(validateItemNamesProcessor.Notifications);
        //}
    }
}
