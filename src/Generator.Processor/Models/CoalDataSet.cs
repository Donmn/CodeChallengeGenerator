using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class CoalDataSet
    {
        [XmlElement("CoalGenerator")]
        public CoalGenerator[] CoalGenerators { get; set; }
    }
}
