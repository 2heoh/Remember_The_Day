using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    class FakeLogger : ILogger
    {
        public void Write(string message)
        {
            
        }
    }

    [TestFixture]
    public class MailingTests
    {
        [Test]
        public void WhenAddingRecipient_ThanRepoContainsThisPerson()
        {
            var repo = new PersonMemoryRepository(new FakeLogger());
            var mailing = new Mailing(repo);

            var homer = new Person("Homer Simpson", "homer@simpson.com");

            mailing.addRecipient(homer);

            Assert.IsNotNull(repo.GetList().FirstOrDefault());
        }

        [Test]
        public void WhenAddingSameRecipientTwice_ThanRepoContainsOnlyOnePerson()
        {
            var repo = new PersonMemoryRepository(new FakeLogger());
            var mailing = new Mailing(repo);

            var homer = new Person("Homer Simpson", "homer@simpson.com");

            mailing.addRecipient(homer);
            mailing.addRecipient(homer);

            Assert.True(repo.GetList().Count == 1);
        }

        [Test]
        public void WhenAddindTwoDifferentPersons_AndOneOfThemHasBirthday_ThenMailingHasOnlySecondPerson()
        {
            var repo = new PersonMemoryRepository(new FakeLogger());
            var mailing = new Mailing(repo);
            
            var firstPersonHasBirtDay = new Person("Homer Simpson", "homer@simpson.com");
            var secondPerson = new Person("Marge Simpson", "marge@simpson.com");
            
            mailing.addRecipient(firstPersonHasBirtDay);
            mailing.addRecipient(secondPerson);

            Assert.True(
                !mailing.getRecipients(firstPersonHasBirtDay).Contains(firstPersonHasBirtDay)
                && mailing.getRecipients(firstPersonHasBirtDay).Contains(secondPerson)
                    );
        }

}
}