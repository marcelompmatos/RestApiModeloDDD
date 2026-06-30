using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly SqlContext _context;

        public DatabaseHealthCheck(SqlContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                if (canConnect)
                    return HealthCheckResult.Healthy("Banco conectado.");

                return HealthCheckResult.Unhealthy("Banco indisponível.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}