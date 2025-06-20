﻿using System.Reflection;

using FinnHub.PortfolioManagement.Application.Abstractions;
using FinnHub.PortfolioManagement.Domain.Aggregates.Repositories;
using FinnHub.PortfolioManagement.Infrastructure.Persistence.Context;
using FinnHub.PortfolioManagement.Infrastructure.Persistence.Repositories;
using FinnHub.PortfolioManagement.Infrastructure.Persistence.Settings;
using FinnHub.PortfolioManagement.Infrastructure.Persistence.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinnHub.PortfolioManagement.Infrastructure.Persistence.Setup;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetSettings(services, configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(settings.ConnectionString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(settings.CommandTimeoutInSeconds);
                sqlOptions.EnableRetryOnFailure();
            }).UseSnakeCaseNamingConvention());

        services.AddHealthChecksConfiguration(settings);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();

        return services;
    }

    private static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, DatabaseSettings settings)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(settings.ConnectionString);
        return services;
    }

    private static DatabaseSettings GetSettings(IServiceCollection services, IConfiguration configuration, string section = DatabaseSettings.SectionName)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var settings = configuration.GetSection(section).Get<DatabaseSettings>()
            ?? throw new ArgumentException($"{nameof(DatabaseSettings)} should be configured.");

        return settings;
    }
}
