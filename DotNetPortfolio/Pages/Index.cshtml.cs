using Microsoft.AspNetCore.Mvc.RazorPages;

using DotNetPortfolio.ViewModel;
using PortfolioServices;
using Services.Portfolio;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPortfolio.Pages
{
public class IndexModel : PageModel
{
    //private readonly IPortfolioService _projectService;
        private readonly IEmailService _emailService;
        public List<ProjectViewModel> Projects { get; set; } = new();
    
        public IndexModel(/*IPortfolioService projectService,*/ IEmailService emailService)
        {
            //_projectService = projectService;
            _emailService = emailService;
        }


        [BindProperty]
        public ContactFormViewModel ContactForm { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var body = $@"
        <p><strong>From:</strong> {ContactForm.Name} ({ContactForm.Email})</p>
        <p><strong>Message:</strong></p>
        <p>{ContactForm.Message}</p>";

            try
            {
                // 1. Send to your inbox
                await _emailService.SendEmailAsync("your@email.com", ContactForm.Subject, body, null);

                // 2. Send auto-reply to sender
                await _emailService.SendAutoReplyAsync(ContactForm.Email, ContactForm.Name);

                TempData["Success"] = "Message sent!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToPage();
        }

        public void OnGet()
    {
        //var dtos = _projectService.GetAllProjects();

        //Projects = dtos.Select(dto => new ProjectViewModel
        //{
        //    Title = dto.Title,
        //    TechStack = dto.TechStack,
        //    Description = dto.Description,
        //    GithubUrl = dto.GithubUrl,
        //    DemoUrl = dto.DemoUrl
        //}).ToList();
    }
}

}