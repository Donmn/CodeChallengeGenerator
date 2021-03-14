using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GenerationOutput
    {
        [XmlElement]
        public GeneratorTotalDataSet Totals { get; set; }

        [XmlElement("MaxEmissionGenerators")]
        public DailyGeneratorEmissionDataSet MaxEmissionGenerators { get; set; }

        [XmlElement("ActualHeatRates")]
        public ActualHeatRateDataSet ActualHeatRates { get; set; }
    }
}
