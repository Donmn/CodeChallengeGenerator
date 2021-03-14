using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ReferenceFactors
    {
        [XmlElement]
        public ReferenceFactor ValueFactor { get; set; }

        [XmlElement]
        public ReferenceFactor EmissionsFactor { get; set; }
    }
}