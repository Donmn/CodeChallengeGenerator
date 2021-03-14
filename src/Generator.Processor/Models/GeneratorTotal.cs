using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GeneratorTotal
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public double Total { get; set; }

    }

}