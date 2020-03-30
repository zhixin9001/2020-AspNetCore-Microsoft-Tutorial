using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using geektime_Config;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}
