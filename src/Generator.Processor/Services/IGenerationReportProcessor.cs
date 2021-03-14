using Generator.Processor.Models;

namespace Generator.Processor.Services
{
    public interface IGenerationReportProcessor
    {
        GenerationOutput Process(GenerationReport generationReport);
    }
}
