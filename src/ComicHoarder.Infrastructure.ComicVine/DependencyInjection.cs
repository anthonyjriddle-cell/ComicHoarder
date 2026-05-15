using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure.ComicVine.ComicVine;
using ComicHoarder.Infrastructure.ComicVine.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ComicHoarder.Infrastructure.ComicVine;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureComicVine(
        this IServiceCollection services,
        string apiKey)
    {

        services.AddScoped<IWebConnection, WebConnection>();
        services.AddScoped<IWebDataService, WebDataService>();
        services.AddScoped<IURLBuilder>(sp => new URLBuilder(apiKey));
        // Add more repositories here...

        return services;
    }
}

