using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace geektime_Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configBuilder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(p => config); //用工厂模式将配置对象注册到容器管理

            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });

            #region ILoggerFactory
            //IServiceProvider service = serviceCollection.BuildServiceProvider();
            //ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            //ILogger alogger = loggerFactory.CreateLogger("alogger");
            //alogger.LogDebug(2001, "aiya");
            //alogger.LogInformation("hello");
            #endregion

            #region
            //serviceCollection.AddTransient<OrderService>();
            //IServiceProvider service = serviceCollection.BuildServiceProvider();   //这两句有先后顺序
            //var order = service.GetService<OrderService>();
            //order.Show();
            #endregion

            #region 日志作用域
            /*
             一个事务包含多条操作
             复杂流程的日志关联
             调用链追踪与请求处理过程对应时
             */
            IServiceProvider service = serviceCollection.BuildServiceProvider();
            ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger("alogger");
            using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
            {
                logger.LogInformation("这是Info");
                logger.LogError("这是Error");
                logger.LogTrace("这是Trace");
            }
            #endregion

            Console.ReadKey();
        }
    }
}
