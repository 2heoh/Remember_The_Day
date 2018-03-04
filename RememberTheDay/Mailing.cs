using System;
using System.Collections.Generic;
using System.Linq;

namespace RememberTheDay
{
    public class Mailing
    {
        private IPersonRepo repo;
        private ILogger logger;

        public Mailing(IPersonRepo _repo, ILogger _logger)
        {
            repo = _repo;
            logger = _logger;
        }

        public void AddRecipient(Person person)
        {
            
            if (!repo.GetList().Contains(person)) 
            {
                repo.Add(person);
                logger.Write(String.Format("added {0}", person));
            }
            else
            {
                throw new Exception("already exists");
            }
        }

        public List<Person> GetNextWeekCelebrants(DateTime today)
        {            
            var recipientList = repo.GetList();
            logger.Write(String.Format("found {0} person(s)", recipientList.Count));
            return recipientList.FindAll(
                x => (x.BirthDay.AddYears(today.Year - x.BirthDay.Year) - today).TotalDays <= 7);
        }
    }
}

