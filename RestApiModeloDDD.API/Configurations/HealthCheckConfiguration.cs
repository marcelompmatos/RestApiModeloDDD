using Microsoft.Extensions.DependencyInjection;
using RestApiModeloDDD.API.HealthChecks;

namespace RestApiModeloDDD.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthChecksConfiguration(
            this IServiceCollection services)
        {
            services
                .AddHealthChecks()

                .AddCheck<DatabaseHealthCheck>("Database");

            return services;
        }
    }
}