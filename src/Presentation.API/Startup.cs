using Autofac;
using Core.Data.EF;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Presentation.API.Bootstraping;
using Presentation.API.Helpers.Models;
using System;
using System.Globalization;
using System.Reflection;

namespace Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string corsPolicyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            VersioningConfiguration(services);

            ConfigureDataContext(services);

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllers();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(Assembly.Load("Core"));
                });

            FluentValidation.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");

            AddSwaggerConfiguration(services);
           
        }

        private void ConfigureDataContext(IServiceCollection services)
        {
            
            var time = int.Parse(Configuration.GetSection("EntityFrameworkTimeOut").Value);
            services.AddDbContext<DataContext>(options => options
              .UseMySql(Configuration.GetConnectionString("Sql"),
              opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(time).TotalSeconds)));
        }

        #region Versioning Configuration

        private static void VersioningConfiguration(IServiceCollection services)
        {
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }

        #endregion

        #region Swagger Configuration

        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API with ASP.NET Core 3.1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Gonza",
                        Url = new Uri("http://www.google.com")
                    }
                });
            });
            
        }

        #endregion

        #region Autofact Registration

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new BootstrapperModule(Configuration));
        }

        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors("Cors");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corsPolicyName);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
