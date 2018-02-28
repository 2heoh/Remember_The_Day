using System;
using System.Collections.Generic;


namespace RememberTheDay
{
    class Program
    {
        static void Main(string[] args)
        {
            Mailing ml = new Mailing();

            
            // adding two persons to the mailing list
            ml.addRecipient(new Person("Mary Jane", "mary.jane@myorg.com"));
            ml.addRecipient(new Person("Kate Stark", "kate.stark@myorg.com"));

            // and John
            Person john = new Person("John Doe", "john.doe@myorg.com");
            ml.addRecipient(john);
            
            // who has a Birth Day today 
            List<Person> recipients = ml.getRecipients(john);

            // get only emails
            List<string> emails = new List<string>();
            foreach (var r in recipients)
            {
                emails.Add(r.Email);
            }

            // Let's see what we have
            Console.WriteLine("Mail list:");
            Console.WriteLine(String.Join(", ", emails));
        }
    }

    public class Person
    {
        public string Name;
        public string Email;

        public Person(string _Name, string _Email)
        {
            Name = _Name;
            Email = _Email;
        }
    }
    
    public class Mailing
    {
        public List<Person> MailingList = new List<Person>();

        public void addRecipient(Person person)
        {
            MailingList.Add(person);
        }

        public List<Person> getRecipients(Person celebrant)
        {
            var i = MailingList.FindIndex(x => celebrant.Name == x.Name);
            
            if (i >= 0)
            {
                MailingList.RemoveAt(i);    
            }
            
            return MailingList;
        }

        public void Send (string title, string message)
        {

            EmailSender sender = new EmailSender(); 
            
            MailingList.ForEach( x => sender.Send(x.Email, title, message));
        }
        
    }
}