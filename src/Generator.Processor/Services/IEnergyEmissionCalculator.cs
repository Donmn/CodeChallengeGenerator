using Generator.Processor.Models;
using System.Collections.Generic;

namespace Generator.Processor.Services
{
    public interface IEnergyEmissionCalculator
    {
        void ProcessMaxGeneratorDailyEmissions(BaseGenerator generator, double emissionsRating, double emissionFactor, List<DailyGeneratorEmission> maxDailyGeneratorEmissions);
    }
}