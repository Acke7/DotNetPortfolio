using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Portfolio
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, List<IFormFile> attachments)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            using (var smtp = new SmtpClient
            {
                Host = smtpSettings["Server"],
                Port = int.Parse(smtpSettings["Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
            })
            {
                using (var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                })
                {
                    mailMessage.To.Add(toEmail);

                    // Add attachments
                    if (attachments != null)
                    {
                        foreach (var attachment in attachments)
                        {
                            if (attachment.Length > 0)
                            {
                                using (var ms = new MemoryStream())
                                {
                                    await attachment.CopyToAsync(ms);
                                    var fileBytes = ms.ToArray();
                                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(fileBytes), attachment.FileName));
                                }
                            }
                        }
                    }

                    try
                    {
                        await smtp.SendMailAsync(mailMessage);
                        Console.WriteLine("Email sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        // ✅ New: Auto-reply method
        public async Task SendAutoReplyAsync(string toEmail, string toName)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var replySubject = "Thanks for contacting me!";
            var replyBody = $@"
        <p>Hi {WebUtility.HtmlEncode(toName)},</p>
        <p>Thank you for reaching out to me. I appreciate your message and will get back to you as soon as possible.</p>
        <p>Best regards,<br><strong>Ahmad Alolabi</strong><br>.NET Developer</p>
    ";

            using (var smtp = new SmtpClient
            {
                Host = smtpSettings["Server"],
                Port = int.Parse(smtpSettings["Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
            })
            {
                using (var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["SenderEmail"], smtpSettings["SenderName"]),
                    Subject = replySubject,
                    Body = replyBody,
                    IsBodyHtml = true
                })
                {
                    mailMessage.To.Add(toEmail);

                    // ✅ Add this line to make sure it reaches the sender:
                    mailMessage.ReplyToList.Add(new MailAddress(smtpSettings["SenderEmail"]));

                    try
                    {
                        await smtp.SendMailAsync(mailMessage);
                        Console.WriteLine("Auto-reply email sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending auto-reply email: {ex.Message}");
                        throw;
                    }
                }
            }
        }

    }
}
