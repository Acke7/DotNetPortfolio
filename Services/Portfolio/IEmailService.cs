using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Portfolio
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, List<IFormFile> attachments);
        Task SendAutoReplyAsync(string toEmail, string toName);
    }

}
