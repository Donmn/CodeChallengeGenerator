namespace Generator.Processor.Services
{
    public interface IActualHeatRateCalculator
    {
        double Calculate(double totalHeatInput, double actualNetGeneration);
    }
}