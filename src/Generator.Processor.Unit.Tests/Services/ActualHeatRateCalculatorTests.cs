using Generator.Processor.Services;
using System;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class ActualHeatRateCalculatorTests
    {
        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var underTest = new ActualHeatRateCalculator();
            Assert.NotNull(underTest);
        }

        [Fact]
        public void Calculate_WithZeroActualNetGeneration_Throws()
        {
            var undertest = new ActualHeatRateCalculator();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => undertest.Calculate(1, 0));

            Assert.Contains("actualNetGeneration", ex.Message);
        }

        [Fact]
        public void Calculate_WithValues_ReturnsExpected()
        {
            const double totalHeatInput = 10;
            const double actualNetGeneration = 2;
            const double expectedActualHeatRate = 5;
            var undertest = new ActualHeatRateCalculator();

            var result = undertest.Calculate(totalHeatInput, actualNetGeneration);

            Assert.Equal(expectedActualHeatRate, result);
        }
    }
}
