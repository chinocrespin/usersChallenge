using Core.Logger.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Core.Logger.Config
{
    public static class LoggerExtensions
    {
        public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggerConfig>(configuration.GetSection("LoggerConfig"));
            services.AddTransient<ILogger, Logger>();
        }
    }
}
