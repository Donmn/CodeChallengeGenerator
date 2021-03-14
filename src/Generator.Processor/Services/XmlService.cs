
using Generator.Processor.Models;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Generator.Processor.Services
{
    public class XmlService : IXmlService
    {
        public ReferenceData DeserializeReferenceData(string referenceDataXml)
        {
            var result = DeserializeData<ReferenceData>(referenceDataXml);
            return result;
        }

        public GenerationReport DeserializeGenerationReportData(string generationReportDataXml)
        {
            var result = DeserializeData<GenerationReport>(generationReportDataXml);
            return result;
        }

        public byte[] SerializeGenerationOutputData(GenerationOutput generationOutput)
        {
            var result = SerializeDataUtf8(generationOutput);
            return result;
        }

        private T DeserializeData<T>(string xmlData)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xmlData);
            var result = (T)xmlSerializer.Deserialize(stringReader);
            return result;
        }

        private byte[] SerializeDataUtf8<T>(T data)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            xmlSerializer.Serialize(streamWriter, data);
            var result = memoryStream.ToArray();

            return result;
        }

    }
}
