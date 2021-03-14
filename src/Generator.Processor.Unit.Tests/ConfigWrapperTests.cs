using Generator.Processor.Models.ReferenceTypes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace Generator.Processor.Unit.Tests
{
    public class ConfigWrapperTests
    {
        private readonly IConfiguration _config;
        private const string GenerationReportFolder = "generationReportFolder";
        private const string GenerationReportFilenameFilter = "generationReportFilenameFilter";
        private const string GenerationOutputFolder = "generationOutputFolder";
        private const string GenerationOutputFilename = "generationOutputFilename";
        private const string ReferenceDataFolder = "referenceDataFolder";
        private const string ReferenceDataFilename = "referenceDataFilename";
        private const string GenerationReportArchive = "generationReportArchive";

        public ConfigWrapperTests()
        {
            var configValues = new Dictionary<string, string> {
                { ConfigKeys.GenerationReportFolder.Key, GenerationReportFolder },
                { ConfigKeys.GenerationReportFilenameFilter.Key, GenerationReportFilenameFilter },
                { ConfigKeys.GenerationOutputFolder.Key, GenerationOutputFolder },
                { ConfigKeys.GenerationOutputFilename.Key, GenerationOutputFilename },
                { ConfigKeys.ReferenceDataFolder.Key, ReferenceDataFolder },
                { ConfigKeys.ReferenceDataFilename.Key, ReferenceDataFilename },
                { ConfigKeys.GenerationReportArchive.Key, GenerationReportArchive }
            };

            _config = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();
        }

        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var underTest = new ConfigWrapper(_config);
            Assert.NotNull(underTest);
        }

        [Fact]
        public void ConfigWrapper_WithValues_ReturnsExpected()
        {
            var underTest = new ConfigWrapper(_config);

            Assert.Equal(GenerationReportFolder, underTest.GenerationReportFolder);
            Assert.Equal(GenerationReportFilenameFilter, underTest.GenerationReportFilenameFilter);
            Assert.Equal(GenerationOutputFolder, underTest.GenerationOutputFolder);
            Assert.Equal(GenerationOutputFilename, underTest.GenerationOutputFilename);
            Assert.Equal(ReferenceDataFolder, underTest.ReferenceDataFolder);
            Assert.Equal(ReferenceDataFilename, underTest.ReferenceDataFilename);
            Assert.Equal(GenerationReportArchive, underTest.GenerationReportArchive);
        }

        [Fact]
        public void ConfigWrapper_WhenValueIsMandatoryAndNull_Throws()
        {
            var emptyConfigValues = new Dictionary<string, string> { };
            var emptyConfig = new ConfigurationBuilder()
            .AddInMemoryCollection(emptyConfigValues)
            .Build();

            var underTest = new ConfigWrapper(emptyConfig);

            Assert.Throws<ArgumentNullException>(() => underTest.GenerationReportFolder);
            Assert.Throws<ArgumentNullException>(() => underTest.GenerationReportFilenameFilter);
            Assert.Throws<ArgumentNullException>(() => underTest.GenerationOutputFolder);
            Assert.Throws<ArgumentNullException>(() => underTest.GenerationOutputFilename);
            Assert.Throws<ArgumentNullException>(() => underTest.ReferenceDataFolder);
            Assert.Throws<ArgumentNullException>(() => underTest.ReferenceDataFilename);
            Assert.Throws<ArgumentNullException>(() => underTest.GenerationReportArchive);
        }

        [Fact]
        public void ConfigWrapper_WhenValueIsMandatoryAndNull_ThrowsReturningKeyAndMandatory()
        {
            var emptyConfigValues = new Dictionary<string, string> { };
            var emptyConfig = new ConfigurationBuilder()
            .AddInMemoryCollection(emptyConfigValues)
            .Build();

            var underTest = new ConfigWrapper(emptyConfig);

            var ex = Assert.Throws<ArgumentNullException>(() => underTest.GenerationReportFolder);
            Assert.Contains(ConfigKeys.GenerationReportFolder.Key, ex.Message);
            Assert.Contains("mandatory", ex.Message);
        }
    }
}
