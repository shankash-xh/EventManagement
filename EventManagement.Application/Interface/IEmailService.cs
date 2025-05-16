using EventManagement.Application.Model;

namespace EventManagement.Application.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
