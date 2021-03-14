using Generator.Processor.Models;
using Generator.Processor.Utilities;

namespace Generator.Processor.Services
{
    public interface IReportingServiceFactory
    {
        IFileSystemWatcherWrapper CreateFileSystemWatcher(string path, string filter);
        IDataManager CreateDataManager();
        IReferenceDataHelper CreateReferenceDataHelper(ReferenceData referenceData);
        IGenerationReportProcessor CreateGenerationReportProcessor(IReferenceDataHelper referenceDataHelper);
    }
}