using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ReferenceData
    {
        [XmlElement]
        public ReferenceFactors Factors { get; set; }
    }
}
