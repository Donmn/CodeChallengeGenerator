using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    public class DailyGeneratorEmissionDataSet
    {
        [XmlElement("Day")]
        public DailyGeneratorEmission[] DailyGeneratorEmissions { get; set; }
    }
}
