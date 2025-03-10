namespace Service.Interfaces;

public interface IMailService
{
    public void SendEmail(string toemail, string emailBody);
     public  Task<string> GetEmailBodyAsync(string templateName);
}
