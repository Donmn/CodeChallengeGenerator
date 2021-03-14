using Generator.Processor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Generator.Processor
{
    public class Program
    {
        private static int Main()
        {
            try
            {
                var applicationHost = SetupApplicationHost();

                var config = ActivatorUtilities.CreateInstance<ConfigWrapper>(applicationHost.Services);
                var reportingServiceFactory = new ReportingServiceFactory();
                var reportingService = new ReportingService(reportingServiceFactory, config);

                reportingService.Init();
                reportingService.Start();

                Console.WriteLine("Press any key to stop monitoring.");
                Console.ReadKey();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message} - {ex.StackTrace}");
                return 1;
            }
        }

        private static IHost SetupApplicationHost()
        {
            var configurationBuilder = new ConfigurationBuilder();
            BuildConfig(configurationBuilder);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IConfigWrapper, ConfigWrapper>();
                })
                .Build();

            return host;
        }

        private static void BuildConfig(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        }
    }
}
