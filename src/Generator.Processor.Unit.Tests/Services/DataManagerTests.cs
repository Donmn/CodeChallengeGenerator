using Generator.Processor.Models;
using Generator.Processor.Services;
using Generator.Processor.Utilities;
using Moq;
using System;
using Xunit;

namespace Generator.Processor.Unit.Tests.Services
{
    public class DataManagerTests
    {
        private readonly DataManager _underTest;

        private readonly Mock<IFileHelper> _fileHelper;
        private readonly Mock<IXmlService> _xmlService;

        public DataManagerTests()
        {
            _fileHelper = new Mock<IFileHelper>();
            _xmlService = new Mock<IXmlService>();

            _underTest = new DataManager(_fileHelper.Object, _xmlService.Object);
        }

        [Fact]
        public void Cstr_DoesNotThrow()
        {
            var result = new DataManager(_fileHelper.Object, _xmlService.Object);

            Assert.NotNull(result);
        }

        [Fact]
        public void Ctr_WhenParametersAreNull_ThrowsAlways()
        {
            Assert.Throws<ArgumentNullException>(() => new DataManager(null, _xmlService.Object));
            Assert.Throws<ArgumentNullException>(() => new DataManager(_fileHelper.Object, null));
        }

        [Fact]
        public void LoadReferenceDataFromFile_WithPath_ReturnsReferenceData()
        {
            const string folder = "aFolder";
            const string filename = "aFilename";
            const string referenceXml = "referenceXml";
            var expectedResult = new ReferenceData();
            _fileHelper.Setup(x => x.ReadFileAsString(It.IsAny<string>(), It.IsAny<string>())).Returns(referenceXml);
            _xmlService.Setup(x => x.DeserializeReferenceData(It.IsAny<string>())).Returns(expectedResult);

            var result = _underTest.LoadReferenceDataFromFile(folder, filename);

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
            _fileHelper.Verify(x => x.ReadFileAsString(folder, filename), Times.Once);
            _xmlService.Verify(x => x.DeserializeReferenceData(referenceXml), Times.Once);

        }

        [Fact]
        public void LoadGenerationReportFromFile_WithPath_ReturnsGenerationReport()
        {
            const string folder = "aFolder";
            const string filename = "aFilename";
            const string generationReportXml = "generationReportXml";
            var expectedResult = new GenerationReport();
            _fileHelper.Setup(x => x.ReadFileAsString(It.IsAny<string>(), It.IsAny<string>())).Returns(generationReportXml);
            _xmlService.Setup(x => x.DeserializeGenerationReportData(It.IsAny<string>())).Returns(expectedResult);

            var result = _underTest.LoadGenerationReportFromFile(folder, filename);

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
            _fileHelper.Verify(x => x.ReadFileAsString(folder, filename), Times.Once);
            _xmlService.Verify(x => x.DeserializeGenerationReportData(generationReportXml), Times.Once);

        }

        [Fact]
        public void SaveGenerationOutputToFile_WithGenerationOutput_SavesFile()
        {
            const string folder = "aFolder";
            const string filename = "aFilename";
            var generationOutput = new GenerationOutput();
            var fileData = new byte[] { };
            _xmlService.Setup(x => x.SerializeGenerationOutputData(It.IsAny<GenerationOutput>())).Returns(fileData);

            _underTest.SaveGenerationOutputToFile(generationOutput, folder, filename);

            _xmlService.Verify(x => x.SerializeGenerationOutputData(generationOutput), Times.Once);
            _fileHelper.Verify(x => x.WriteByteArrayAsFile(folder, filename, fileData), Times.Once);
        }

        [Fact]
        public void MoveFileToArchiveWithTimestamp_WithSourceAndArchive_AppendsTimestamp_MovesFile()
        {
            const string folder = "aFolder";
            const string archivefolder = "aArchiveFolder";
            const string filename = "aFilename";
            const string filePrefix = "yyyyMMdd_HHmmss_";
            const string archiveFileNameFormat = filePrefix + filename;

            _underTest.MoveFileToArchiveWithTimestamp(folder, filename, archivefolder);

            _fileHelper.Verify(x => x.MoveFile(folder, filename, archivefolder, It.Is<string>(s => s.Length == archiveFileNameFormat.Length && s.Contains(filename))), Times.Once);
        }

    }
}
