using FinnHub.PortfolioManagement.Infrastructure.Integrations.Services.MarketData.Setup;
using FinnHub.PortfolioManagement.Infrastructure.Integrations.Shared.Policies;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Polly;

namespace FinnHub.PortfolioManagement.Infrastructure.Integrations.Setup;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddIntegrationsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarketDataConfiguration(configuration);

        services.AddSingleton<IPolicyFactory<IAsyncPolicy<HttpResponseMessage>>, PolicyFactory>();
        return services;
    }
}
