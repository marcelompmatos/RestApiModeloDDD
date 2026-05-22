using Serilog;

namespace RestApiModeloDDD.API.Logging
{
    public static class SerilogConfiguration
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()

                .WriteTo.Console()

                .WriteTo.File(
                    "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)

                .WriteTo.Seq("http://localhost:5341")

                .CreateLogger();
        }
    }
}
