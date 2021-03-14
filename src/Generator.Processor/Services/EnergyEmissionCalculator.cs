
using Generator.Processor.Models;
using Generator.Processor.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Processor.Services
{
    public class EnergyEmissionCalculator : IEnergyEmissionCalculator
    {
        public void ProcessMaxGeneratorDailyEmissions(BaseGenerator generator, double emissionsRating, double emissionFactor, List<DailyGeneratorEmission> maxDailyGeneratorEmissions)
        {
            Asserter.AssertNotNull(generator, nameof(generator));
            Asserter.AssertNotNull(generator.Generation, nameof(generator.Generation));
            Asserter.AssertNotNull(generator.Generation.DailyGenerations, nameof(generator.Generation.DailyGenerations));
            Asserter.AssertNotNull(maxDailyGeneratorEmissions, nameof(maxDailyGeneratorEmissions));

            foreach (var dailyGeneration in generator.Generation.DailyGenerations)
            {
                var energyEmission = CalculateEnergyEmission(dailyGeneration.Energy, emissionsRating, emissionFactor);

                bool addRecord = false;
                if (!maxDailyGeneratorEmissions.Any(x => x.Date == dailyGeneration.Date))
                {
                    addRecord = true;
                }
                else if (maxDailyGeneratorEmissions.Any(x => x.Date == dailyGeneration.Date && x.Emission < energyEmission))
                {
                    var recordWithSameDateAndLowerEmissionToRemove = maxDailyGeneratorEmissions.Single(x => x.Date == dailyGeneration.Date);
                    maxDailyGeneratorEmissions.Remove(recordWithSameDateAndLowerEmissionToRemove);
                    addRecord = true;
                }

                if (addRecord)
                {
                    maxDailyGeneratorEmissions.Add(new DailyGeneratorEmission
                    {
                        Name = generator.Name,
                        Date = dailyGeneration.Date,
                        Emission = energyEmission
                    });
                }
            }
        }

        public double CalculateEnergyEmission(double energy, double emissionRating, double emissionFactor)
        {
            return energy * emissionRating * emissionFactor;
        }
    }
}
