using ComicHoarder.Application.UseCases.Publishers.Interfaces;
using ComicHoarder.Application.UseCases.Publishers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure;
using ComicHoarder.Application.UseCases.Volumes;
using ComicHoarder.Application.UseCases.Volumes.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Application.UseCases.Issues;
using ComicHoarder.Application.UseCases.Dashboard.Interfaces;
using ComicHoarder.Application.UseCases.Dashboard;
using Radzen;
using Radzen.Blazor;
using ComicHoarder.Infrastructure.ComicVine;
using ComicHoarder.Shared;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ComicHoarder.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            var loggerFactory = LoggingSetup.CreateLoggerFactory(
                builder.Configuration,
                "ComicHoarder.Blazor"
            );

            builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);

            builder.Services.AddInfrastructure(
                builder.Configuration.GetConnectionString("DefaultConnection"));

            #pragma warning disable ASP0000 //I know what I'm doing (I think)
            using (var tempapp = builder.Services.BuildServiceProvider())
            #pragma warning restore ASP0000
            {
                using (var scope = tempapp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CHContext>();

                    var apiKey = db.Settings
                        .Where(s => s.Name == "ComicVineKey")
                        .Select(s => s.Value)
                        .FirstOrDefault();

                    builder.Services.AddInfrastructureComicVine(apiKey);
                }
            }

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add Publisher services to the container
            builder.Services.AddScoped<IViewPublishersByNameUseCase, ViewPublishersByNameUseCase>();
            builder.Services.AddScoped<IViewPublisherByIdUseCase, ViewPublisherByIdUseCase>();
            builder.Services.AddScoped<IAddPublisherUseCase, AddPublisherUseCase>();
            builder.Services.AddScoped<IEditPublisherUseCase, EditPublisherUseCase>();
            builder.Services.AddScoped<IDeletePublisherUseCase, DeletePublisherUseCase>();
            builder.Services.AddTransient<IViewVolumesByPublisherAndNameUseCase, ViewVolumesByPublisherAndNameUseCase>();
            builder.Services.AddTransient<IViewVolumeByIdUseCase, ViewVolumeByIdUseCase>();
            builder.Services.AddTransient<IAddVolumeUseCase, AddVolumeUseCase>();
            builder.Services.AddTransient<IEditVolumeUseCase, EditVolumeUseCase>();
            builder.Services.AddTransient<IDeleteVolumeUseCase, DeleteVolumeUseCase>();
            builder.Services.AddTransient<IAddIssueUseCase, AddIssueUseCase>();
            builder.Services.AddTransient<IViewIssuesByVolumeAndNameUseCase, ViewIssuesByVolumeAndNameUseCase>();
            builder.Services.AddTransient<IViewIssueByIdUseCase, ViewIssueByIdUseCase>();
            builder.Services.AddTransient<IEditIssueUseCase, EditIssueUseCase>();
            builder.Services.AddTransient<IDeleteIssueUseCase, DeleteIssueUseCase>();
            builder.Services.AddTransient<IGetAllIssueFormatsUseCase, GetAllIssueFormatsUseCase>();

            builder.Services.AddTransient<ISearchMissingComicVineIssuesByVolumeUseCase, SearchMissingComicVineIssuesByVolumeUseCase>();
            builder.Services.AddTransient<ISearchComicVinePublisherUseCase, SearchComicVinePublisherUseCase>();
            builder.Services.AddTransient<ISearchMissingComicVinePublishersUseCase, SearchMissingComicVinePublishersUseCase>();
            builder.Services.AddTransient<ISearchMissingComicVineVolumesByPublisherUseCase, SearchMissingComicVineVolumesByPublisherUseCase>();

            builder.Services.AddTransient<IGetComicIssuesToCollectCountByPublisherUseCase, GetComicIssuesToCollectCountByPublisherUseCase>();
            builder.Services.AddTransient<IGetComicIssuesToCollectWithLinkUseCase, GetComicIssuesToCollectWithLinkUseCase>();

            builder.Services.AddRadzenComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
