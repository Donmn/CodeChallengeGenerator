using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class CoalGenerator : BaseGenerator
    {
        [XmlElement]
        public double TotalHeatInput { get; set; }
        [XmlElement]
        public double ActualNetGeneration { get; set; }
        [XmlElement]
        public double EmissionsRating { get; set; }
    }
}
