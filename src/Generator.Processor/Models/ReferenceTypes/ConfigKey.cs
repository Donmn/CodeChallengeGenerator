using System.Collections.Generic;

namespace Generator.Processor.Models.ReferenceTypes
{
    public static class ConfigKeys
    {
        public static readonly ConfigKey GenerationReportFolder = new ConfigKey { Key = "Generator.Processor.GernerationReport.InputPath", Madatory = true };
        public static readonly ConfigKey GenerationReportFilenameFilter = new ConfigKey { Key = "Generator.Processor.GernerationReport.FilenameFilter", Madatory = true };
        public static readonly ConfigKey GenerationOutputFolder = new ConfigKey { Key = "Generator.Processor.GenerationOutput.OutputPath", Madatory = true };
        public static readonly ConfigKey GenerationOutputFilename = new ConfigKey { Key = "Generator.Processor.GenerationOutput.Filename", Madatory = true };
        public static readonly ConfigKey ReferenceDataFolder = new ConfigKey { Key = "Generator.Processor.Reference.Path", Madatory = true };
        public static readonly ConfigKey ReferenceDataFilename = new ConfigKey { Key = "Generator.Processor.Reference.Filename", Madatory = true };
        public static readonly ConfigKey GenerationReportArchive = new ConfigKey { Key = "Generator.Processor.GernerationReport.ArchivePath", Madatory = true };

        public static List<ConfigKey> All => new List<ConfigKey>
        {
            GenerationReportFolder,
            GenerationReportFilenameFilter,
            GenerationOutputFolder,
            GenerationOutputFilename,
            ReferenceDataFolder,
            ReferenceDataFilename,
            GenerationReportArchive
        };
    }

    public class ConfigKey
    {
        public string Key { get; set; }
        public bool Madatory { get; set; }
    }
}
