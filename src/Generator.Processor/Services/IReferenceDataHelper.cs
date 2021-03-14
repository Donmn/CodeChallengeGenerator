using Generator.Processor.Models;
using Generator.Processor.Models.ReferenceTypes;

namespace Generator.Processor.Services
{
    public interface IReferenceDataHelper
    {
        GeneratorType GetGeneratorTypeForWindGeneratorFromLocation(WindGenerator windGenerator);
        double GetValueFactorForGenerator(GeneratorType generatorType);
        double GetEmissionsFactorForGenerator(GeneratorType generatorType);
    }
}
