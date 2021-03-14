using System;
using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class DailyGeneration
    {
        [XmlElement]
        public DateTime Date { get; set; }
        [XmlElement]
        public double Energy { get; set; }
        [XmlElement]
        public double Price { get; set; }
    }
}
