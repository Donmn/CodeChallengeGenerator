using Generator.Processor.Utilities;
using System;
using System.IO;

namespace Generator.Processor.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IReportingServiceFactory _reportingServiceFactory;
        private readonly IConfigWrapper _config;

        private IDataManager _dataManager;
        private IGenerationReportProcessor _generationReportProcessor;
        private IFileSystemWatcherWrapper _fileSystemWatcher;

        public ReportingService(IReportingServiceFactory reportingServiceFactory, IConfigWrapper config)
        {
            Asserter.AssertNotNull(reportingServiceFactory, nameof(reportingServiceFactory));
            Asserter.AssertNotNull(config, nameof(config));
            _reportingServiceFactory = reportingServiceFactory;
            _config = config;
        }

        public void Init()
        {
            Console.WriteLine($"Initialisation started...");

            _dataManager = _reportingServiceFactory.CreateDataManager();
            var referenceData = _dataManager.LoadReferenceDataFromFile(_config.ReferenceDataFolder, _config.ReferenceDataFilename);
            var referenceDataHelper = _reportingServiceFactory.CreateReferenceDataHelper(referenceData);
            _generationReportProcessor = _reportingServiceFactory.CreateGenerationReportProcessor(referenceDataHelper);

            Console.WriteLine($"Initialisation complete. Reference data loaded: Filename: <{_config.ReferenceDataFilename}>, Folder:<{_config.ReferenceDataFolder}>.");
        }

        public void Start()
        {
            MonitorFolder(_config.GenerationReportFolder, _config.GenerationReportFilenameFilter);
        }

        private void ProcessReport(string generatorReportFilename)
        {
            Console.WriteLine($"Report processing started: FileName: <{generatorReportFilename}>, Folder: <{_config.GenerationReportFolder}>...");

            //Load
            var generationReport = _dataManager.LoadGenerationReportFromFile(_config.GenerationReportFolder, generatorReportFilename);
            //Process
            var generationOutput = _generationReportProcessor.Process(generationReport);
            //Save
            _dataManager.SaveGenerationOutputToFile(generationOutput, _config.GenerationOutputFolder, _config.GenerationOutputFilename);
            //Archive
            _dataManager.MoveFileToArchiveWithTimestamp(_config.GenerationReportFolder, generatorReportFilename, _config.GenerationReportArchive);

            Console.WriteLine($"Report processing complete. Generation output: Filename: <{_config.GenerationOutputFilename}>, Folder: <{_config.GenerationOutputFolder}>.");
        }

        private void MonitorFolder(string path, string filter)
        {
            _fileSystemWatcher = _reportingServiceFactory.CreateFileSystemWatcher(path, filter);
            _fileSystemWatcher.Created += FileSystemWatcher_FileCreated;
            _fileSystemWatcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring started for Generation Report files matching: Filter: <{filter}>, Folder: <{path}>.");
        }

        private void FileSystemWatcher_FileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File creation detected: <{e.Name}>");
            ProcessReport(e.Name);
        }
    }
}
