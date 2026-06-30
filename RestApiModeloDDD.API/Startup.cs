using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestApiModeloDDD.API.Configurations;
using RestApiModeloDDD.API.Middlewares;
using RestApiModeloDDD.API.Observability;
using RestApiModeloDDD.Domain.Validations;
using RestApiModeloDDD.Infrastructure.Data.Context;
using RestApiModeloDDD.Infrastructure.IOC;
using Serilog;
using System;
using System.Text;

namespace RestApiModeloDDD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection =
                Configuration.GetConnectionString("SqlConnectionString");

            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(connection));

            // =========================
            // OBSERVABILIDADE
            // =========================
            services.AddObservability();

            // =========================
            // HEALTH CHECKS
            // =========================
            services.AddHealthChecksConfiguration();

            // =========================
            // JWT
            // =========================
            var secretKey = Configuration["JwtSettings:SecretKey"];
            var issuer = Configuration["JwtSettings:Issuer"];
            var audience = Configuration["JwtSettings:Audience"];

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("JwtSettings:SecretKey não configurado.");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new Exception("JwtSettings:Issuer não configurado.");

            if (string.IsNullOrWhiteSpace(audience))
                throw new Exception("JwtSettings:Audience não configurado.");

            var key = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),

                            ValidateIssuer = true,
                            ValidIssuer = issuer,

                            ValidateAudience = true,
                            ValidAudience = audience,

                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };
                });

            services.AddAuthorization();

            services.AddControllers();

            // =========================
            // FLUENT VALIDATION
            // =========================
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ClienteValidation>();

            services.AddEndpointsApiExplorer();

            // =========================
            // SWAGGER
            // =========================
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Model DDD",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Bearer {token}"
                    });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference =
                                    new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ModuleIOC());
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "API Model DDD v1");

                c.RoutePrefix = "swagger";
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter =
                    UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

