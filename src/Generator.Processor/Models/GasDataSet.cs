using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GasDataSet
    {
        [XmlElement("GasGenerator")]
        public GasGenerator[] GasGenerators { get; set; }
    }
}
