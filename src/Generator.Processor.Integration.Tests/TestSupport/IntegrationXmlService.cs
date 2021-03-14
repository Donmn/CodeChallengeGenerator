using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Generator.Processor.Integration.Tests.TestSupport
{
    public class IntegrationXmlService
    {
        public T DeserializeData<T>(string xmlData)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xmlData);
            var result = (T)xmlSerializer.Deserialize(stringReader);
            return result;
        }

        public byte[] SerializeDataUtf8<T>(T data)
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
