using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace RestApiModeloDDD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()

                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()

                .WriteTo.Console()

                .WriteTo.File(
                            path: "logs/log-.txt",
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 7,
                            fileSizeLimitBytes: 10485760,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            flushToDiskInterval: TimeSpan.FromSeconds(1))

                .WriteTo.Seq("http://localhost:5341")

                .CreateLogger();

            try
            {
                Log.Information("Iniciando API");

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex,
                    "Erro ao iniciar aplicação");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(
            string[] args) =>

            Host.CreateDefaultBuilder(args)

                .UseSerilog()

                .UseServiceProviderFactory(
                    new AutofacServiceProviderFactory())

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}