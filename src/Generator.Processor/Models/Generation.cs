using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Generation
    {
        [XmlElement("Day")]
        public DailyGeneration[] DailyGenerations { get; set; }
    }
}
