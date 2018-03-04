using System;

namespace RememberTheDay
{

    public class Person
    {
        public string Name;
        public string Email;
        public DateTime BirthDay;

        public Person(string _Name, string _Email, DateTime _birthDay)
        {
            Name = _Name;
            Email = _Email;
            BirthDay = _birthDay;
        }
        
        public override string ToString()
        {
            return "Person: " + Name + " " + BirthDay;
        }
    }
}