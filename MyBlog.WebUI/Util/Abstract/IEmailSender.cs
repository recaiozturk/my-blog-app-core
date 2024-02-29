namespace MyBlog.WebUI.Util.Abstract
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email,string clientName, string clientEmail, string subject, string message);
    }
}
