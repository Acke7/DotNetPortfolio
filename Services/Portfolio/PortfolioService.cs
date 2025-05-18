using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Dtos;
using DataAccessLayer.Models;



namespace PortfolioServices;

public class PortfolioService : IPortfolioService
{
    private readonly PortfolioDbContext _context;

    public PortfolioService(PortfolioDbContext context)
    {
        _context = context;
    }

    public List<ProjectDto> GetAllProjects()
    {
        return _context.Projects
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                TechStack = p.TechStack,
                Date = p.Date,
                Description = p.Description,
                GithubUrl = p.GithubUrl,
                DemoUrl = p.DemoUrl
            })
            .ToList();
    }
}
