using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class GasGenerator : BaseGenerator
    {
        [XmlElement]
        public double EmissionsRating { get; set; }
    }
}
