using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Utilities;
using System.Collections.Generic;

namespace Generator.Processor.Services
{
    public class GenerationReportProcessor : IGenerationReportProcessor
    {
        private readonly ITotalGenerationCalculator _totalGenerationCalculator;
        private readonly IEnergyEmissionCalculator _energyEmissionCalculator;
        private readonly IActualHeatRateCalculator _actualHeatRateCalculator;
        private readonly IReferenceDataHelper _referenceDataHelper;

        public GenerationReportProcessor(ITotalGenerationCalculator totalGenerationCalculator, IEnergyEmissionCalculator energyEmissionCalculator, IActualHeatRateCalculator actualHeatRateCalculator, IReferenceDataHelper referenceDataHelper)
        {
            Asserter.AssertNotNull(totalGenerationCalculator, nameof(totalGenerationCalculator));
            Asserter.AssertNotNull(energyEmissionCalculator, nameof(energyEmissionCalculator));
            Asserter.AssertNotNull(actualHeatRateCalculator, nameof(actualHeatRateCalculator));
            Asserter.AssertNotNull(referenceDataHelper, nameof(referenceDataHelper));

            _totalGenerationCalculator = totalGenerationCalculator;
            _energyEmissionCalculator = energyEmissionCalculator;
            _actualHeatRateCalculator = actualHeatRateCalculator;
            _referenceDataHelper = referenceDataHelper;
        }

        public GenerationOutput Process(GenerationReport generationReport)
        {
            var generatorTotals = new List<GeneratorTotal>();
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission>();
            var actualHeatRates = new List<ActualHeatRate>();

            ProcessCalculations(generationReport, generatorTotals, maxDailyGeneratorEmissions, actualHeatRates);

            var result = new GenerationOutput
            {
                Totals = new GeneratorTotalDataSet { GeneratorTotals = generatorTotals.ToArray() },
                MaxEmissionGenerators = new DailyGeneratorEmissionDataSet { DailyGeneratorEmissions = maxDailyGeneratorEmissions.ToArray() },
                ActualHeatRates = new ActualHeatRateDataSet { GeneratorActualHeatRates = actualHeatRates.ToArray() }
            };

            return result;
        }

        private void ProcessCalculations(GenerationReport generationReport, List<GeneratorTotal> generatorTotals, List<DailyGeneratorEmission> maxDailyGeneratorEmissions, List<ActualHeatRate> actualHeatRates)
        {
            if (generationReport?.Wind?.WindGenerators != null)
            {
                foreach (var windGenerator in generationReport.Wind.WindGenerators)
                {
                    var generatorType = _referenceDataHelper.GetGeneratorTypeForWindGeneratorFromLocation(windGenerator);
                    var generatorTotal = SumTotalEnergyForGenerator(windGenerator, generatorType);
                    generatorTotals.Add(generatorTotal);
                }
            }

            if (generationReport?.Gas?.GasGenerators != null)
            {
                foreach (var gasGenerator in generationReport.Gas.GasGenerators)
                {
                    var generatorTotal = SumTotalEnergyForGenerator(gasGenerator, GeneratorTypes.Gas);
                    generatorTotals.Add(generatorTotal);

                    ProcessMaxGeneratorDailyEmissions(gasGenerator, gasGenerator.EmissionsRating, GeneratorTypes.Gas, maxDailyGeneratorEmissions);
                }
            }

            if (generationReport?.Coal?.CoalGenerators != null)
            {
                foreach (var coalGenerator in generationReport.Coal.CoalGenerators)
                {
                    var generatorTotal = SumTotalEnergyForGenerator(coalGenerator, GeneratorTypes.Coal);
                    generatorTotals.Add(generatorTotal);

                    ProcessMaxGeneratorDailyEmissions(coalGenerator, coalGenerator.EmissionsRating, GeneratorTypes.Coal, maxDailyGeneratorEmissions);

                    var generatorActualHeatRate = CalculateActualHeatRate(coalGenerator, coalGenerator.TotalHeatInput, coalGenerator.ActualNetGeneration);
                    actualHeatRates.Add(generatorActualHeatRate);
                }
            }
        }

        private GeneratorTotal SumTotalEnergyForGenerator(BaseGenerator generator, GeneratorType generatorType)
        {
            var result = new GeneratorTotal { Name = generator.Name };
            var valueFactor = _referenceDataHelper.GetValueFactorForGenerator(generatorType);
            result.Total = _totalGenerationCalculator.SumTotalEnergy(generator.Generation.DailyGenerations, valueFactor);
            return result;
        }

        private void ProcessMaxGeneratorDailyEmissions(BaseGenerator generator, double emissionsRating, GeneratorType generatorType, List<DailyGeneratorEmission> maxDailyGeneratorEmissions)
        {
            var emissionsFactor = _referenceDataHelper.GetEmissionsFactorForGenerator(generatorType);
            _energyEmissionCalculator.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionsFactor, maxDailyGeneratorEmissions);
        }

        private ActualHeatRate CalculateActualHeatRate(BaseGenerator generator, double totalHeatInput, double actualNetGeneration)
        {
            var result = new ActualHeatRate { Name = generator.Name };
            result.HeatRate = _actualHeatRateCalculator.Calculate(totalHeatInput, actualNetGeneration);
            return result;
        }

    }
}
