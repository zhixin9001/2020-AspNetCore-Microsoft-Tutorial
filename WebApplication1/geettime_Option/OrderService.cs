using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace geettime_Option
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
        string ShowTime();
    }

    public class OrderService : IOrderService
    {
        IOptionsMonitor<OrderServiceOptions> _options;
        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            _options = options;
            _options.OnChange(option =>
            {
                Console.WriteLine($"配置更新了，最新的值是:{_options.CurrentValue.MaxOrderCount}");
            });
        }

        public int ShowMaxOrderCount()
        {
            return _options.CurrentValue.MaxOrderCount;
        }

        public string ShowTime()
        {
            return _options.CurrentValue.LastTime;
        }
    }

    public class OrderServiceOptions
    {
        [Range(0, 200)]
        public int MaxOrderCount { get; set; } = 100;

        public string LastTime { get; set; }
    }
    public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
        {
            if (options.MaxOrderCount > 100)
            {
                return ValidateOptionsResult.Fail("MaxOrderCount 不能大于100");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}
