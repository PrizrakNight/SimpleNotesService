using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using System;

namespace SimpleNotesServer
{
    public static class DependencyInjection
    {
        public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(serviceProvider => CreateLogger(serviceProvider));
        }

        private static Logger CreateLogger(IServiceProvider serviceProvider)
        {
            var configuration = new LoggerConfiguration();

            configuration.Enrich.WithExceptionDetails();
            configuration.Enrich.WithClientAgent();
            configuration.Enrich.WithClientIp();
            configuration.Enrich.WithMachineName();

            configuration.WriteTo.Console();
            configuration.WriteTo.RollingFile(new JsonFormatter(renderMessage: true), "logs/log-{Date}.log");

            return configuration.CreateLogger();
        }
    }
}
