using Generator.Processor.Models;

namespace Generator.Processor.Services
{
    public interface IDataManager
    {
        ReferenceData LoadReferenceDataFromFile(string folder, string fileName);
        GenerationReport LoadGenerationReportFromFile(string folder, string fileName);
        void SaveGenerationOutputToFile(GenerationOutput generationOutput, string folder, string fileName);
        void MoveFileToArchiveWithTimestamp(string sourceFolder, string sourceFilename, string archiveFolder);
    }
}
