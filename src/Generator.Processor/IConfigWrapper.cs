namespace Generator.Processor
{
    public interface IConfigWrapper
    {
        string GenerationReportFolder { get; }
        string GenerationReportFilenameFilter { get; }
        string GenerationOutputFolder { get; }
        string GenerationOutputFilename { get; }
        string ReferenceDataFolder { get; }
        string ReferenceDataFilename { get; }
        string GenerationReportArchive { get; }

    }
}
