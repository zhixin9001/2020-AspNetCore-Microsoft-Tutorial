using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class StartupDevelopment
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IInjected, Injected>();
            services.AddScoped<IMyDependency, MyDependency>();
            //services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));  //容器通过利用（泛型）开放类型解析 ILogger<TCategoryName>，而无需注册每个（泛型）构造类型
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config, IInjected injected, IMyDependency myDependency)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await myDependency.WriteMessage("Inject---");
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
