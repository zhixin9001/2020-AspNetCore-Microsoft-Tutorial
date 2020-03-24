using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _4_Config
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            var a = config.GetSection("key1").Value;
            var b = config.GetSection("section0:key0").Value;
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var starship = _config.GetSection("starship").Get<Starship>();
            services.AddSingleton<IStarship>(starship);
            services.AddControllers();

            services.Configure<MyOptionsWithDelegateConfig>(options =>
            {
                options.Option1 = "";
            });

            services.Configure<MySubOptions>(_config.GetSection("subsection1"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
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
                //    //var starship = new Starship();
                //    //config.GetSection("starship").Bind(starship);
                //    var ss = config.GetSection("starship").Get<Starship>();
                //    var b = config.GetSection("section0:key0").Value;
                //    var c = config.GetSection("section1:key1").Value;

                //    var d = config.GetSection("cmd").Value;
                //    var e = config.GetSection("cmd2").Value;

                //    await context.Response.WriteAsync($"Hello World!=={b}=={c}==cmd={d}==cmd2={e},CommandLineKey1={config.GetSection("environment").Value},sss={config.GetSection("Env1").Value}");
                //});
            });
        }
    }
}
