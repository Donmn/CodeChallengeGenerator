using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class WindGenerator : BaseGenerator
    {
        [XmlElement]
        public string Location { get; set; }
    }
}
