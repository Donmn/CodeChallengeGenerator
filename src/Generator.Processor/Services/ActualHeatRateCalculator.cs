using Generator.Processor.Utilities;

namespace Generator.Processor.Services
{
    public class ActualHeatRateCalculator : IActualHeatRateCalculator
    {
        public double Calculate(double totalHeatInput, double actualNetGeneration)
        {
            Asserter.AssertIsNotZero(actualNetGeneration, nameof(actualNetGeneration));

            return totalHeatInput / actualNetGeneration;
        }
    }
}
