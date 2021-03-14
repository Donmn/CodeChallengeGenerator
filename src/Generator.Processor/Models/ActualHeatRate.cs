using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ActualHeatRate
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public double HeatRate { get; set; }

    }
}