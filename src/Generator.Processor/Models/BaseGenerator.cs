using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class BaseGenerator
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public Generation Generation { get; set; }
    }
}
