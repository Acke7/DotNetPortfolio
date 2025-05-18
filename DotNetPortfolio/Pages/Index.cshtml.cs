using Microsoft.AspNetCore.Mvc.RazorPages;

using DotNetPortfolio.ViewModel;
using PortfolioServices;

namespace DotNetPortfolio.Pages
{
public class IndexModel : PageModel
{
    private readonly IPortfolioService _projectService;

    public List<ProjectViewModel> Projects { get; set; } = new();

    public IndexModel(IPortfolioService projectService)
    {
        _projectService = projectService;
    }

    public void OnGet()
    {
        var dtos = _projectService.GetAllProjects();

        Projects = dtos.Select(dto => new ProjectViewModel
        {
            Title = dto.Title,
            TechStack = dto.TechStack,
            Description = dto.Description,
            GithubUrl = dto.GithubUrl,
            DemoUrl = dto.DemoUrl
        }).ToList();
    }
}

}