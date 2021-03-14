using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class WindDataSet
    {
        [XmlElement("WindGenerator")]
        public WindGenerator[] WindGenerators { get; set; }
    }
}
