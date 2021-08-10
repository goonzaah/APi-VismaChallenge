using Autofac;
using Microsoft.Extensions.Configuration;
using Presentation.API.Helpers.Models;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Autofac.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Presentation.API.Bootstraping
{
    public class CoreModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public CoreModule(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterSerilogLogger(builder);
            RegisterValidators(builder);
        }

        private void RegisterValidators(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.Load("Presentation.API"))
                .Where(t => t.IsClass && t.Name.EndsWith("Validator"))
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        private void RegisterSerilogLogger(ContainerBuilder builder)
        {
            var loggerConfig = new LoggerConfiguration()
                .WriteTo.MySQL(configuration.GetConnectionString("Sql"), 
                    restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(configuration["Logging:LogLevel"]));


            builder.RegisterSerilog(loggerConfig);
        }
    }
}
