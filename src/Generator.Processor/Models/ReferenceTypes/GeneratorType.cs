using System.Collections.Generic;

namespace Generator.Processor.Models.ReferenceTypes
{
    public enum WindGeneratorLocationType
    {
        Offshore,
        Onshore
    }

    public enum ValueFactorType
    {
        Low,
        Medium,
        High,
    }

    public enum EmissionsType
    {
        Na,
        Medium,
        High,
    }

    public static class GeneratorTypes
    {
        public static readonly GeneratorType OffShoreWind = new GeneratorType { Name = "Offshore Wind", ValueFactorType = ValueFactorType.Low, EmissionsType = EmissionsType.Na };
        public static readonly GeneratorType OnShoreWind = new GeneratorType { Name = "Onshore Wind", ValueFactorType = ValueFactorType.High, EmissionsType = EmissionsType.Na };
        public static readonly GeneratorType Gas = new GeneratorType { Name = "Gas", ValueFactorType = ValueFactorType.Medium, EmissionsType = EmissionsType.Medium };
        public static readonly GeneratorType Coal = new GeneratorType { Name = "Coal", ValueFactorType = ValueFactorType.Medium, EmissionsType = EmissionsType.High };

        public static List<GeneratorType> All => new List<GeneratorType>
        {
            OffShoreWind,
            OnShoreWind,
            Gas,
            Coal
        };
    }

    public class GeneratorType
    {
        public string Name { get; set; }
        public ValueFactorType ValueFactorType { get; set; }
        public EmissionsType EmissionsType { get; set; }
    }
}
