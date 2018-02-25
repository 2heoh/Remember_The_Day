using System;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class MailingTests
    {

        [Test]
        public void WhenCreating_AndNoPersonsAddded_ThanHasNoPersons()
        {
            // Arrange
            Mailing m = new Mailing();
            
                        
            // Assert
            Assert.AreEqual(m.MailingList.Count, 0);
        }
        
        
        [Test]
        public void WhenCreating_AndOnePersonAdded_ThenHasOnlyOnePerson()
        {
            // Arrange
            Mailing m = new Mailing();
            
            // Act
            m.addRecipient(new Person("a", "b"));
            
            
            // Assert
            Assert.AreEqual(m.MailingList.Count, 1);
        }
        
        
        [Test]
        public void WhenCreatingWithOnePerson_AndHeHasBirthDay_ThenListIsEmpty()
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
        public void WhenCreatingWithOnePerson_AndAnotherGuyHasBirthDay_ThenListHasOnePerson()
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
        public void WhenCreatingWithTwoPersons_AndOneOfThemHasBirthDay_ThenListHasSecondPerson()
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