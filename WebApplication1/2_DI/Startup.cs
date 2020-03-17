using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace _2_DI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISingletonTest, SingletonTest>();
            services.TryAddSingleton<ISingletonTest, SingletonTest>();
            services.AddTransient<ITransientTest, TransientTest>();
            services.AddScoped<IScopedTest, ScopedTest>();
            //services.AddTransient<ScopeAndTransientTester>();
            //services.AddScoped(typeof(IScopedTest), typeof(ScopedTest));

            services.AddControllers();

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, ISingletonTest singleton, ITransientTest transient, IScopedTest scoped*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    //await context.Response.WriteAsync($"singleton={singleton.guid};transient={transient.guid};scoped={scoped.guid}");
                //});
            });
        }
    }
}
