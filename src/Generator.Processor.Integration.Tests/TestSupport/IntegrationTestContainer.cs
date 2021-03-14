
using Generator.Processor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Generator.Processor.Integration.Tests.TestSupport
{
    public class IntegrationTestContainer
    {
        private readonly IntegrationFileHelper _fileHelper;
        private readonly IntegrationXmlService _xmlService;

        public IntegrationTestContainer()
        {
            _fileHelper = new IntegrationFileHelper();
            _xmlService = new IntegrationXmlService();
        }

        public IntegrationFileHelper FileHelper => _fileHelper;
        public IntegrationXmlService XmlService => _xmlService;

        public byte[] GetReferenceDataXml(ReferenceData data)
        {
            return _xmlService.SerializeDataUtf8(data);
        }

        public IConfigWrapper CreateConfig(Dictionary<string, string> configValues)
        {
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

            return new ConfigWrapper(config);
        }
    }
}
