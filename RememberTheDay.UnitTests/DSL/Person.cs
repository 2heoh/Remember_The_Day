using System;

namespace RememberTheDay.UnitTests.DSL
{
    public class PersonBuider
    {
        private Person _person;
        private string _name;
        private string _email;
        
        public PersonBuider(string name)
        {
            _person = new Person(name);
        }

        public PersonBuider WithEmail(string email)
        {
            _person.SetEmail(email);
            return this;
        }

        public Person Born(DateTime date)
        {
            _person.SetBirthDay(date);
            return this;
        }
        
        public static implicit operator Person(PersonBuider buider)
        {
            return buider._person;
        }
        
        

    }
}