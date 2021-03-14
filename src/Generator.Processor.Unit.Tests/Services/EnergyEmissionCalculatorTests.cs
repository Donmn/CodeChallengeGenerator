
using Generator.Processor.Models;
using Generator.Processor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class EnergyEmissionCalculatorTests
    {
        private readonly EnergyEmissionCalculator _underTest;

        public EnergyEmissionCalculatorTests()
        {
            _underTest = new EnergyEmissionCalculator();
        }

        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var underTest = new EnergyEmissionCalculator();
            Assert.NotNull(underTest);
        }

        [Fact]
        public void CalculateEnergyEmission_WithValues_ReturnsEnergyEmissionValue()
        {
            var energy = 10;
            var emissionRating = 20;
            var emissionFactor = 30;

            var result = _underTest.CalculateEnergyEmission(energy, emissionRating, emissionFactor);

            Assert.Equal(6000, result);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithNullGenerator_Throws()
        {
            var undertest = new EnergyEmissionCalculator();

            Assert.Throws<ArgumentNullException>(() => _underTest.ProcessMaxGeneratorDailyEmissions(null, 0, 0, new List<DailyGeneratorEmission>()));
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithNullGeneration_Throws()
        {
            var badGenerator = new BaseGenerator();
            var undertest = new EnergyEmissionCalculator();

            Assert.Throws<ArgumentNullException>(() => _underTest.ProcessMaxGeneratorDailyEmissions(badGenerator, 0, 0, new List<DailyGeneratorEmission>()));
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithNullDailyGenerations_Throws()
        {
            var badGenerator = new BaseGenerator { Generation = new Generation() };
            var undertest = new EnergyEmissionCalculator();

            Assert.Throws<ArgumentNullException>(() => _underTest.ProcessMaxGeneratorDailyEmissions(badGenerator, 0, 0, new List<DailyGeneratorEmission>()));
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithNullDailyGeneratorEmission_Throws()
        {
            var goodGenerator = new BaseGenerator { Generation = new Generation { DailyGenerations = new DailyGeneration[] { } } };
            var undertest = new EnergyEmissionCalculator();

            Assert.Throws<ArgumentNullException>(() => _underTest.ProcessMaxGeneratorDailyEmissions(goodGenerator, 0, 0, null));
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithOneDailyGeneration_AddDailyGeneration()
        {
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission>();
            var date = new DateTime(2021, 01, 01);
            var dailyGenerations = new DailyGeneration[] {
                new DailyGeneration { Date = date, Energy = 10 }
            };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Single(maxDailyGeneratorEmissions);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions.First().Name);
            Assert.Equal(date, maxDailyGeneratorEmissions.First().Date);
            Assert.Equal(6000, maxDailyGeneratorEmissions.First().Emission);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithOneDailyGeneration_AndOneSupplied_SameDateBiggerEmission_ReplacesDailyGeneration()
        {
            var date = new DateTime(2021, 01, 01);
            var originaldailyGenerationEmission = new DailyGeneratorEmission { Date = date, Emission = 1, Name = "replaceMe" };
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission> { originaldailyGenerationEmission };
            var newDailyGeneration = new DailyGeneration { Date = date, Energy = 10 };
            var dailyGenerations = new DailyGeneration[] { newDailyGeneration };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Single(maxDailyGeneratorEmissions);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions.First().Name);
            Assert.Equal(date, maxDailyGeneratorEmissions.First().Date);
            Assert.Equal(6000, maxDailyGeneratorEmissions.First().Emission);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithOneDailyGeneration_AndOneSupplied_SameDateSmallerEmission_DoesNotReplacesDailyGeneration()
        {
            var date = new DateTime(2021, 01, 01);
            var originaldailyGenerationEmission = new DailyGeneratorEmission { Date = date, Emission = 10000, Name = "doNotReplaceMe" };
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission> { originaldailyGenerationEmission };
            var newDailyGeneration = new DailyGeneration { Date = date, Energy = 10 };
            var dailyGenerations = new DailyGeneration[] { newDailyGeneration };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Single(maxDailyGeneratorEmissions);
            Assert.Equal(originaldailyGenerationEmission.Name, maxDailyGeneratorEmissions.First().Name);
            Assert.Equal(originaldailyGenerationEmission.Date, maxDailyGeneratorEmissions.First().Date);
            Assert.Equal(originaldailyGenerationEmission.Emission, maxDailyGeneratorEmissions.First().Emission);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithOneDailyGeneration_AndOneSupplied_DifferentDate_AddsDailyGeneration()
        {
            var date = new DateTime(2021, 01, 01);
            var originaldailyGenerationEmission = new DailyGeneratorEmission { Date = date, Emission = 10000, Name = "ignoreMe" };
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission> { originaldailyGenerationEmission };
            var newDate = new DateTime(2021, 01, 02);
            var newDailyGeneration = new DailyGeneration { Date = newDate, Energy = 10 };
            var dailyGenerations = new DailyGeneration[] { newDailyGeneration };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Equal(2, maxDailyGeneratorEmissions.Count);
            Assert.Equal(originaldailyGenerationEmission.Name, maxDailyGeneratorEmissions[0].Name);
            Assert.Equal(originaldailyGenerationEmission.Date, maxDailyGeneratorEmissions[0].Date);
            Assert.Equal(originaldailyGenerationEmission.Emission, maxDailyGeneratorEmissions[0].Emission);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions[1].Name);
            Assert.Equal(newDate, maxDailyGeneratorEmissions[1].Date);
            Assert.Equal(6000, maxDailyGeneratorEmissions[1].Emission);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithTwoDailyGeneration_AndOneSupplied_DifferentDate_AddsDailyGeneration()
        {
            var date = new DateTime(2021, 01, 01);
            var originaldailyGenerationEmission = new DailyGeneratorEmission { Date = date, Emission = 10000, Name = "ignoreMe" };
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission> { originaldailyGenerationEmission };
            var newDate1 = new DateTime(2021, 01, 02);
            var newDailyGeneration1 = new DailyGeneration { Date = newDate1, Energy = 10 };
            var newDate2 = new DateTime(2021, 01, 03);
            var newDailyGeneration2 = new DailyGeneration { Date = newDate2, Energy = 50 };
            var dailyGenerations = new DailyGeneration[] { newDailyGeneration1, newDailyGeneration2 };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Equal(3, maxDailyGeneratorEmissions.Count);
            Assert.Equal(originaldailyGenerationEmission.Name, maxDailyGeneratorEmissions[0].Name);
            Assert.Equal(originaldailyGenerationEmission.Date, maxDailyGeneratorEmissions[0].Date);
            Assert.Equal(originaldailyGenerationEmission.Emission, maxDailyGeneratorEmissions[0].Emission);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions[1].Name);
            Assert.Equal(newDate1, maxDailyGeneratorEmissions[1].Date);
            Assert.Equal(6000, maxDailyGeneratorEmissions[1].Emission);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions[2].Name);
            Assert.Equal(newDate2, maxDailyGeneratorEmissions[2].Date);
            Assert.Equal(30000, maxDailyGeneratorEmissions[2].Emission);
        }

        [Fact]
        public void ProcessMaxGeneratorDailyEmissions_WithTwoDailyGeneration_AndOneSupplied_AddsOneAndReplacesExistingDailyGeneration()
        {
            var date = new DateTime(2021, 01, 01);
            var originaldailyGenerationEmission = new DailyGeneratorEmission { Date = date, Emission = 1, Name = "replaceMe" };
            var maxDailyGeneratorEmissions = new List<DailyGeneratorEmission> { originaldailyGenerationEmission };
            var newDate1 = new DateTime(2021, 01, 02);
            var newDailyGeneration1 = new DailyGeneration { Date = newDate1, Energy = 10 };
            var newDailyGeneration2 = new DailyGeneration { Date = date, Energy = 50 };
            var dailyGenerations = new DailyGeneration[] { newDailyGeneration1, newDailyGeneration2 };
            var emissionsRating = 20;
            var emissionFactor = 30;
            var generator = new BaseGenerator { Name = "aGenerator", Generation = new Generation { DailyGenerations = dailyGenerations } };

            _underTest.ProcessMaxGeneratorDailyEmissions(generator, emissionsRating, emissionFactor, maxDailyGeneratorEmissions);

            Assert.Equal(2, maxDailyGeneratorEmissions.Count);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions[0].Name);
            Assert.Equal(newDate1, maxDailyGeneratorEmissions[0].Date);
            Assert.Equal(6000, maxDailyGeneratorEmissions[0].Emission);
            Assert.Equal(generator.Name, maxDailyGeneratorEmissions[1].Name);
            Assert.Equal(date, maxDailyGeneratorEmissions[1].Date);
            Assert.Equal(30000, maxDailyGeneratorEmissions[1].Emission);
        }

    }
}
