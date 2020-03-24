using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _5_Options
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile("options.json", reloadOnChange: true, optional: true);

                config.Sources.Clear(); //配置在这里无法删除默认config
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear(); //配置在这里【可以】删除默认config
                config.AddEnvironmentVariables("custom_");
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
