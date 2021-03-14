using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace Generator.Processor.Integration.Tests.TestSupport
{
    public class IntegrationFileHelper
    {
        public FileInfo CurrentFolder()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var locationFileInfo = new FileInfo(assemblyLocation);
            Assert.NotNull(locationFileInfo.Directory);
            return locationFileInfo;
        }

        public FileInfo GetFileInfo(string directoryAndFilename)
        {
            var locationFileInfo = CurrentFolder();
            var path = Path.Join(locationFileInfo.Directory.FullName, directoryAndFilename);
            var result = new FileInfo(path);
            Assert.True(result.Exists);
            return result;
        }

        public string JoinRelativePathToRunPath(string relativePath)
        {
            var currentFolder = CurrentFolder();
            return Path.Join(currentFolder.DirectoryName, relativePath);
        }

        public void CreateFolder(string directoryName)
        {
            var currentFolder = CurrentFolder();
            var path = Path.Join(currentFolder.DirectoryName, directoryName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void CopyFile(string sourceDirectoryAndFilename, string destinationDirectoryAndFilename)
        {
            var sourceFile = GetFileInfo(sourceDirectoryAndFilename);
            Assert.True(sourceFile.Exists);

            var currentFolder = CurrentFolder();
            var destinationFilePath = Path.Join(currentFolder.DirectoryName, destinationDirectoryAndFilename);

            File.Copy(sourceFile.FullName, destinationFilePath, true);

        }

        public string ReadFileAsString(string directoryAndFileName)
        {
            var fileToLoad = GetFileInfo(directoryAndFileName);
            var result = File.ReadAllText(fileToLoad.FullName);
            return result;
        }

        public void WriteStringAsFile(string directoryAndFileName, string data, Encoding encoding)
        {
            var currentFolder = CurrentFolder();
            var path = Path.Join(currentFolder.DirectoryName, directoryAndFileName);
            File.WriteAllText(path, data, encoding);
        }

        public void WriteBytesAsFile(string directoryAndFileName, byte[] data)
        {
            var currentFolder = CurrentFolder();
            var path = Path.Join(currentFolder.DirectoryName, directoryAndFileName);
            File.WriteAllBytes(path, data);
        }

    }
}
