using Generator.Processor.Models;

namespace Generator.Processor.Services
{
    public class TotalGenerationCalculator : ITotalGenerationCalculator
    {
        public double SumTotalEnergy(DailyGeneration[] dailyGenerations, double valueFactor)
        {
            if (dailyGenerations == null || dailyGenerations.Length == 0)
                return 0;

            double result = 0;

            foreach (var dailyGeneration in dailyGenerations)
            {
                result += CalculateDailyGenerationValue(dailyGeneration.Energy, dailyGeneration.Price, valueFactor);
            }

            return result;
        }

        public double CalculateDailyGenerationValue(double engery, double price, double valueFactor)
        {
            return (engery * price * valueFactor);
        }
    }
}
