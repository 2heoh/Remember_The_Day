using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RememberTheDay
{
    
    public class AlreadyExistsException: Exception
    {
        public AlreadyExistsException()
        {
        }

        public AlreadyExistsException(string message)
            : base(message)
        {
        }

        public AlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }    
    public class Mailing
    {
        public IPersonRepo Repo;
        public ILogger Logger;
        public ISender Sender;

        public Mailing(IPersonRepo _repo, ILogger _logger, ISender _sender)
        {
            Repo = _repo;
            Logger = _logger;
            Sender = _sender;
        }

        public void AddRecipient(Person person)
        {
            
            if (!Repo.GetList().Contains(person)) 
            {
                Repo.Add(person);
                Logger.Write(string.Format("added {0}", person));
            }
            else
            {
                throw new AlreadyExistsException();
            }
        }

        public List<Person> GetNextWeekCelebrants(DateTime today)
        {            
            var recipientList = Repo.GetList();
            Logger.Write(string.Format("found {0} person(s)", recipientList.Count));
            var nextWeek = recipientList.FindAll( 
                x=> (x.BirthDay.AddYears(today.Year - x.BirthDay.Year) - today).TotalDays <= 7 &&
                    (x.BirthDay.AddYears(today.Year - x.BirthDay.Year) - today).TotalDays > 0);
 
            return nextWeek;
        }

        public List<MyMailMessage> CreateBirthDayMessagesForNextWeek(DateTime today)
        {
            var messages = new List<MyMailMessage>();
            var personList = Repo.GetList();
            var celebrants = GetNextWeekCelebrants(today);
            
            foreach (var person in celebrants)
            {
                var recipients = personList.FindAll(x => x.Email != person.Email).ConvertAll(x => x.Email).ToArray();

                var message = MailMesageFabric.Create(person, recipients);
                
                messages.Add(message);   
            }

            Logger.Write($"created {messages.Count} message(s)");
            
            return messages.ToList();
        }

        public void GetMessagesAndSend(DateTime today)
        {
            
            Sender.setLogger(Logger);
            
            foreach (var message in CreateBirthDayMessagesForNextWeek(today))
            {
                Sender.Send(message);                
            }
        }
    }
}

