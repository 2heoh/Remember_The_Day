using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Xml.Schema;


namespace RememberTheDay
{
    class Program
    {
        static void Main(string[] args)
        {
    
            var repo = new PersonRepository(new MacFileSystem(), new ConsoleLogger());
            
            Mailing ml = new Mailing(
                repo, 
                new ConsoleLogger(), 
                new EmailSender(new EmailClient()) 
            );
            
            var mary = new Person("Mary Jane", "mary.jane@myorg.com", new DateTime(1980, 01, 01));
            var kate = new Person("Kate Stark", "kate.stark@myorg.com", new DateTime(1984, 12, 31));

            ml.AddRecipient(mary);
            ml.AddRecipient(kate);
            
            DateTime today = new DateTime().Date;

            ml.GetNextWeekCelebrants(today);
            
        }
    }


}