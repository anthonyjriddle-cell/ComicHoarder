using ComicHoarder.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComicHoarder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string? connectionString)
    {
        services.AddDbContextFactory<CHContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IPublisherRepository, PublisherEFCoreRepository>();
        services.AddScoped<IVolumeRepository, VolumeEFCoreRepository>();
        //services.AddScoped<IIssueRepository, IssueRepository>();
        // Add more repositories here...

        return services;
    }
}

