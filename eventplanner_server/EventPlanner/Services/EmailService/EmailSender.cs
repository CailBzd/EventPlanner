public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string aEmail, string aSubject, string aMessage)
    {
        // Implement your email sending logic here
        return Task.CompletedTask;
    }
}
