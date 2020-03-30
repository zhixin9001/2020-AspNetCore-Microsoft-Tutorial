using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace geektime_Logging
{
    public class OrderService
    {
        ILogger<OrderService> _logger;
        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public void Show()
        {
            _logger.LogInformation("Show Time by template {}", DateTime.Now);
            //_logger.LogInformation($"Show Time{DateTime.Now}");  //不推荐这种方式，即使关闭log也会有字符串拼接操作
        }
    }
}
