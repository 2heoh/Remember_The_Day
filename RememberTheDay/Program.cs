using System;
using System.Collections.Generic;


namespace RememberTheDay
{
    class Program
    {
        static void Main(string[] args)
        {

            var repo = new PersonMemoryRepository(new ConsoleLogger());
            
            Mailing ml = new Mailing(repo);

            var log = new ConsoleLogger();
            
            var mary = new Person("Mary Jane", "mary.jane@myorg.com");
            var kate = new Person("Kate Stark", "kate.stark@myorg.com");

            ml.addRecipient(mary);
            ml.addRecipient(kate);

            var mailingList = ml.getRecipients(mary);
        }
    }


}