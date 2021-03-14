using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Services;
using System;
using System.Linq;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class ReferenceDataHelperTests
    {

        private readonly ReferenceDataHelper _underTest;
        private readonly ReferenceData _referenceData;

        public ReferenceDataHelperTests()
        {
            _referenceData = new ReferenceData
            {
                Factors = new ReferenceFactors
                {
                    ValueFactor = new ReferenceFactor { High = 100, Low = 10, Medium = 50 },
                    EmissionsFactor = new ReferenceFactor { High = 1100, Low = 1010, Medium = 1050 }
                }
            };
            _underTest = new ReferenceDataHelper(_referenceData);
        }

        [Fact]
        public void Cstr_doesNotThrow()
        {
            var undertest = new ReferenceDataHelper(_referenceData);
            Assert.NotNull(undertest);
        }

        [Fact]
        public void GetGeneratorTypeForWindGeneratorFromLocation_WithNullWindGenerator_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _underTest.GetGeneratorTypeForWindGeneratorFromLocation(null));
        }

        [Fact]
        public void GetGeneratorTypeForWindGeneratorFromLocation_WithNullLocation_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _underTest.GetGeneratorTypeForWindGeneratorFromLocation(new WindGenerator()));
        }

        [Fact]
        public void GetGeneratorTypeForWindGeneratorFromLocation_WithInvalidLocation_Throws()
        {
            const string windGeneratorName = "aName";
            const string badLocation = "abadlocation";
            var ex = Assert.Throws<InvalidOperationException>(() => _underTest.GetGeneratorTypeForWindGeneratorFromLocation(new WindGenerator { Name = windGeneratorName, Location = badLocation }));

            Assert.Contains(windGeneratorName, ex.Message);
            Assert.Contains(badLocation, ex.Message);
            Assert.Contains(WindGeneratorLocationType.Onshore.ToString(), ex.Message);
            Assert.Contains(WindGeneratorLocationType.Offshore.ToString(), ex.Message);
        }

        [Fact]
        public void GetGeneratorTypeForWindGeneratorFromLocation_WithOffShoreLocation_ReturnsOffShoreGeneratorType()
        {
            const string windGeneratorName = "aName";
            const string badLocation = "Offshore";

            var result = _underTest.GetGeneratorTypeForWindGeneratorFromLocation(new WindGenerator { Name = windGeneratorName, Location = badLocation });

            Assert.Equal(GeneratorTypes.OffShoreWind, result);
        }

        [Fact]
        public void GetGeneratorTypeForWindGeneratorFromLocation_WithOnShoreLocation_ReturnsOnShoreGeneratorType()
        {
            const string windGeneratorName = "aName";
            const string badLocation = "Onshore";

            var result = _underTest.GetGeneratorTypeForWindGeneratorFromLocation(new WindGenerator { Name = windGeneratorName, Location = badLocation });

            Assert.Equal(GeneratorTypes.OnShoreWind, result);
        }


        [Fact]
        public void GetValueFactorForGenerator_WithNullGenerator_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _underTest.GetValueFactorForGenerator(null));
        }

        [Fact]
        public void GetValueFactorForGenerator_WithHighFactor_ReturnsHighFactorValue()
        {
            var generatorType = GeneratorTypes.All.First(x => x.ValueFactorType == ValueFactorType.High);

            var result = _underTest.GetValueFactorForGenerator(generatorType);

            Assert.Equal(_referenceData.Factors.ValueFactor.High, result);
        }

        [Fact]
        public void GetValueFactorForGenerator_WithMediumFactor_ReturnsMediumFactorValue()
        {
            var generatorType = GeneratorTypes.All.First(x => x.ValueFactorType == ValueFactorType.Medium);

            var result = _underTest.GetValueFactorForGenerator(generatorType);

            Assert.Equal(_referenceData.Factors.ValueFactor.Medium, result);
        }

        [Fact]
        public void GetValueFactorForGenerator_WithLowFactor_ReturnsLowFactorValue()
        {
            var generatorType = GeneratorTypes.All.First(x => x.ValueFactorType == ValueFactorType.Low);

            var result = _underTest.GetValueFactorForGenerator(generatorType);

            Assert.Equal(_referenceData.Factors.ValueFactor.Low, result);
        }

        [Fact]
        public void GetEmissionsFactorForGenerator_WithNullGenerator_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _underTest.GetEmissionsFactorForGenerator(null));
        }

        [Fact]
        public void GetEmissionsFactorForGenerator_WithHighFactor_ReturnsHighEmissionsValue()
        {
            var generatorType = GeneratorTypes.All.First(x => x.EmissionsType == EmissionsType.High);

            var result = _underTest.GetEmissionsFactorForGenerator(generatorType);

            Assert.Equal(_referenceData.Factors.EmissionsFactor.High, result);
        }

        [Fact]
        public void GetEmissionsFactorForGenerator_WithMediumFactor_ReturnsMediumEmissionsValue()
        {
            var generatorType = GeneratorTypes.All.First(x => x.EmissionsType == EmissionsType.Medium);

            var result = _underTest.GetEmissionsFactorForGenerator(generatorType);

            Assert.Equal(_referenceData.Factors.EmissionsFactor.Medium, result);
        }

        [Fact]
        public void GetEmissionsFactorForGenerator_WithNaFactor_ReturnsValueOne()
        {
            var generatorType = GeneratorTypes.All.First(x => x.EmissionsType == EmissionsType.Na);

            var result = _underTest.GetEmissionsFactorForGenerator(generatorType);

            Assert.Equal(1, result);
        }


    }
}
