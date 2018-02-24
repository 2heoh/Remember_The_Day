using System;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class MailingHas
    {

        [Test]
        public void WhenCreatingMailing_AndNoPersons_ThanMailingHasNoPersons()
        {
            // Arrange
            Mailing m = new Mailing();
            
                        
            // Assert
            Assert.AreEqual(m.MailingList.Count, 0);
        }
        
        
        [Test]
        public void OnePerson_WhenAddedOnlyOnePerson()
        {
            // Arrange
            Mailing m = new Mailing();
            
            // Act
            m.addRecipient(new Person("a", "b"));
            
            
            // Assert
            Assert.AreEqual(m.MailingList.Count, 1);
        }
        
        
        [Test]
        public void OnePerson_AndHeHasBirthDay_ThenListIsEmpty()
        {
            // Arrange
            Mailing m = new Mailing();
            Person p = new Person("a", "b");
            m.addRecipient(p);
            
            
            // Act
            m.getRecipients(p);
            
            
            // Assert
            Assert.AreEqual(m.MailingList.Count, 0);
        }  
        
        [Test]
        public void AddedOnePerson_AndAnotherGuyHasBirthDay_ThenListHasOnePerson()
        {
            // Arrange
            Mailing m = new Mailing();
            Person p = new Person("a", "b");
            m.addRecipient(p);
            
            
            // Act
            m.getRecipients(new Person("c", "d"));
            
            
            // Assert
            Assert.AreEqual(m.MailingList.Count, 1);
        } 
        
        
        [Test]
        public void AddedTwoPersons_AndOneOfThemHasBirthDay_ThenListHasAnotherPerson()
        {
            // Arrange
            Mailing m = new Mailing();
            Person p = new Person("a", "a");
            m.addRecipient(p);
            p = new Person("b", "b");
            m.addRecipient(p);
            
            // Act
            m.getRecipients(new Person("a", "a"));
                        
            // Assert
            Assert.AreEqual(m.MailingList.ToArray()[0].Name, "b");
        }        
    }
}