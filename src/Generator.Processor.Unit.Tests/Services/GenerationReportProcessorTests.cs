using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class GenerationReportProcessorTests
    {
        private readonly GenerationReportProcessor _underTest;
        private readonly Mock<ITotalGenerationCalculator> _totalGenerationCalculator;
        private readonly Mock<IEnergyEmissionCalculator> _energyEmissionCalculator;
        private readonly Mock<IActualHeatRateCalculator> _actualHeatRateCalculator;
        private readonly Mock<IReferenceDataHelper> _referenceDataHelper;

        public GenerationReportProcessorTests()
        {
            _totalGenerationCalculator = new Mock<ITotalGenerationCalculator>();
            _energyEmissionCalculator = new Mock<IEnergyEmissionCalculator>();
            _actualHeatRateCalculator = new Mock<IActualHeatRateCalculator>();
            _referenceDataHelper = new Mock<IReferenceDataHelper>();
            _underTest = new GenerationReportProcessor(_totalGenerationCalculator.Object, _energyEmissionCalculator.Object, _actualHeatRateCalculator.Object, _referenceDataHelper.Object);
        }

        [Fact]
        public void Ctr_DoesNotThrow()
        {
            var underTest = new GenerationReportProcessor(_totalGenerationCalculator.Object, _energyEmissionCalculator.Object, _actualHeatRateCalculator.Object, _referenceDataHelper.Object);

            Assert.NotNull(underTest);
        }

        [Fact]
        public void Ctr_WhenParametersAreNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new GenerationReportProcessor(null, _energyEmissionCalculator.Object, _actualHeatRateCalculator.Object, _referenceDataHelper.Object));
            Assert.Throws<ArgumentNullException>(() => new GenerationReportProcessor(_totalGenerationCalculator.Object, null, _actualHeatRateCalculator.Object, _referenceDataHelper.Object));
            Assert.Throws<ArgumentNullException>(() => new GenerationReportProcessor(_totalGenerationCalculator.Object, _energyEmissionCalculator.Object, null, _referenceDataHelper.Object));
            Assert.Throws<ArgumentNullException>(() => new GenerationReportProcessor(_totalGenerationCalculator.Object, _energyEmissionCalculator.Object, _actualHeatRateCalculator.Object, null));
        }

        [Fact]
        public void Process_WithGenerationReport_ReturnsGenerationOutput()
        {
            var generationReport = new GenerationReport();

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            Assert.IsType<GenerationOutput>(result);
        }

        [Fact]
        public void Process_WithGenerationReport_AndNoGenerators_DoesNotThrow()
        {
            var generationReport = new GenerationReport();

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals);
            Assert.NotNull(result.Totals.GeneratorTotals);
            Assert.Empty(result.Totals.GeneratorTotals);

            Assert.NotNull(result.MaxEmissionGenerators);
            Assert.NotNull(result.MaxEmissionGenerators.DailyGeneratorEmissions);
            Assert.Empty(result.MaxEmissionGenerators.DailyGeneratorEmissions);

            Assert.NotNull(result.ActualHeatRates);
            Assert.NotNull(result.ActualHeatRates.GeneratorActualHeatRates);
            Assert.Empty(result.ActualHeatRates.GeneratorActualHeatRates);
        }


        [Fact]
        public void Process_WithGenerationReportAndWindGenerators_SetsTotalWindEnergyForEach()
        {
            var generationReport = new GenerationReport
            {
                Wind = new WindDataSet
                {
                    WindGenerators = new WindGenerator[] {
                        new WindGenerator { Name = "Wind1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new WindGenerator { Name = "Wind2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals.GeneratorTotals);
            Assert.Equal(2, result.Totals.GeneratorTotals.Length);
            Assert.Equal("Wind1", result.Totals.GeneratorTotals[0].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[0].Total);
            Assert.Equal("Wind2", result.Totals.GeneratorTotals[1].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[1].Total);
        }

        [Fact]
        public void Process_WithGenerationReportAndWindGenerators_GetsGeneratorTypeAndCalculatesTotalEnergy()
        {
            var generationReport = new GenerationReport
            {
                Wind = new WindDataSet
                {
                    WindGenerators = new WindGenerator[] {
                        new WindGenerator { Name = "Wind1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new WindGenerator { Name = "Wind2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);
            _referenceDataHelper.Setup(x => x.GetGeneratorTypeForWindGeneratorFromLocation(It.IsAny<WindGenerator>())).Returns(GeneratorTypes.OffShoreWind);
            _referenceDataHelper.Setup(x => x.GetValueFactorForGenerator(It.Is<GeneratorType>(g => g == GeneratorTypes.OffShoreWind))).Returns(0.101);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _referenceDataHelper.Verify(x => x.GetGeneratorTypeForWindGeneratorFromLocation(It.IsAny<WindGenerator>()), Times.Exactly(2));
            _referenceDataHelper.Verify(x => x.GetValueFactorForGenerator(GeneratorTypes.OffShoreWind), Times.Exactly(2));
            _totalGenerationCalculator.Verify(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), 0.101), Times.Exactly(2));
        }

        [Fact]
        public void Process_WithGenerationReportAndGasGenerators_SetsTotalGasEnergyForEach()
        {
            var generationReport = new GenerationReport
            {
                Gas = new GasDataSet
                {
                    GasGenerators = new GasGenerator[] {
                        new GasGenerator { Name = "Gas1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new GasGenerator { Name = "Gas2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals.GeneratorTotals);
            Assert.Equal(2, result.Totals.GeneratorTotals.Length);
            Assert.Equal("Gas1", result.Totals.GeneratorTotals[0].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[0].Total);
            Assert.Equal("Gas2", result.Totals.GeneratorTotals[1].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[1].Total);
        }

        [Fact]
        public void Process_WithGenerationReportAndGasGenerators_GetsValueFactorForGeneratorTypeAndCalculatesTotalEnergy()
        {
            var generationReport = new GenerationReport
            {
                Gas = new GasDataSet
                {
                    GasGenerators = new GasGenerator[] {
                        new GasGenerator { Name = "Gas1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new GasGenerator { Name = "Gas2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);
            _referenceDataHelper.Setup(x => x.GetValueFactorForGenerator(It.Is<GeneratorType>(g => g == GeneratorTypes.Gas))).Returns(0.202);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _referenceDataHelper.Verify(x => x.GetGeneratorTypeForWindGeneratorFromLocation(It.IsAny<WindGenerator>()), Times.Never);
            _referenceDataHelper.Verify(x => x.GetValueFactorForGenerator(GeneratorTypes.Gas), Times.Exactly(2));
            _totalGenerationCalculator.Verify(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), 0.202), Times.Exactly(2));
        }

        [Fact]
        public void Process_WithGenerationReportAndCoalGenerators_SetsTotalCoalEnergyForEach()
        {
            var generationReport = new GenerationReport
            {
                Coal = new CoalDataSet
                {
                    CoalGenerators = new CoalGenerator[] {
                        new CoalGenerator { Name = "Coal1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new CoalGenerator { Name = "Coal2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            Assert.NotNull(result.Totals.GeneratorTotals);
            Assert.Equal(2, result.Totals.GeneratorTotals.Length);
            Assert.Equal("Coal1", result.Totals.GeneratorTotals[0].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[0].Total);
            Assert.Equal("Coal2", result.Totals.GeneratorTotals[1].Name);
            Assert.Equal(5, result.Totals.GeneratorTotals[1].Total);
        }

        [Fact]
        public void Process_WithGenerationReportAndCoalGenerators_GetsValueFactorForGeneratorTypeAndCalculatesTotalEnergy()
        {
            var generationReport = new GenerationReport
            {
                Coal = new CoalDataSet
                {
                    CoalGenerators = new CoalGenerator[] {
                        new CoalGenerator { Name = "Coal1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new CoalGenerator { Name = "Coal2", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _totalGenerationCalculator.Setup(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), It.IsAny<double>())).Returns(5);
            _referenceDataHelper.Setup(x => x.GetValueFactorForGenerator(It.Is<GeneratorType>(g => g == GeneratorTypes.Coal))).Returns(0.303);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _referenceDataHelper.Verify(x => x.GetGeneratorTypeForWindGeneratorFromLocation(It.IsAny<WindGenerator>()), Times.Never);
            _referenceDataHelper.Verify(x => x.GetValueFactorForGenerator(GeneratorTypes.Coal), Times.Exactly(2));
            _totalGenerationCalculator.Verify(x => x.SumTotalEnergy(It.IsAny<DailyGeneration[]>(), 0.303), Times.Exactly(2));
        }

        [Fact]
        public void Process_WithGenerationReportAndWindGenerators_DoesNotProcessDailyEmissions()
        {
            var generationReport = new GenerationReport
            {
                Wind = new WindDataSet
                {
                    WindGenerators = new WindGenerator[] {
                        new WindGenerator { Name = "Wind1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                    }
                }
            };

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _energyEmissionCalculator.Verify(x => x.ProcessMaxGeneratorDailyEmissions(It.IsAny<BaseGenerator>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<List<DailyGeneratorEmission>>()), Times.Never);
        }


        [Fact]
        public void Process_WithGenerationReportAndGasGenerators_GetsEmissionFactorForGeneratorTypeAndCalculatesEmissions()
        {
            var generationReport = new GenerationReport
            {
                Gas = new GasDataSet
                {
                    GasGenerators = new GasGenerator[] {
                        new GasGenerator { Name = "Gas1", EmissionsRating = 5, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new GasGenerator { Name = "Gas2", EmissionsRating = 6, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _referenceDataHelper.Setup(x => x.GetEmissionsFactorForGenerator(It.Is<GeneratorType>(g => g == GeneratorTypes.Gas))).Returns(0.404);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _referenceDataHelper.Verify(x => x.GetEmissionsFactorForGenerator(GeneratorTypes.Gas), Times.Exactly(2));
            _energyEmissionCalculator.Verify(x => x.ProcessMaxGeneratorDailyEmissions(It.Is<BaseGenerator>(g => g.Name == "Gas1"), 5, 0.404, It.IsAny<List<DailyGeneratorEmission>>()), Times.Once);
            _energyEmissionCalculator.Verify(x => x.ProcessMaxGeneratorDailyEmissions(It.Is<BaseGenerator>(g => g.Name == "Gas2"), 6, 0.404, It.IsAny<List<DailyGeneratorEmission>>()), Times.Once);
        }

        [Fact]
        public void Process_WithGenerationReportAndCoalGenerators_GetsEmissionFactorForGeneratorTypeAndCalculatesEmissions()
        {
            var generationReport = new GenerationReport
            {
                Coal = new CoalDataSet
                {
                    CoalGenerators = new CoalGenerator[] {
                        new CoalGenerator { Name = "Coal1", EmissionsRating = 7, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new CoalGenerator { Name = "Coal2", EmissionsRating = 8, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };
            _referenceDataHelper.Setup(x => x.GetEmissionsFactorForGenerator(It.Is<GeneratorType>(g => g == GeneratorTypes.Coal))).Returns(0.505);

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _referenceDataHelper.Verify(x => x.GetEmissionsFactorForGenerator(GeneratorTypes.Coal), Times.Exactly(2));
            _energyEmissionCalculator.Verify(x => x.ProcessMaxGeneratorDailyEmissions(It.Is<BaseGenerator>(g => g.Name == "Coal1"), 7, 0.505, It.IsAny<List<DailyGeneratorEmission>>()), Times.Once);
            _energyEmissionCalculator.Verify(x => x.ProcessMaxGeneratorDailyEmissions(It.Is<BaseGenerator>(g => g.Name == "Coal2"), 8, 0.505, It.IsAny<List<DailyGeneratorEmission>>()), Times.Once);
        }

        [Fact]
        public void Process_WithGenerationReportAndWindAndGasGenerators_DoesNotProcessActualHeatRates()
        {
            var generationReport = new GenerationReport
            {
                Wind = new WindDataSet
                {
                    WindGenerators = new WindGenerator[] {
                        new WindGenerator { Name = "Wind1", Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                    }
                },
                Gas = new GasDataSet
                {
                    GasGenerators = new GasGenerator[] {
                        new GasGenerator { Name = "Gas1", EmissionsRating = 5, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                    }
                }
            };

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _actualHeatRateCalculator.Verify(x => x.Calculate(It.IsAny<double>(), It.IsAny<double>()), Times.Never);
        }

        [Fact]
        public void Process_WithGenerationReportAndCoalGenerators_CalculatesActualHeatRates()
        {
            var generationReport = new GenerationReport
            {
                Coal = new CoalDataSet
                {
                    CoalGenerators = new CoalGenerator[] {
                        new CoalGenerator { Name = "Coal1", TotalHeatInput = 10, ActualNetGeneration = 11, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }},
                        new CoalGenerator { Name = "Coal2", TotalHeatInput = 12, ActualNetGeneration = 13, Generation = new Generation { DailyGenerations = new DailyGeneration []{ } }}
                    }
                }
            };

            var result = _underTest.Process(generationReport);

            Assert.NotNull(result);
            _actualHeatRateCalculator.Verify(x => x.Calculate(10, 11), Times.Once);
            _actualHeatRateCalculator.Verify(x => x.Calculate(12, 13), Times.Once);
        }

    }
}
