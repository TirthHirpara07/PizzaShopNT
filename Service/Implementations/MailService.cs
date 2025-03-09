using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;

namespace Service.Implementations;

public class MailService : IMailService
{
    private readonly IConfiguration _config;

    public MailService( IConfiguration config){
         _config = config;
    }
    
    public void SendEmail(string toemail, string emailBody)
    {
        try
        {
            string? smtpEmail = _config.GetValue<string>("SMTPCredentials:Email");
            string? smtpappPass = _config.GetValue<string>("SMTPCredentials:AppPass");
            string? smtphost = _config.GetValue<string>("SMTPCredentials:Host");

            SmtpClient smtpclient = new SmtpClient(smtphost)
            {
                Port = 587,
                Credentials = new NetworkCredential(smtpEmail, smtpappPass),
                EnableSsl = true,

            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(smtpEmail),
                Subject = "Reset Password from PizzaShop",
                Body = emailBody,
                IsBodyHtml = true,

            };
            mail.To.Add(toemail);
            smtpclient.Send(mail);
            return;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public async Task<string> GetEmailBodyAsync(string templateName)
   {
      string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MailTemplate", templateName);
      return await File.ReadAllTextAsync(templatePath);
   }

}
