using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Driftservice_Registration.Models
{
    public class MailService
    {
        public async Task<bool> SendMail(Contact contact)
        {
            try
            {
                var body = "<h4>Hej, {0}!</h4><hr/><p>Det här är ett testmail,</p><p>Vi hör av oss om det händer något.</p>";
                var message = new MailMessage();
                message.From = new MailAddress("testarn123123@gmail.com");
                message.To.Add(new MailAddress(contact.Email));
                message.Subject = "Driftservice";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = string.Format(body, contact.FirstName);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("testarn123123@gmail.com", "test#123");
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}