using Generator.Processor.Models;
using Generator.Processor.Services;
using Generator.Processor.Utilities;
using Moq;
using System;
using System.IO;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class ReportingServiceTests
    {
        private readonly Mock<IReportingServiceFactory> _reportingServiceFactory;
        private readonly Mock<IDataManager> _dataManager;
        private readonly Mock<IGenerationReportProcessor> _generationReportProcessor;
        private readonly Mock<IConfigWrapper> _config;

        private readonly ReportingService _underTest;
        private const string ReferenceFolder = "aReferenceFolder";
        private const string ReferenceFile = "aReferenceFile";
        private const string InputFolder = "aInputFolder";
        private const string GeneratorReportFilter = "*.xml";
        private const string OutputFolder = "aOutputFolder";
        private const string GeneratorOutputFilename = "aGeneratorOutputFile";
        private const string ArchiveFolder = "aArchiveFolder";


        public ReportingServiceTests()
        {
            _reportingServiceFactory = new Mock<IReportingServiceFactory>();
            _dataManager = new Mock<IDataManager>();
            _generationReportProcessor = new Mock<IGenerationReportProcessor>();

            _reportingServiceFactory.Setup(x => x.CreateDataManager()).Returns(_dataManager.Object);
            _reportingServiceFactory.Setup(x => x.CreateGenerationReportProcessor(It.IsAny<ReferenceDataHelper>())).Returns(_generationReportProcessor.Object);

            _config = new Mock<IConfigWrapper>();
            _config.Setup(x => x.ReferenceDataFolder).Returns(ReferenceFolder);
            _config.Setup(x => x.ReferenceDataFilename).Returns(ReferenceFile);
            _config.Setup(x => x.GenerationReportFolder).Returns(InputFolder);
            _config.Setup(x => x.GenerationReportFilenameFilter).Returns(GeneratorReportFilter);
            _config.Setup(x => x.GenerationOutputFolder).Returns(OutputFolder);
            _config.Setup(x => x.GenerationOutputFilename).Returns(GeneratorOutputFilename);
            _config.Setup(x => x.GenerationReportArchive).Returns(ArchiveFolder);

            _underTest = new ReportingService(_reportingServiceFactory.Object, _config.Object);
        }

        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var underTest = new ReportingService(_reportingServiceFactory.Object, _config.Object);

            Assert.NotNull(underTest);
        }

        [Fact]
        public void Ctr_WhenParametersAreNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new ReportingService(null, _config.Object));
            Assert.Throws<ArgumentNullException>(() => new ReportingService(_reportingServiceFactory.Object, null));
        }

        [Fact]
        public void Init_LoadsReferenceData_AndCorrectlySetsupServices()
        {
            var referenceData = new ReferenceData { Factors = new ReferenceFactors { EmissionsFactor = new ReferenceFactor(), ValueFactor = new ReferenceFactor() } };
            var referenceDataHelper = new ReferenceDataHelper(referenceData);
            _dataManager.Setup(x => x.LoadReferenceDataFromFile(It.IsAny<string>(), It.IsAny<string>())).Returns(referenceData);
            _reportingServiceFactory.Setup(x => x.CreateReferenceDataHelper(It.IsAny<ReferenceData>())).Returns(referenceDataHelper);

            _underTest.Init();

            _reportingServiceFactory.Verify(x => x.CreateDataManager(), Times.Once);
            _dataManager.Verify(x => x.LoadReferenceDataFromFile(ReferenceFolder, ReferenceFile), Times.Once);
            _reportingServiceFactory.Verify(x => x.CreateReferenceDataHelper(referenceData), Times.Once);
            _reportingServiceFactory.Verify(x => x.CreateGenerationReportProcessor(referenceDataHelper), Times.Once);
            _config.VerifyGet(x => x.ReferenceDataFolder, Times.Exactly(2));
            _config.VerifyGet(x => x.ReferenceDataFilename, Times.Exactly(2));
        }

        [Fact]
        public void Start_WithPathAndFilter_CreatesFileWatcherAndEvent()
        {
            var fileSystemWatcher = new Mock<IFileSystemWatcherWrapper>();
            fileSystemWatcher.SetupSet(x => x.EnableRaisingEvents = It.IsAny<bool>()).Verifiable();
            fileSystemWatcher.SetupAdd(x => x.Created += (sender, args) => { });
            _reportingServiceFactory.Setup(x => x.CreateFileSystemWatcher(It.IsAny<string>(), It.IsAny<string>())).Returns(fileSystemWatcher.Object);

            _underTest.Start();

            _reportingServiceFactory.Verify(x => x.CreateFileSystemWatcher(InputFolder, GeneratorReportFilter), Times.Once);
            fileSystemWatcher.VerifySet(x => x.EnableRaisingEvents = true, Times.Once);
            fileSystemWatcher.VerifyAdd(x => x.Created += It.IsAny<FileSystemEventHandler>(), Times.Once);
            _config.VerifyGet(x => x.GenerationReportFolder, Times.Once);
            _config.VerifyGet(x => x.GenerationReportFilenameFilter, Times.Once);
        }

        [Fact]
        public void Start_WhenFileWatcherEventFires_ProcessesReport()
        {
            var generationReport = new GenerationReport();
            var generationOutput = new GenerationOutput();
            var fileSystemWatcher = new Mock<IFileSystemWatcherWrapper>();
            _reportingServiceFactory.Setup(x => x.CreateFileSystemWatcher(It.IsAny<string>(), It.IsAny<string>())).Returns(fileSystemWatcher.Object);
            _dataManager.Setup(x => x.LoadGenerationReportFromFile(It.IsAny<string>(), It.IsAny<string>())).Returns(generationReport);
            _generationReportProcessor.Setup(x => x.Process(It.IsAny<GenerationReport>())).Returns(generationOutput);

            _underTest.Init();
            _underTest.Start();
            fileSystemWatcher.Raise(x => x.Created += null, new FileSystemEventArgs(WatcherChangeTypes.Created, "", "myFile.xml"));

            _dataManager.Verify(x => x.LoadGenerationReportFromFile(InputFolder, "myFile.xml"), Times.Once);
            _generationReportProcessor.Verify(x => x.Process(generationReport), Times.Once);
            _dataManager.Verify(x => x.SaveGenerationOutputToFile(generationOutput, OutputFolder, GeneratorOutputFilename), Times.Once);
            _dataManager.Verify(x => x.MoveFileToArchiveWithTimestamp(InputFolder, "myFile.xml", ArchiveFolder), Times.Once);
            _config.VerifyGet(x => x.GenerationReportFolder, Times.AtLeastOnce);
            _config.VerifyGet(x => x.GenerationOutputFolder, Times.Exactly(2));
            _config.VerifyGet(x => x.GenerationOutputFilename, Times.Exactly(2));
            _config.VerifyGet(x => x.GenerationReportArchive, Times.Once);
        }
    }
}
