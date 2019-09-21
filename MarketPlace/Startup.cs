using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using static System.Environment;
using MarketPlace.Domain;
using MarketPlace.Api;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace MarketPlace
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Marketplace",
                Conventions =
                    {
                        FindIdentityProperty = m => m.Name == "_databaseId"
                    }
            };
            store.Conventions.RegisterAsyncIdConvention<ClassifiedAd>(
                (dbName, entity) => Task.FromResult("ClassifiedAd/" + entity.Id.ToString()));
            store.Initialize();

            services.AddTransient(c => store.OpenAsyncSession());
            services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
            services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
            services.AddSingleton<ClassifiedAdsApplicationService>();
            
            services.AddMvc();
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "ClassifiedAds",
                        Version = "v1"
                    }
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "ClassifiedAds v1"
                )
            );
        }
    }
}
