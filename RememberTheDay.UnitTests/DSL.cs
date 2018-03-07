using System.Collections.Generic;
using RememberTheDay.UnitTests.DSL;

namespace RememberTheDay.UnitTests
{

    public static class The
    {
        public static PersonBuider Person(string name)
        {
            return new PersonBuider(name);
        }

        public static List<Person> Simpsons()
        {
            var personList = new List<Person>();
            personList.Add(Person("Homer").WithEmail("h.simpson@fox.com").Born(23.FebraryOf(1970)));
            personList.Add(Person("Marge").WithEmail("m.simpson@fox.com").Born(8.MarchOf(1973)));
            personList.Add(Person("Bart").WithEmail("bart.simpson@gmail.com").Born(1.JanuaryOf(1989)));
            personList.Add(Person("Lisa").WithEmail("lisa.simpson@gmail.com").Born(2.FebraryOf(1991)));
            personList.Add(Person("Meggie").WithEmail("meggie.simpson@gmail.com").Born(3.MarchOf(1995)));

            return personList;
        }

        public static MailingBuilder Mailing()
        {
            return new MailingBuilder();
        }

        public static RepoBuilder Repo()
        {
            return new RepoBuilder();
        }

        public static MacFileSystemBuilder MacFileSystem()
        {
            return new MacFileSystemBuilder();
        }

        public static MessageListBuilder MessageList(List<MyMailMessage> mailList)
        {
            return new MessageListBuilder(mailList);
        }

        public static SenderBuilder Sender()
        {
            return new SenderBuilder();
        }
    }
}