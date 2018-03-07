using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

namespace RememberTheDay.UnitTests.DSL
{
    public class MessageListBuilder
    {
        private readonly List<MyMailMessage> _messageList = new List<MyMailMessage>();
        private string exclude;
        private int elementNumber = 0;

        public MessageListBuilder(List<MyMailMessage> _ml)
        {
            _messageList = _ml;
        }

        public MessageListBuilder First()
        {
            elementNumber = 0;
            return this;
        }

        public MessageListBuilder Number(int number)
        {
            elementNumber = number-1;
            return this;
        }

        public bool NotContainInAddress(string email)
        {
            var message = new MyMailMessage(); 
            
            for (var i = 0; i <= elementNumber; i++)
            {
                message = _messageList.FirstOrDefault();
                _messageList.RemoveAt(0);
            }

            return !message.SendTo.Contains(email);
        }

        
        public bool ContainEmailsAllExcept(string email)
        {
            var emails = The.Simpsons().ConvertAll(x => x.Email);
            emails.Remove(email);
            
            var res = _messageList.ElementAt(elementNumber).SendTo;
            
            return  res.ToHashSet().SetEquals(emails.ToHashSet());
        }
    }
}