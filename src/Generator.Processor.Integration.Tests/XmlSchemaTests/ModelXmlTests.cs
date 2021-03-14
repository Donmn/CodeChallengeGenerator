using Generator.Processor.Integration.Tests.TestSupport;
using Generator.Processor.Models;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Generator.Processor.Integration.Tests.XmlSchemaTests
{
    public class ModelXmlTests
    {
        private readonly IntegrationTestContainer _container;
        private const string GenerationOutputXmlFilename = @"XmlSchemaTests\GenerationOutput.xml";
        private const string ReferenceDataResultXmlFilename = @"XmlSchemaTests\ReferenceDataResult.xml";
        private const string WindGeneratorReportXmlFilename = @"XmlSchemaTests\WindGeneratorReport.xml";
        private const string GasGeneratorReportXmlFilename = @"XmlSchemaTests\GasGeneratorReport.xml";
        private const string CoalGeneratorReportXmlFilename = @"XmlSchemaTests\CoalGeneratorReport.xml";

        public ModelXmlTests()
        {
            _container = new IntegrationTestContainer();
        }

        [Fact]
        public void LoadGenerationOuputXmlFile_Deserialize_DeserialisesGenerationOutputModel()
        {
            var expectedGeneratorTotal = new GeneratorTotal { Name = "Coal[1]", Total = 5341.716526632 };
            var expectedDailyGeneratorEmission = new DailyGeneratorEmission { Name = "Coal[1]", Emission = 137.175004008, Date = new DateTime(2017, 01, 01) };
            var expectedActualHeatRate = new ActualHeatRate { Name = "Coal[1]", HeatRate = 12.849293200 };

            var expectedGenerationOutput = new GenerationOutput
            {
                Totals = new GeneratorTotalDataSet
                {
                    GeneratorTotals = new GeneratorTotal[] { expectedGeneratorTotal }
                },
                MaxEmissionGenerators = new DailyGeneratorEmissionDataSet
                {
                    DailyGeneratorEmissions = new DailyGeneratorEmission[] { expectedDailyGeneratorEmission }
                },
                ActualHeatRates = new ActualHeatRateDataSet
                {
                    GeneratorActualHeatRates = new ActualHeatRate[] { expectedActualHeatRate }
                }
            };
            var generationOutputXml = _container.FileHelper.ReadFileAsString(GenerationOutputXmlFilename);

            var result = _container.XmlService.DeserializeData<GenerationOutput>(generationOutputXml);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals);
            Assert.True(result.Totals.GeneratorTotals.Any());
            var generatorTotal = result.Totals.GeneratorTotals.First(x => x.Name == expectedGeneratorTotal.Name);
            Assert.Equal(expectedGeneratorTotal.Total, generatorTotal.Total);

            Assert.NotNull(result.MaxEmissionGenerators);
            Assert.True(result.MaxEmissionGenerators.DailyGeneratorEmissions.Any());
            var dailyGeneratorEmission = result.MaxEmissionGenerators.DailyGeneratorEmissions.First(x => x.Name == expectedDailyGeneratorEmission.Name && x.Date == expectedDailyGeneratorEmission.Date);
            Assert.Equal(expectedDailyGeneratorEmission.Emission, dailyGeneratorEmission.Emission);

            Assert.NotNull(result.ActualHeatRates);
            Assert.True(result.ActualHeatRates.GeneratorActualHeatRates.Any());
            var generatorActualHeatRate = result.ActualHeatRates.GeneratorActualHeatRates.First(x => x.Name == expectedActualHeatRate.Name);
            Assert.Equal(expectedActualHeatRate.HeatRate, generatorActualHeatRate.HeatRate);
        }


        [Fact]
        public void SaveReferenceXmlFile_WithReferenceModel_DeserializesToString()
        {
            var referenceData = new ReferenceData
            {
                Factors = new ReferenceFactors
                {
                    ValueFactor = new ReferenceFactor { High = 0.946, Medium = 0.696, Low = 0.265 },
                    EmissionsFactor = new ReferenceFactor { High = 0.812, Medium = 0.562, Low = 0.312 }
                }
            };

            var result = _container.XmlService.SerializeDataUtf8(referenceData);

            Assert.NotNull(result);
            var resultXml = Encoding.UTF8.GetString(result);
            _container.FileHelper.WriteStringAsFile(ReferenceDataResultXmlFilename, resultXml, Encoding.UTF8);
        }

        [Fact]
        public void SaveGeneratorReportXmlFile_WithWindGeneratorModel_DeserializesToString()
        {
            var generationReport = new GenerationReport
            {
                Wind = new WindDataSet
                {
                    WindGenerators = new WindGenerator[] {
                        new WindGenerator
                        {
                            Name = "Wind[Offshore]",
                            Location = "Offshore",
                            Generation = new Generation
                            {
                                DailyGenerations = new DailyGeneration []
                                {
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,01),
                                        Energy = 100.368,
                                        Price = 20.148
                                    },
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,02),
                                        Energy = 90.843,
                                        Price = 25.516
                                    }
                                }
                            }
                        },
                        new WindGenerator {
                            Name = "Wind[Onshore]",
                            Location = "Onshore",
                            Generation = new Generation
                            {
                                DailyGenerations = new DailyGeneration []
                                {
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,01),
                                        Energy = 56.578,
                                        Price = 29.542
                                    },
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,02),
                                        Energy = 48.540,
                                        Price = 22.954
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var result = _container.XmlService.SerializeDataUtf8(generationReport);

            Assert.NotNull(result);
            var resultXml = Encoding.UTF8.GetString(result);
            _container.FileHelper.WriteStringAsFile(WindGeneratorReportXmlFilename, resultXml, Encoding.UTF8);
        }

        [Fact]
        public void SaveGeneratorReportXmlFile_WithGasGeneratorModel_DeserializesToString()
        {
            var generationReport = new GenerationReport
            {
                Gas = new GasDataSet
                {
                    GasGenerators = new GasGenerator[] {
                        new GasGenerator
                        {
                            Name = "Gas[1]",
                            EmissionsRating = 0.038,
                            Generation = new Generation
                            {
                                DailyGenerations = new DailyGeneration []
                                {
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,01),
                                        Energy = 259.235,
                                        Price = 15.837
                                    },
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,02),
                                        Energy = 235.975,
                                        Price = 16.556
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var result = _container.XmlService.SerializeDataUtf8(generationReport);

            Assert.NotNull(result);
            var resultXml = Encoding.UTF8.GetString(result);
            _container.FileHelper.WriteStringAsFile(GasGeneratorReportXmlFilename, resultXml, Encoding.UTF8);
        }

        [Fact]
        public void SaveGeneratorReportXmlFile_WithCoalGeneratorModel_DeserializesToString()
        {
            var generationReport = new GenerationReport
            {
                Coal = new CoalDataSet
                {
                    CoalGenerators = new CoalGenerator[] {
                        new CoalGenerator
                        {
                            Name = "Coal[1]",
                            EmissionsRating = 0.038,
                            TotalHeatInput = 11.815,
                            ActualNetGeneration = 11.815,
                            Generation = new Generation
                            {
                                DailyGenerations = new DailyGeneration []
                                {
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,01),
                                        Energy = 350.487,
                                        Price = 10.146
                                    },
                                    new DailyGeneration {
                                        Date = new DateTime(2017,01,02),
                                        Energy = 348.611,
                                        Price = 11.815
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var result = _container.XmlService.SerializeDataUtf8(generationReport);

            Assert.NotNull(result);
            var resultXml = Encoding.UTF8.GetString(result);
            _container.FileHelper.WriteStringAsFile(CoalGeneratorReportXmlFilename, resultXml, Encoding.UTF8);
        }

    }
}
