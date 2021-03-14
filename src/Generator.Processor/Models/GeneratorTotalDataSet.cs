using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GeneratorTotalDataSet
    {
        [XmlElement("Generator")]
        public GeneratorTotal[] GeneratorTotals { get; set; }
    }
}
