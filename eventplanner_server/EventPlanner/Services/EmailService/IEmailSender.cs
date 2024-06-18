using System.Threading.Tasks;

public interface IEmailSender
{
    Task SendEmailAsync(string aEmail, string aSubject, string aMessage);
}