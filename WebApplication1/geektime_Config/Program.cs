using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace geektime_Config
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            #region AddInMemoryCollection
            //builder.AddInMemoryCollection(new Dictionary<string, string>()
            //{
            //    { "key1","value1" },
            //    { "key2","value2" },
            //    { "section1:key4","value4" },
            //    { "section2:key5","value5" },
            //    { "section2:key6","value6" },
            //    { "section2:section3:key7","value7" }
            //});

            //IConfigurationRoot configurationRoot = builder.Build();


            //Console.WriteLine(configurationRoot["key1"]);
            //Console.WriteLine(configurationRoot["key2"]);


            //IConfigurationSection section = configurationRoot.GetSection("section1");
            //Console.WriteLine($"key4:{section["key4"]}");
            //Console.WriteLine($"key5:{section["key5"]}");

            //IConfigurationSection section2 = configurationRoot.GetSection("section2");
            //Console.WriteLine($"key5_v2:{section2["key5"]}");
            //var section3 = section2.GetSection("section3");
            //Console.WriteLine($"key7:{section3["key7"]}");
            #endregion

            #region CommandLine
            //var mapper = new Dictionary<string, string> { { "-k1", "CommandLineKey1" } };
            //builder.AddCommandLine(args, mapper);

            //IConfigurationRoot configurationRoot = builder.Build();
            //Console.WriteLine($"CommandLineKey2:{configurationRoot["CommandLineKey2"]}");
            //Console.WriteLine($"CommandLineKey1:{configurationRoot["CommandLineKey1"]}");  //Switch mappings 交换映射
            #endregion

            #region EnvironmentVariables
            //builder.AddEnvironmentVariables("Custom_");
            //IConfigurationRoot configurationRoot = builder.Build();
            //Console.WriteLine($"KEY1:{configurationRoot["KEY1"]}");
            //Console.WriteLine($"KEY3:{configurationRoot["SECTION1:KEY3"]}");
            ////shell:   $env:CUSTOM_KEY1="dll-3"
            ////cmd:     set CUSTOM_KEY1=dll-3
            #endregion

            #region File-Json
            //builder.AddJsonFile("config.json");
            //var configurationRoot = builder.Build();
            //var config = new Config()
            //{
            //    Key1 = "config key1",
            //    Key5 = false
            //};

            //configurationRoot.GetSection("OrderService").Bind(config);
            //Console.WriteLine($"Key1:{config.Key1}");
            //Console.WriteLine($"Key5:{config.Key5}");
            //Console.WriteLine($"Key6:{config.Key6}");
            #endregion

            #region Custom
            ////builder.Add(new MyConfigurationSource());
            //builder.AddMyConfiguration();

            //var configRoot = builder.Build();

            //ChangeToken.OnChange(() => configRoot.GetReloadToken(), () =>
            //{
            //    Console.WriteLine($"lastTime:{configRoot["lastTime"]}");
            //});

            //Console.WriteLine("开始了");
            //Console.ReadKey();
            #endregion
        }

        class Config
        {
            public string Key1 { get; set; }
            public bool Key5 { get; set; }
            public int Key6 { get; private set; } = 100;
        }
    }
}
