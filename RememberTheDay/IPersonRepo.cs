using System.Collections.Generic;

namespace RememberTheDay
{
    public interface IPersonRepo
    {
        void Add(Person p);
        
        List<Person> GetList();
        
        void Remove(Person p); 
    }
    
}