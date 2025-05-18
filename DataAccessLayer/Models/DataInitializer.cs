using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class DataInitializer
    {
        private readonly PortfolioDbContext _dbContext;

        public DataInitializer(PortfolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            _dbContext.Database.Migrate();

            if (!_dbContext.Projects.Any())
            {
                _dbContext.Projects.AddRange(
                    new Project
                    {
                        Title = "Architecture Portfolio Website",
                        TechStack = "ASP.NET Core MVC, Razor, Bootstrap",
                        Date = new DateTime(2024, 4, 10),
                        Description = "A personal architecture portfolio with live projects, categories, and a contact form.",
                        GithubUrl = "https://github.com/youruser/architecture-portfolio",
                        DemoUrl = "https://axion.icu"
                    },
                    new Project
                    {
                        Title = "Bank App",
                        TechStack = "ASP.NET Core MVC, EF Core, SQL Server",
                        Date = new DateTime(2024, 5, 5),
                        Description = "A banking system with customer accounts, money transfers, suspicious transaction checker, and reports.",
                        GithubUrl = "https://github.com/youruser/bankapp",
                        DemoUrl = "https://yourbankapp.azurewebsites.net"
                    },
                    new Project
                    {
                        Title = "Superhero Web API",
                        TechStack = "ASP.NET Core Web API, Swagger, JWT",
                        Date = new DateTime(2024, 3, 20),
                        Description = "An API that provides superhero data with support for authentication and role-based access.",
                        GithubUrl = "https://github.com/youruser/superhero-api",
                        DemoUrl = "https://yoursuperheroapi.azurewebsites.net"
                    }
                );

                _dbContext.SaveChanges();
            }
        }
    }
}
