using System.IO;

namespace Generator.Processor.Utilities
{
    public interface IFileSystemWatcherWrapper
    {
        event FileSystemEventHandler Created;
        bool EnableRaisingEvents { get; set; }
    }
}
