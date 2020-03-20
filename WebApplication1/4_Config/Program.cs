using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _4_Config
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static readonly Dictionary<string, string> _switchMappings =
 new Dictionary<string, string>
 {
        { "-CLKey1", "CommandLineKey1" },
        { "-CLKey2", "CommandLineKey2" }
 };

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    var dict = new Dictionary<string, string>
                    {
                        {"key1", "value1"},
                        {"key2", "value2"}
                    };

                    config.AddInMemoryCollection(dict);
                    config.AddJsonFile("config1.json", optional: true, reloadOnChange: true);
                    //config.AddCommandLine(args);
                    //config.Sources.Clear();
                    config.AddEnvironmentVariables("CUSTOM_");
                })
            //.ConfigureAppConfiguration((hostingContext, config) =>
            //{
            //    config.AddCommandLine(args, _switchMappings);
            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
