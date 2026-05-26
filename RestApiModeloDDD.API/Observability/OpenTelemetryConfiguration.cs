using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace RestApiModeloDDD.API.Observability
{
   
        public static class OpenTelemetryConfiguration
        {
            public static IServiceCollection AddObservability(
                this IServiceCollection services)
            {
                services.AddOpenTelemetry()
                    .ConfigureResource(resource =>
                    {
                        resource.AddService(
                            serviceName: "RestApiModeloDDD.API");
                    })
                    .WithTracing(tracing =>
                    {
                        tracing
                            // NECESSÁRIO
                            .AddSource("RestApiModeloDDD.API")
                            .AddAspNetCoreInstrumentation()
                            .AddHttpClientInstrumentation()
                            .AddEntityFrameworkCoreInstrumentation()
                            .AddConsoleExporter();
                    })
                    .WithMetrics(metrics =>
                    {
                        metrics
                            .AddAspNetCoreInstrumentation()
                            .AddRuntimeInstrumentation()
                            .AddHttpClientInstrumentation()
                            .AddConsoleExporter();
                    });

                return services;
            }
        }
    }
