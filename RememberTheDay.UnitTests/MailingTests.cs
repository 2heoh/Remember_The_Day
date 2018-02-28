using System.Collections.Generic;
using NUnit.Framework;


namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class MailingTests
    {

        private List<Person> singlePersonList(Person p)
        {
            List<Person> l = new List<Person>();
            l.Add(p);
            return l;
        }
        
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
            Person p = new Person("Homer Simpson", "homer@simpson.com");
            
            // Act
            m.addRecipient(p);
            
            
            // Assert
            CollectionAssert.AreEqual(m.MailingList, singlePersonList(p));
        }
        
        
        [Test]
        public void WhenCreatingWithOnePerson_AndHeHasBirthDay_ThenListIsEmpty()
        {
            // Arrange
            Mailing m = new Mailing();
            Person p = new Person("Marge Simpson", "marge@simpson.com");
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
            Person p = new Person("Homer Simpson", "homer@simpson.com");
            m.addRecipient(p);
            
            // Act
            m.getRecipients(new Person("Marge Simpson", "marge@simpson.com"));
            
            // Assert
            CollectionAssert.AreEqual(m.MailingList, singlePersonList(p));
        } 
        
        
        [Test]
        public void WhenCreatingWithTwoPersons_AndOneOfThemHasBirthDay_ThenListHasSecondPerson()
        {
            // Arrange
            Mailing m = new Mailing();
            Person p1 = new Person("Homer Simpson", "homer@simpson.com");
            m.addRecipient(p1);
            Person p2 = new Person("Marge Simpson", "marge@simpson.com");
            m.addRecipient(p2);
            
            // Act
            m.getRecipients(p1);
                        
            // Assert
            CollectionAssert.AreEqual(m.MailingList, singlePersonList(p2));
        }        
    }
}