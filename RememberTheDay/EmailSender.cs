using System;
using System.Net.Mail;

namespace RememberTheDay
{
    public class EmailClient {
        private string _host = "smtp.gmail.com";
        private int _port = 25;
        public string emailFrom = "you@yourcompany.com";

        private SmtpClient _client; 
        
        public EmailClient()
        {
            _client = new SmtpClient();
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            _client.Host = _host;    
            _client.Port = _port;

        }

        public void Send(MyMailMessage myMessage)
        {
            MailMessage message = new MailMessage(emailFrom, String.Join(", ", myMessage.SendTo));
            message.Subject = myMessage.Subject;
            message.Body = myMessage.Message;
            
        }
    }
    
    public class EmailSender : ISender
    {
        
        private ILogger Logger;
        private EmailClient Client;

        public EmailSender(EmailClient _client)
        {
            Client = _client;
        }
        
        public void setLogger(ILogger log)
        {
            Logger = log;
        }
        

        public void Send(MyMailMessage myMessage)
        {
            Logger.Write("Sending email");              
            Client.Send(myMessage);
        }
        
    }
}