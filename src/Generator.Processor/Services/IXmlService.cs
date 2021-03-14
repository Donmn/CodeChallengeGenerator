using Generator.Processor.Models;

namespace Generator.Processor.Services
{
    public interface IXmlService
    {
        ReferenceData DeserializeReferenceData(string referenceDataXml);
        GenerationReport DeserializeGenerationReportData(string generationReportDataXml);
        byte[] SerializeGenerationOutputData(GenerationOutput generationOutput);
    }
}