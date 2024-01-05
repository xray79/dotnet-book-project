using Microsoft.AspNetCore.Identity.UI.Services;

namespace book_project.utility;

public class EmailSender: IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
         return Task.CompletedTask;
    }
}