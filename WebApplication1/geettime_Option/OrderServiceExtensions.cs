using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace geettime_Option
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<OrderServiceOptions>().Bind(configuration).Configure(options =>
            {
                configuration.Bind(options);
            })
               //.Validate(options => options.MaxOrderCount <= 100, "<=100");
               //.ValidateDataAnnotations<OrderServiceOptions>();
               .Services.AddSingleton<IValidateOptions<OrderServiceOptions>,OrderServiceValidateOptions > ();

            //services.AddOptions<OrderServiceOptions>().Bind(configuration);
            services.Configure<OrderServiceOptions>(configuration);
            #region 后期更新
            services.PostConfigure<OrderServiceOptions>(options =>
            {
                options.MaxOrderCount += 20;
            });
            #endregion
            services.AddSingleton<IOrderService, OrderService>();
            //services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
