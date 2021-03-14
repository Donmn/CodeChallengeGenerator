
namespace Generator.Processor.Utilities
{
    public interface IFileHelper
    {
        string ReadFileAsString(string folderPath, string filename);
        void WriteByteArrayAsFile(string folderPath, string filename, byte[] data);
        void MoveFile(string sourceFolderPath, string sourceFilename, string destinationFolderPath, string destinationFilename);
    }
}
