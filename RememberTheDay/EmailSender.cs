using System.Net.Mail;
using System.Reflection;

namespace RememberTheDay
{
    public class EmailSender
    {
        
        private SmtpClient _client;
        private string _host = "smtp.gmail.com";
        private int _port = 25;
        private string emailFrom = "you@yourcompany.com";

        public EmailSender()
        {
            _client = new SmtpClient();
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            _client.Host = _host;    
            _client.Port = _port;
            
        }
        
        public EmailSender(string host, int port)
        {
            _client = new SmtpClient();
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            
            _client.Host = host;    
            _client.Port = port;
            
        }

        public void Send(string emailTo , string subject, string body)
        {
            MailMessage mail = new MailMessage(emailFrom, emailTo);

            mail.Subject = subject;
            mail.Body = body;
            _client.Send(mail);

        }
        
    }
}