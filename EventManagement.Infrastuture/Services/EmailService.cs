using EventManagement.Application.Interface;
using EventManagement.Application.Model;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EventManagement.Infrastuture.Services;

public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<bool> SendEmailAsync(Email email)
    {
        using SmtpClient smtpClient = new(_emailSettings.Client, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.AppKey),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true
        };
        MailMessage mailMessage = new()
        {
            From = new MailAddress(_emailSettings.FromAddress!, _emailSettings.FromName),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email.To!);
        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            return (true);
        }
        catch (Exception)
        {
            return (false);
        }
    }
}
