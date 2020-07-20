using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Poc_Elastic_Search.Api.Contract;
using Poc_Elastic_Search.Api.ElasticSearch.Repository;
using Poc_Elastic_Search.Api.services;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Poc_Elastic_Search.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri)){
                AutoRegisterTemplate = true,
            }).CreateLogger();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
        IWebHostEnvironment env,
        ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void RegisterServices(IServiceCollection services)
        {
            #region Repository
            services.AddScoped<IPersonRepository, PersonRepository>();
            #endregion

            #region Service
            services.AddScoped<IPersonService, PersonService>();
            #endregion
        }
    }
}
