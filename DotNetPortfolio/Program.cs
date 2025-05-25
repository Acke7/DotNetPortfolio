using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using PortfolioServices;
using Services.Portfolio;



namespace DotNetPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // EF Core + Razor Pages
            builder.Services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<DataInitializer>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<DataInitializer>().SeedData();
            }


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.MapGet("/api/weather", async () =>
            {
                var httpClient = new HttpClient();
                var apiKey = "cb13ded496cfee0d1e4ded291a984836";
                var city = "Stockholm";  // or make this configurable

                var response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");

                if (!response.IsSuccessStatusCode)
                {
                    return Results.Problem("Could not fetch weather data");
                }

                var json = await response.Content.ReadAsStringAsync();
                return Results.Content(json, "application/json");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
