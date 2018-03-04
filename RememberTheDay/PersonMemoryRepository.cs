using System.Collections.Generic;
using System.Linq;

namespace RememberTheDay
{

    public class PersonMemoryRepository : IPersonRepo
    {

        private List<Person> personList = new List<Person>();

        public void Add(Person p)
        {
            personList.Add(p);
        }

        public List<Person> GetList()
        {
            return personList;
            
        }

        public void Remove(Person p)
        {
            personList.FirstOrDefault(x => x.Email == p.Email);
        }
    }
}