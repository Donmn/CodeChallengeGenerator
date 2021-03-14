using Generator.Processor.Integration.Tests.TestSupport;
using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Generator.Processor.Integration.Tests.EndToEnd
{
    public class EndToEndTests
    {
        private readonly IntegrationTestContainer _container;
        private const string IntegrationTestSourceFolder = @"\EndToEnd";
        private const string IntegrationTestInputFolder = @"\EndToEnd\CurrentRun";
        private const string ReferenceDataXmlFilename = @"IntegrationTest_EndToEnd_ReferenceData.xml";
        private const string GenerationReportXmlFilename = @"IntegrationTest_EndToEnd_GenerationReport.xml";
        private const string GenerationOutputXmlFilename = @"IntegrationTest_EndToEnd_GenerationOutput.xml";

        public EndToEndTests()
        {
            _container = new IntegrationTestContainer();
        }

        [Fact]
        public void EndToEndTest_WriteReport_Execute_CreatesOutput()
        {
            //Expected Values based on the GenerationReport input
            var expectedGeneratorTotal = new GeneratorTotal { Name = "Coal[1]", Total = 4443 };
            var expectedDailyGeneratorEmission = new DailyGeneratorEmission { Name = "Coal[1]", Emission = 140, Date = new DateTime(2017, 01, 01) };
            var expectedActualHeatRate = new ActualHeatRate { Name = "Coal[1]", HeatRate = 0.8333333333333334 };

            //Prepare for run: Folder, Config and report file
            _container.FileHelper.CreateFolder(IntegrationTestInputFolder);
            var config = CreateConfigForRun();
            _container.FileHelper.CopyFile(Path.Join(IntegrationTestSourceFolder, GenerationReportXmlFilename), Path.Join(IntegrationTestInputFolder, GenerationReportXmlFilename));

            //Process the Report
            ProcessReport(config);

            //Load the output and verify
            var generationOutputPath = Path.Join(IntegrationTestInputFolder, GenerationOutputXmlFilename);
            var generationOutputXml = _container.FileHelper.ReadFileAsString(generationOutputPath);
            var result = _container.XmlService.DeserializeData<GenerationOutput>(generationOutputXml);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals);
            Assert.True(result.Totals.GeneratorTotals.Any());
            Assert.Equal(4, result.Totals.GeneratorTotals.Length);
            var generatorTotal = result.Totals.GeneratorTotals.First(x => x.Name == expectedGeneratorTotal.Name);
            Assert.Equal(expectedGeneratorTotal.Total, generatorTotal.Total);

            Assert.NotNull(result.MaxEmissionGenerators);
            Assert.True(result.MaxEmissionGenerators.DailyGeneratorEmissions.Any());
            Assert.Equal(3, result.MaxEmissionGenerators.DailyGeneratorEmissions.Length);
            var dailyGeneratorEmission = result.MaxEmissionGenerators.DailyGeneratorEmissions.First(x => x.Name == expectedDailyGeneratorEmission.Name && x.Date == expectedDailyGeneratorEmission.Date);
            Assert.Equal(expectedDailyGeneratorEmission.Emission, dailyGeneratorEmission.Emission);

            Assert.NotNull(result.ActualHeatRates);
            Assert.True(result.ActualHeatRates.GeneratorActualHeatRates.Any());
            Assert.Single(result.ActualHeatRates.GeneratorActualHeatRates);
            var generatorActualHeatRate = result.ActualHeatRates.GeneratorActualHeatRates.First(x => x.Name == expectedActualHeatRate.Name);
            Assert.Equal(expectedActualHeatRate.HeatRate, generatorActualHeatRate.HeatRate);

        }

        private void ProcessReport(IConfigWrapper config)
        {
            var reportingServiceFactory = new ReportingServiceFactory();
            var dataManager = reportingServiceFactory.CreateDataManager();
            var referenceData = dataManager.LoadReferenceDataFromFile(config.ReferenceDataFolder, config.ReferenceDataFilename);
            var referenceDataHelper = reportingServiceFactory.CreateReferenceDataHelper(referenceData);
            var generationReportProcessor = reportingServiceFactory.CreateGenerationReportProcessor(referenceDataHelper);
            var generationReport = dataManager.LoadGenerationReportFromFile(config.GenerationReportFolder, GenerationReportXmlFilename);
            var generationOutput = generationReportProcessor.Process(generationReport);
            dataManager.SaveGenerationOutputToFile(generationOutput, config.GenerationOutputFolder, config.GenerationOutputFilename);
            dataManager.MoveFileToArchiveWithTimestamp(config.GenerationReportFolder, GenerationReportXmlFilename, config.GenerationReportArchive);
        }

        private IConfigWrapper CreateConfigForRun()
        {
            var configValues = new Dictionary<string, string> {
                { ConfigKeys.GenerationReportFolder.Key, _container.FileHelper.JoinRelativePathToRunPath(IntegrationTestInputFolder) },
                { ConfigKeys.GenerationReportFilenameFilter.Key, GenerationReportXmlFilename },
                { ConfigKeys.GenerationOutputFolder.Key, _container.FileHelper.JoinRelativePathToRunPath(IntegrationTestInputFolder) },
                { ConfigKeys.GenerationOutputFilename.Key, GenerationOutputXmlFilename },
                { ConfigKeys.ReferenceDataFolder.Key, _container.FileHelper.JoinRelativePathToRunPath(IntegrationTestSourceFolder) },
                { ConfigKeys.ReferenceDataFilename.Key, ReferenceDataXmlFilename },
                { ConfigKeys.GenerationReportArchive.Key, _container.FileHelper.JoinRelativePathToRunPath(IntegrationTestInputFolder) }
            };

            var result = _container.CreateConfig(configValues);
            return result;
        }
    }
}
