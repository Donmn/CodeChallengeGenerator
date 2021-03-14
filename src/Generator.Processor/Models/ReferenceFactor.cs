using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ReferenceFactor
    {
        [XmlElement]
        public double High { get; set; }

        [XmlElement]
        public double Medium { get; set; }

        [XmlElement]
        public double Low { get; set; }
    }
}
