using Generator.Processor.Models;
using Generator.Processor.Services;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class TotalGenerationCalculatorTests
    {
        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var undertest = new TotalGenerationCalculator();
            Assert.NotNull(undertest);
        }

        [Fact]
        public void CalculateDailyGenerationValue_WithValues_ReturnsDailyGenerationValue()
        {
            var energy = 10;
            var price = 20;
            var valueFactor = 30;
            var undertest = new TotalGenerationCalculator();

            var result = undertest.CalculateDailyGenerationValue(energy, price, valueFactor);

            Assert.Equal(6000, result);
        }


        [Fact]
        public void SumTotalEnergy_WithNullGenerations_DoesNotThrow_ReturnsZero()
        {
            var valueFactor = 0;
            var undertest = new TotalGenerationCalculator();

            var result = undertest.SumTotalEnergy(null, valueFactor);

            Assert.Equal(0, result);
        }

        [Fact]
        public void SumTotalEnergy_WithNoGenerations_DoesNotThrow_ReturnsZero()
        {
            var valueFactor = 0;
            var undertest = new TotalGenerationCalculator();
            var dailyGenerations = new DailyGeneration[] { };

            var result = undertest.SumTotalEnergy(dailyGenerations, valueFactor);

            Assert.Equal(0, result);
        }

        [Fact]
        public void SumTotalEnergy_WithGenerations_ReturnsTotal()
        {
            var undertest = new TotalGenerationCalculator();
            var valueFactor = 30;
            var dailyGenerations = new DailyGeneration[] {
                new DailyGeneration { Energy = 10, Price = 40 },
                new DailyGeneration { Energy = 20, Price = 50 }
            };

            var result = undertest.SumTotalEnergy(dailyGenerations, valueFactor);

            Assert.Equal(42000, result);
        }

    }
}
