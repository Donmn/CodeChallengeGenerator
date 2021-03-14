using Generator.Processor.Models;
using Generator.Processor.Utilities;

namespace Generator.Processor.Services
{
    public class ReportingServiceFactory : IReportingServiceFactory
    {
        public IDataManager CreateDataManager()
        {
            var fileHelper = new FileHelper();
            var xmlService = new XmlService();
            var result = new DataManager(fileHelper, xmlService);
            return result;
        }

        public IReferenceDataHelper CreateReferenceDataHelper(ReferenceData referenceData)
        {
            var result = new ReferenceDataHelper(referenceData);
            return result;
        }

        public IGenerationReportProcessor CreateGenerationReportProcessor(IReferenceDataHelper referenceDataHelper)
        {
            var totalGenerationCalculator = new TotalGenerationCalculator();
            var energyEmissionCalculator = new EnergyEmissionCalculator();
            var actualHeatRateCalculator = new ActualHeatRateCalculator();

            var result = new GenerationReportProcessor(totalGenerationCalculator, energyEmissionCalculator, actualHeatRateCalculator, referenceDataHelper);
            return result;
        }

        public IFileSystemWatcherWrapper CreateFileSystemWatcher(string path, string filter)
        {
            var result = new FileSystemWatcherWrapper(path, filter);
            return result;
        }
    }
}
