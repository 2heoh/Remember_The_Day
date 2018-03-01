using System;
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

            Exception ex = Assert.Throws<Exception>(
                delegate { mailing.addRecipient(homer); } 
            );
            
            Assert.That(ex.Message, Is.EqualTo("already exists") );
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

            var expected = new List<Person>();
            expected.Add(secondPerson);
            
            Assert.AreEqual(mailing.getRecipients(firstPersonHasBirtDay), expected);
        }

}
}