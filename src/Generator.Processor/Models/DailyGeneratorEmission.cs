using System;
using System.Xml.Serialization;

namespace Generator.Processor.Models
{
    public class DailyGeneratorEmission
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public double Emission { get; set; }
    }
}