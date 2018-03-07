using System;

namespace RememberTheDay
{

    public class Person
    {
        public readonly string Name;
        public string Email;
        public DateTime BirthDay;

        public Person(string _name)
        {
            Name = _name;
        }

        public Person(string _Name, string _email)
        {
            Name = _Name;
            Email = _email;
        }

        public void SetEmail(string _email)
        {
            Email = _email;
        }

        public void SetBirthDay(DateTime _birthDate)
        {
            BirthDay = _birthDate;
        }

        public Person(string _Name, string _Email, DateTime _birthDay)
        {
            Name = _Name;
            Email = _Email;
            BirthDay = _birthDay;
        }
        
        public override string ToString()
        {
            return $"Person: {Name} {BirthDay.Day:D2}.{BirthDay.Month:D2}.{BirthDay.Year}";
        }
    }
}