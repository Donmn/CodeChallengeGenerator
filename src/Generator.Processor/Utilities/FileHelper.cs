using System.IO;

namespace Generator.Processor.Utilities
{
    public class FileHelper : IFileHelper
    {
        public string ReadFileAsString(string folderPath, string filename)
        {
            Asserter.AssertStringIsNotNullOrEmpty(folderPath, nameof(folderPath));
            Asserter.AssertStringIsNotNullOrEmpty(filename, nameof(filename));

            var fileToLoad = GetFileInfo(folderPath, filename);
            Asserter.AssertIsTrue(fileToLoad.Exists, nameof(fileToLoad.Exists), $"File does not exist: {fileToLoad.FullName}");
            var result = File.ReadAllText(fileToLoad.FullName);
            return result;
        }

        public void WriteByteArrayAsFile(string folderPath, string filename, byte[] data)
        {
            Asserter.AssertStringIsNotNullOrEmpty(folderPath, nameof(folderPath));
            Asserter.AssertStringIsNotNullOrEmpty(filename, nameof(filename));

            var fileToWrite = GetFileInfo(folderPath, filename);
            Asserter.AssertIsTrue(fileToWrite.Directory.Exists, nameof(fileToWrite.Directory.Exists), $"Folder does not exist: {fileToWrite.Directory.FullName}");
            File.WriteAllBytes(fileToWrite.FullName, data);
        }

        public void MoveFile(string sourceFolderPath, string sourceFilename, string destinationFolderPath, string destinationFilename)
        {
            Asserter.AssertStringIsNotNullOrEmpty(sourceFolderPath, nameof(sourceFolderPath));
            Asserter.AssertStringIsNotNullOrEmpty(sourceFilename, nameof(sourceFilename));
            Asserter.AssertStringIsNotNullOrEmpty(destinationFolderPath, nameof(destinationFolderPath));
            Asserter.AssertStringIsNotNullOrEmpty(destinationFilename, nameof(destinationFilename));

            var sourcePath = Path.Join(sourceFolderPath, sourceFilename);
            var destinationPath = Path.Join(destinationFolderPath, destinationFilename);
            File.Move(sourcePath, destinationPath);
        }

        private FileInfo GetFileInfo(string folderPath, string filename)
        {
            Asserter.AssertStringIsNotNullOrEmpty(folderPath, nameof(folderPath));
            Asserter.AssertStringIsNotNullOrEmpty(filename, nameof(filename));

            var path = Path.Join(folderPath, filename);
            var result = new FileInfo(path);
            return result;
        }

    }
}
