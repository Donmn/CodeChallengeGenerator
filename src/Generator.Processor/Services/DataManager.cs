using Generator.Processor.Models;
using Generator.Processor.Utilities;
using System;

namespace Generator.Processor.Services
{
    public class DataManager : IDataManager
    {
        private readonly IFileHelper _fileHelper;
        private readonly IXmlService _xmlService;
        private const string ArchiveDatePrefixFormat = "yyyyMMdd_HHmmss_";

        public DataManager(IFileHelper fileHelper, IXmlService xmlService)
        {
            Asserter.AssertNotNull(fileHelper, nameof(fileHelper));
            Asserter.AssertNotNull(xmlService, nameof(xmlService));

            _fileHelper = fileHelper;
            _xmlService = xmlService;
        }

        public ReferenceData LoadReferenceDataFromFile(string folder, string fileName)
        {
            var referenceDataAsXml = _fileHelper.ReadFileAsString(folder, fileName);
            var result = _xmlService.DeserializeReferenceData(referenceDataAsXml);
            return result;
        }

        public GenerationReport LoadGenerationReportFromFile(string folder, string fileName)
        {
            var generationReportAsXml = _fileHelper.ReadFileAsString(folder, fileName);
            var result = _xmlService.DeserializeGenerationReportData(generationReportAsXml);
            return result;
        }

        public void SaveGenerationOutputToFile(GenerationOutput generationOutput, string folder, string fileName)
        {
            var generationOutputAsXmlBytes = _xmlService.SerializeGenerationOutputData(generationOutput);
            _fileHelper.WriteByteArrayAsFile(folder, fileName, generationOutputAsXmlBytes);
        }

        public void MoveFileToArchiveWithTimestamp(string sourceFolder, string sourceFilename, string archiveFolder)
        {
            var dateTimePrefix = DateTime.Now.ToString(ArchiveDatePrefixFormat);
            var archiveFileName = dateTimePrefix + sourceFilename;
            _fileHelper.MoveFile(sourceFolder, sourceFilename, archiveFolder, archiveFileName);
        }

    }
}
