using Generator.Processor.Integration.Tests.TestSupport;
using Generator.Processor.Models;
using Generator.Processor.Services;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Generator.Processor.Integration.Tests.XmlSchemaTests
{
    public class XmlServiceTests
    {
        private readonly IntegrationTestContainer _container;
        private const string ReferenceDataXmlFilename = @"XmlSchemaTests\ReferenceData.xml";
        private const string GenerationReportXmlFilename = @"XmlSchemaTests\GenerationReport.xml";
        private const string GeneratorOutputResultXmlFilename = @"XmlSchemaTests\GeneratorOutputReport.xml";

        public XmlServiceTests()
        {
            _container = new IntegrationTestContainer();
        }

        [Fact]
        public void LoadReferenceXmlFile_Deserialize_ReturnsReferenceModel()
        {
            var expectedReferenceData = new ReferenceData
            {
                Factors = new ReferenceFactors
                {
                    ValueFactor = new ReferenceFactor { High = 0.946, Medium = 0.696, Low = 0.265 },
                    EmissionsFactor = new ReferenceFactor { High = 0.812, Medium = 0.562, Low = 0.312 }
                }
            };
            var referenceXml = _container.FileHelper.ReadFileAsString(ReferenceDataXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeReferenceData(referenceXml);

            Assert.NotNull(result);
            Assert.Equal(expectedReferenceData.Factors.ValueFactor.High, result.Factors.ValueFactor.High);
            Assert.Equal(expectedReferenceData.Factors.ValueFactor.Medium, result.Factors.ValueFactor.Medium);
            Assert.Equal(expectedReferenceData.Factors.ValueFactor.Low, result.Factors.ValueFactor.Low);
            Assert.Equal(expectedReferenceData.Factors.EmissionsFactor.High, result.Factors.EmissionsFactor.High);
            Assert.Equal(expectedReferenceData.Factors.EmissionsFactor.Medium, result.Factors.EmissionsFactor.Medium);
            Assert.Equal(expectedReferenceData.Factors.EmissionsFactor.Low, result.Factors.EmissionsFactor.Low);
        }

        [Fact]
        public void LoadGenerationReportXmlFile_Deserialize_ReturnsGenerationReportModel()
        {
            var generationReportXml = _container.FileHelper.ReadFileAsString(GenerationReportXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeGenerationReportData(generationReportXml);

            Assert.NotNull(result);
            Assert.Equal(2, result.Wind.WindGenerators.Length);
            Assert.Single(result.Gas.GasGenerators);
            Assert.Single(result.Coal.CoalGenerators);
        }

        [Fact]
        public void LoadGenerationReportXmlFile_Deserialize_DeserialisesWindGeneratorModel()
        {
            var expectedWindData = new WindGenerator
            {
                Name = "Wind[Offshore]",
                Location = "Offshore"
            };
            var generationReportXml = _container.FileHelper.ReadFileAsString(GenerationReportXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeGenerationReportData(generationReportXml);

            Assert.NotNull(result);
            Assert.True(result.Wind.WindGenerators.Any());
            var windGenerator = result.Wind.WindGenerators.First(x => x.Name == expectedWindData.Name);
            Assert.Equal(expectedWindData.Location, windGenerator.Location);
            Assert.True(windGenerator.Generation.DailyGenerations.Any());
        }

        [Fact]
        public void LoadGenerationReportXmlFile_Deserialize_DeserialisesGasGeneratorModel()
        {
            var expectedGasData = new GasGenerator
            {
                Name = "Gas[1]",
                EmissionsRating = 0.038
            };
            var generationReportXml = _container.FileHelper.ReadFileAsString(GenerationReportXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeGenerationReportData(generationReportXml);

            Assert.NotNull(result);
            Assert.True(result.Gas.GasGenerators.Any());
            var gasGenerator = result.Gas.GasGenerators.First(x => x.Name == expectedGasData.Name);
            Assert.Equal(expectedGasData.EmissionsRating, gasGenerator.EmissionsRating);
            Assert.True(gasGenerator.Generation.DailyGenerations.Any());
        }

        [Fact]
        public void LoadGenerationReportXmlFile_Deserialize_DeserialisesCoalGeneratorModel()
        {
            var expectedCoalData = new CoalGenerator
            {
                Name = "Coal[1]",
                TotalHeatInput = 11.815,
                ActualNetGeneration = 11.815,
                EmissionsRating = 0.482
            };
            var generationReportXml = _container.FileHelper.ReadFileAsString(GenerationReportXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeGenerationReportData(generationReportXml);

            Assert.NotNull(result);
            Assert.True(result.Coal.CoalGenerators.Any());
            var coalGenerator = result.Coal.CoalGenerators.First(x => x.Name == expectedCoalData.Name);
            Assert.Equal(expectedCoalData.TotalHeatInput, coalGenerator.TotalHeatInput);
            Assert.Equal(expectedCoalData.ActualNetGeneration, coalGenerator.ActualNetGeneration);
            Assert.Equal(expectedCoalData.EmissionsRating, coalGenerator.EmissionsRating);
            Assert.True(coalGenerator.Generation.DailyGenerations.Any());
        }

        [Fact]
        public void LoadGenerationReportXmlFile_Deserialize_DeserialisesDailyGenerationModel()
        {
            const string windGeneratorName = "Wind[Offshore]";
            var expectedDailyGeneration = new DailyGeneration
            {
                Date = new DateTime(2017, 01, 01),
                Energy = 100.368,
                Price = 20.148
            };
            var generationReportXml = _container.FileHelper.ReadFileAsString(GenerationReportXmlFilename);
            var underTest = new XmlService();

            var result = underTest.DeserializeGenerationReportData(generationReportXml);

            Assert.NotNull(result);
            Assert.True(result.Wind.WindGenerators.Any());
            var windGenerator = result.Wind.WindGenerators.First(x => x.Name == windGeneratorName);
            Assert.NotNull(windGenerator.Generation);
            Assert.Equal(3, windGenerator.Generation.DailyGenerations.Length);
            var dailyGeneration = windGenerator.Generation.DailyGenerations.First(x => x.Date == expectedDailyGeneration.Date);
            Assert.Equal(expectedDailyGeneration.Energy, dailyGeneration.Energy);
            Assert.Equal(expectedDailyGeneration.Price, dailyGeneration.Price);
        }

        [Fact]
        public void SaveGeneratorOutputXmlFile_WithGeneratorOutputModel_DeserializesToString()
        {
            var generationOutput = new GenerationOutput
            {
                Totals = new GeneratorTotalDataSet
                {
                    GeneratorTotals = new GeneratorTotal[]
                    {
                        new GeneratorTotal { Name = "Coal[1]", Total = 5341.716526632},
                        new GeneratorTotal { Name = "Gas[1]", Total = 8512.254605520}
                    }
                },
                MaxEmissionGenerators = new DailyGeneratorEmissionDataSet
                {
                    DailyGeneratorEmissions = new DailyGeneratorEmission[]
                    {
                        new DailyGeneratorEmission { Name = "Coal[1]", Emission = 137.175004008, Date = new DateTime(2017,01,01) },
                        new DailyGeneratorEmission { Name = "Coal[1]", Emission = 136.440767624, Date = new DateTime(2017,01,02) }
                    }
                },
                ActualHeatRates = new ActualHeatRateDataSet
                {
                    GeneratorActualHeatRates = new ActualHeatRate[]
                    {
                        new ActualHeatRate { Name = "Coal[1]", HeatRate = 12.849293200 }
                    }
                }
            };
            var underTest = new XmlService();

            var result = underTest.SerializeGenerationOutputData(generationOutput);

            Assert.NotNull(result);
            var resultXml = Encoding.UTF8.GetString(result);
            _container.FileHelper.WriteStringAsFile(GeneratorOutputResultXmlFilename, resultXml, Encoding.UTF8);
        }

    }
}
