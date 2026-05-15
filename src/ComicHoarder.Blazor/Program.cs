using ComicHoarder.Application.UseCases.Publishers.Interfaces;
using ComicHoarder.Application.UseCases.Publishers;
using ComicHoarder.Blazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure;

namespace ComicHoarder.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(
                builder.Configuration.GetConnectionString("DefaultConnection"));

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();


            // Add Publisher services to the container
            builder.Services.AddScoped<IViewPublishersByNameUseCase, ViewPublishersByNameUseCase>();
            builder.Services.AddScoped<IViewPublisherByIdUseCase, ViewPublisherByIdUseCase>();
            builder.Services.AddScoped<IAddPublisherUseCase, AddPublisherUseCase>();
            builder.Services.AddScoped<IEditPublisherUseCase, EditPublisherUseCase>();
            builder.Services.AddScoped<IDeletePublisherUseCase, DeletePublisherUseCase>();



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
