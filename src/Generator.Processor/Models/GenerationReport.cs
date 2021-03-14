using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GenerationReport
    {
        [XmlElement]
        public WindDataSet Wind { get; set; }

        [XmlElement]
        public GasDataSet Gas { get; set; }

        [XmlElement]
        public CoalDataSet Coal { get; set; }

    }
}
