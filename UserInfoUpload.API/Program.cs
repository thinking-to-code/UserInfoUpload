using Microsoft.Extensions.Diagnostics.HealthChecks; // Add this namespace
using Common;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

namespace UserInfoUpload.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //builder.Services.AddHealthChecks(); //Add health checks
            builder.Services.AddHealthChecks()
                .AddCheck<ApplicationDbContextHealthCheck>("Database"); // Replace AddDbContextCheck

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // read connection string from appSettings.json
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbServices(connectionString);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // map health check endpoint            
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            description = entry.Value.Description
                        }),
                        totalDuration = report.TotalDuration.TotalMilliseconds
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });

            app.Run();
        }
    }

    // Add this custom health check class
    
}
