using System;
using System.Collections.Generic;
using System.Linq;

namespace RememberTheDay
{
    public class Mailing
    {
        private IPersonRepo repo;

        public Mailing(IPersonRepo _repo)
        {
            repo = _repo;
        }

        public void addRecipient(Person person)
        {
            if (!repo.GetList().Contains(person)) 
            {
                repo.Add(person);
            }
            else
            {
                throw new Exception("already exists");
            }
        }

        public List<Person> getRecipients(Person celebrant)
        {
            var recipients = repo.GetList();

            return recipients.Where(x => x.Email != celebrant.Email).ToList();
        }
    }
}

