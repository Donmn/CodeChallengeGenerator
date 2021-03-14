using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Utilities;
using System;

namespace Generator.Processor.Services
{
    public class ReferenceDataHelper : IReferenceDataHelper
    {
        private readonly ReferenceData _referenceData;

        public ReferenceDataHelper(ReferenceData referenceData)
        {
            Asserter.AssertNotNull(referenceData, nameof(_referenceData));
            Asserter.AssertNotNull(referenceData.Factors, nameof(_referenceData.Factors));
            Asserter.AssertNotNull(referenceData.Factors.ValueFactor, nameof(_referenceData.Factors.ValueFactor));
            Asserter.AssertNotNull(referenceData.Factors.EmissionsFactor, nameof(_referenceData.Factors.EmissionsFactor));
            _referenceData = referenceData;
        }

        public GeneratorType GetGeneratorTypeForWindGeneratorFromLocation(WindGenerator windGenerator)
        {
            Asserter.AssertNotNull(windGenerator, nameof(windGenerator));
            Asserter.AssertNotNull(windGenerator.Location, nameof(windGenerator.Location));

            if (Enum.TryParse(windGenerator.Location, out WindGeneratorLocationType windGeneratorLocationType))
            {
                switch (windGeneratorLocationType)
                {
                    case WindGeneratorLocationType.Offshore:
                        return GeneratorTypes.OffShoreWind;
                    case WindGeneratorLocationType.Onshore:
                        return GeneratorTypes.OnShoreWind;
                    default:
                        break;
                }
            }

            throw new InvalidOperationException($"Unknown wind generator type: Name: {windGenerator.Name}, Location: {windGenerator.Location}.  Expecting location to be: {WindGeneratorLocationType.Onshore} or {WindGeneratorLocationType.Offshore}");
        }

        public double GetValueFactorForGenerator(GeneratorType generatorType)
        {
            Asserter.AssertNotNull(generatorType, nameof(generatorType));

            switch (generatorType.ValueFactorType)
            {
                case ValueFactorType.High:
                    return _referenceData.Factors.ValueFactor.High;
                case ValueFactorType.Medium:
                    return _referenceData.Factors.ValueFactor.Medium;
                case ValueFactorType.Low:
                    return _referenceData.Factors.ValueFactor.Low;
                default:
                    break;
            }

            throw new InvalidOperationException($"Unknown GeneratorType ValueFactorType: {generatorType.ValueFactorType}.  Expecting ValueFactorType to be: {string.Join(", ", Enum.GetNames(typeof(ValueFactorType)))}");

        }

        public double GetEmissionsFactorForGenerator(GeneratorType generatorType)
        {
            Asserter.AssertNotNull(generatorType, nameof(generatorType));

            switch (generatorType.EmissionsType)
            {
                case EmissionsType.High:
                    return _referenceData.Factors.EmissionsFactor.High;
                case EmissionsType.Medium:
                    return _referenceData.Factors.EmissionsFactor.Medium;
                case EmissionsType.Na:
                    return 1;
                default:
                    break;
            }

            throw new InvalidOperationException($"Unknown GeneratorType EmissionsType: {generatorType.EmissionsType}.  Expecting EmissionsType to be: {string.Join(", ", Enum.GetNames(typeof(EmissionsType)))}");

        }
    }
}
