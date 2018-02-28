using System.Collections.Generic;
using System.Linq;

namespace RememberTheDay
{

    public class PersonMemoryRepository : IPersonRepo
    {
        private ILogger _logger;

        private List<Person> personList = new List<Person>();
         
        public PersonMemoryRepository(ILogger log)
        {
            _logger = log;
        }   

        public void Add(Person p)
        {
            
            personList.Add(p);
            _logger.Write("Person name: " + p.Name);
        }

        public List<Person> GetList()
        {

            _logger.Write("got " + personList.Count + " person(s)");
    
            return personList;
            
        }

        public void Remove(Person p)
        {
            personList.FirstOrDefault(x => x.Email == p.Email);
        }
    }
}