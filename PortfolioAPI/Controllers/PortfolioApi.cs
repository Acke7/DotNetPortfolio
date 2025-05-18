using Microsoft.AspNetCore.Mvc;
using PortfolioServices;


namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IPortfolioService _projectService;

    public ProjectsController(IPortfolioService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_projectService.GetAllProjects());
}
