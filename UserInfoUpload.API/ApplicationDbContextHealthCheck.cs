using Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UserInfoUpload.API
{
    public class ApplicationDbContextHealthCheck : IHealthCheck
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDbContextHealthCheck(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Perform a simple query to check database connectivity
                _dbContext.Database.CanConnect();
                return Task.FromResult(HealthCheckResult.Healthy("Database is reachable."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Database is not reachable.", ex));
            }
        }
    }
}
