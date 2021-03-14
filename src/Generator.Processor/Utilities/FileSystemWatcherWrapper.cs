using System.IO;

namespace Generator.Processor.Utilities
{
    public class FileSystemWatcherWrapper : FileSystemWatcher, IFileSystemWatcherWrapper
    {
        public FileSystemWatcherWrapper(string path, string filter)
            : base(path, filter)
        {
        }
    }
}
