using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestApiModeloDDD.Infrastructure.IOC;
using RestApiModeloDDD.Infrastructure.Data.Context;
using RestApiModeloDDD.API.Middlewares;
using Serilog;

namespace RestApiModeloDDD.API
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(
            IServiceCollection services)
        {
            var connection =
                Configuration.GetConnectionString(
                    "SqlConnectionString");

            services.AddDbContext<SqlContext>(
                options =>
                    options.UseSqlServer(connection));

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API Model DDD",
                        Version = "v1"
                    });
            });
        }

        public void ConfigureContainer(
            ContainerBuilder builder)
        {
            builder.RegisterModule(
                new ModuleIOC());
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // LOGS DE REQUESTS
            app.UseSerilogRequestLogging();

            // MIDDLEWARE GLOBAL DE EXCEPTION
             app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "API Model DDD v1");

                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}