using AmmoFinder.Common.Extensions;
using AmmoFinder.Data;
using AmmoFinder.Persistence;
using AmmoFinder.Persistence.Services;
using AmmoFinder.Retailers;
using AutoMapper;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AmmoFinder.Api
{
    //http://gunbot.net/
    //http://gunbot.net/list
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            #region Core

            services.AddAutoMapper(config =>
            {
                config.AddMaps(new List<Assembly> { typeof(ProductServiceBase).Assembly, typeof(RefreshProducts).Assembly });
            });

            services.AddControllers(options =>
                {
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo());
                options.IncludeXmlComments(typeof(Startup).Assembly.GetXmlComments());
            });

            #endregion Core

            services.AddProductPersistence(Configuration.GetConnectionString("Products"));
            services.AddRetailers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Core

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionHandler(options =>
            {
                options.ContentType = "application/json";
                options.ResponseBody(exception =>
                {
                    return exception.Message;
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(options =>
            {
            });

            app.UseReDoc(options =>
            {
                options.SpecUrl("../swagger/v1/swagger.yaml");
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.yaml", "v1");
            });

            #endregion Core

            app.UpdateAndSeedDatabase<ProductsContext>();
        }
    }
}
