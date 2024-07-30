using System.Net;
using System.Net.Mail;
using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Services
{
    public interface IMailService
    {
        Task<string> ComposeResetPasswordHTML(string email, string username, string otp);
        Task<string> ComposeVerifyEmailHTML(string email, string username, string code);
        Task SendEmail(string emailAddress, string subject, string bodyHtml);
    }
    public class MailService : IMailService
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public MailService(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ComposeResetPasswordHTML(string email, string username, string otp)
        {
            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", "reset-password.html");
            var htmlContent = File.ReadAllText(htmlPath);
            // Buat parser dan parse HTML
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(htmlContent);
            var queryString = new { email, otp };
            document.GetElementById("username").TextContent = $"Hello, {username}";
            document.GetElementById("otp-code").TextContent = otp;
            document.QuerySelector("a").SetAttribute("href", queryString.ToQueryString("https://api.dietary.cloud/user/verify-otp"));
            return document.ToHtml();
        }
        public async Task<string> ComposeVerifyEmailHTML(string email, string username, string code)
        {
            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html", "verify-email.html");
            var htmlContent = File.ReadAllText(htmlPath);
            // Buat parser dan parse HTML
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(htmlContent);
            var queryString = new { email, code };
            document.GetElementById("username").TextContent = $"Hello, {username}";
            document.QuerySelector("a").SetAttribute("href", queryString.ToQueryString("https://api.dietary.cloud/user/verify-email"));
            return document.ToHtml();
        }
        public async Task SendEmail(string emailAddress, string subject, string bodyHtml)
        {
            // Konfigurasi SMTP
            int smtpPort = 587;
            string smtpServer = _configuration["SmtpServer"];
            string smtpUsername = _configuration["SmtpUsername"];
            string smtpPassword = _configuration["SmtpPassword"];

            // Buat instance dari SmtpClient
            SmtpClient client = new(smtpServer, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword)
            };

            // Buat email
            MailMessage mailMessage = new()
            {
                From = new MailAddress("noreply@ismilelab-telu.com", "I-Smile Account"),
                IsBodyHtml = true,
                Subject = subject,
                Body = bodyHtml
            };
            mailMessage.To.Add(emailAddress);

            // Kirim email
            await client.SendMailAsync(mailMessage);
        }
    }
}
