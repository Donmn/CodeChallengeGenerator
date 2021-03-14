using Generator.Processor.Models.ReferenceTypes;
using Generator.Processor.Utilities;
using Microsoft.Extensions.Configuration;

namespace Generator.Processor
{
    public class ConfigWrapper : IConfigWrapper
    {
        private readonly IConfiguration _config;

        public ConfigWrapper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerationReportFolder => ValidateStringAndReturn(ConfigKeys.GenerationReportFolder, _config.GetValue<string>(ConfigKeys.GenerationReportFolder.Key));
        public string GenerationReportFilenameFilter => ValidateStringAndReturn(ConfigKeys.GenerationReportFilenameFilter, _config.GetValue<string>(ConfigKeys.GenerationReportFilenameFilter.Key));
        public string GenerationOutputFolder => ValidateStringAndReturn(ConfigKeys.GenerationOutputFolder, _config.GetValue<string>(ConfigKeys.GenerationOutputFolder.Key));
        public string GenerationOutputFilename => ValidateStringAndReturn(ConfigKeys.GenerationOutputFilename, _config.GetValue<string>(ConfigKeys.GenerationOutputFilename.Key));
        public string ReferenceDataFolder => ValidateStringAndReturn(ConfigKeys.ReferenceDataFolder, _config.GetValue<string>(ConfigKeys.ReferenceDataFolder.Key));
        public string ReferenceDataFilename => ValidateStringAndReturn(ConfigKeys.ReferenceDataFilename, _config.GetValue<string>(ConfigKeys.ReferenceDataFilename.Key));
        public string GenerationReportArchive => ValidateStringAndReturn(ConfigKeys.GenerationReportArchive, _config.GetValue<string>(ConfigKeys.GenerationReportArchive.Key));

        private string ValidateStringAndReturn(ConfigKey configKey, string value)
        {
            if (!configKey.Madatory)
                return value;

            var message = $"Config Key is mandatory. Ensure key exists and has a valid value: <{configKey.Key}>";

            Asserter.AssertStringIsNotNullOrEmpty(value, configKey.Key, message);

            return value;
        }

    }
}
