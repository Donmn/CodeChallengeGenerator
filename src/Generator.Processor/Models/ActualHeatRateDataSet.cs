using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ActualHeatRateDataSet
    {
        [XmlElement("Generator")]
        public ActualHeatRate[] GeneratorActualHeatRates { get; set; }
    }
}
