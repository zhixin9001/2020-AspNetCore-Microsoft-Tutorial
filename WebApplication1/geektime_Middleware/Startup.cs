using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using geektime_Middleware.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace geektime_Middleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(jsonoptions =>
                {
                    jsonoptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Exception

            //app.UseExceptionHandler(errApp =>
            //{
            //    errApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        IKnownException knownException = exceptionHandlerPathFeature.Error as IKnownException;
            //        if (knownException == null)
            //        {
            //            //var logger = context.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            //            //logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);
            //            knownException = KnownException.Unknown;
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            knownException = KnownException.FromKnownException(knownException);
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json; charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});

            #endregion

            #region StaticFile
            app.UseDirectoryBrowser(); //在UseDefaultFiles之前
            app.UseDefaultFiles(); //在UseStaticFiles之前
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/files",  //http://localhost:5000/files/page.html
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "file"))  //http://localhost:5000/page.html
            });
            app.UseStaticFiles();

            app.MapWhen(context =>
            {
                return !context.Request.Path.Value.StartsWith("/api");
            }, appBuilder =>
            {
                var option = new RewriteOptions();
                option.AddRewrite(".*", "/index.html", true);
                appBuilder.UseRewriter(option);

                appBuilder.UseStaticFiles();
                //appBuilder.Run(async c =>
                //{
                //    var file = env.WebRootFileProvider.GetFileInfo("index.html");

                //    c.Response.ContentType = "text/html";
                //    using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
                //    {
                //        await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null, BufferSize, c.RequestAborted);
                //    }
                //});
            });
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
