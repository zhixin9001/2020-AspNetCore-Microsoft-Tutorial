using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                configuration.Bind(options);
            });
            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}
