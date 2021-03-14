using Generator.Processor.Models;

namespace Generator.Processor.Services
{
    public interface ITotalGenerationCalculator
    {
        double SumTotalEnergy(DailyGeneration[] dailyGenerations, double valueFactor);
    }
}