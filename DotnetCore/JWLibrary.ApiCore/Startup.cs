using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWLibrary.ApiCore.Config;
using JWLibrary.ApiCore.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JWLibrary.ApiCore
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
            SwaggerConfig.ConfigureServices(services);
            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new LowercaseContractResolver();
                });
            //.AddNewtonsoftJson(o => o.SerializerSettings.Converters.Insert(0, new CustomConverter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SwaggerConfig.Configure(app);
            Database.DatabaseConfig.Configure();
        }
    }

    public class LowercaseContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver {
        protected override string ResolvePropertyName(string propertyName) {
            return propertyName.ToLower();
        }
    }
}
