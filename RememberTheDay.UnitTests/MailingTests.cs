using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    class LoggerStub : ILogger
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
            // arrange
            var repo = new PersonMemoryRepository();
            var mailing = new Mailing(repo, new LoggerStub());
            var homer = new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 2, 23));
                        
            // act
            mailing.AddRecipient(homer);

            var expected = new List<Person>();
            expected.Add(homer);
            
            // assert 
            CollectionAssert.AreEqual(repo.GetList(), expected);
        }

        [Test]
        public void WhenAddingSameRecipientTwice_ThanThrowsExceptionAlreadyExists()
        {
            var repo = new PersonMemoryRepository();
            var mailing = new Mailing(repo, new LoggerStub());
            var homer = new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 2, 23));

            mailing.AddRecipient(homer);

            Exception ex = Assert.Throws<Exception>(
                delegate { mailing.AddRecipient(homer); } 
            );
            
            Assert.That(ex.Message, Is.EqualTo("already exists") );
        }

        [Test]
        public void WhenGettingListOfCelebrantsNextWeek_AndWeHaveOnlyOneBirthDay_ThenWeGetOnlyOnePerson()
        {
            var repo = new PersonMemoryRepository();
            var mailing = new Mailing(repo, new LoggerStub());
            
            var homer = new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 2, 23));
            var marge = new Person("Marge", "m.simpson@fox.com", new DateTime(1973, 3, 8));
            
            mailing.AddRecipient(homer);
            mailing.AddRecipient(marge);

            var result = mailing.GetNextWeekCelebrants(new DateTime(2017, 2, 20));
            
            Assert.AreEqual(new List<Person> {homer}, result);
        }
        
        [Test]
        public void WhenAddingNewPerson_LoggerWritesHisNameAndBirthDayOnce()
        {
            var mockLogger = new Mock<ILogger>();
            var repo = new PersonMemoryRepository();

            var mailing = new Mailing(repo, mockLogger.Object);
            
            mailing.AddRecipient(new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 2, 23)));
            
            mockLogger.Verify( x => x.Write("added Person: Homer 02/23/1970 00:00:00"), Times.Once() );
        }        

        [Test]
        public void WhenGettingNextWeekCelebrants_ThenLoggerOnceWritesThatThereIsTwoPersons()
        {
            var mockLogger = new Mock<ILogger>();

            var mailing = new Mailing(new PersonMemoryRepository(), mockLogger.Object);
            
            mailing.AddRecipient(new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 1, 1)));
            mailing.AddRecipient(new Person("Marge", "m.simpson@fox.com", new DateTime(1973, 12, 31)));

            mailing.GetNextWeekCelebrants(new DateTime(2017, 12, 30));

            mockLogger.Verify(x => x.Write("found 2 person(s)"), Times.Once());
        }

        
        [Test]
        public void WhenGettingListOfPersons_AndListIsEmpty_ThenLoggerWritesThatNoPersonFound()
        {
            var mockLogger = new Mock<ILogger>();
            var mockFileSystem = new Mock<MacFileSystem>();
            mockFileSystem.Setup(x => x.GetFiles()).Returns(new string[0]);
            var repo = new PersonRepository(mockLogger.Object, mockFileSystem.Object);
            
            var mailing = new Mailing(repo, mockLogger.Object);

            mailing.GetNextWeekCelebrants(new DateTime(1980, 1, 1));
            
            mockLogger.Verify(x => x.Write("found 0 person(s)"));
        }

        [Test]
        public void WhenGettingListOfPersons_AndThereIsOnePerson_ThenLoggerWritesHisEmailNameAndBirthday()
        {
            var mockLogger = new Mock<ILogger>();
            var mockFileSystem = new Mock<MacFileSystem>();
            
            mockFileSystem.Setup(x => x.GetFiles()).Returns(new []{"/path/to/personfile.txt"});
            mockFileSystem.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new []{"h.simpson@fox.com", "Homer", "1970/01/01"});
            
            var repo = new PersonRepository(mockLogger.Object, mockFileSystem.Object);
            
            var mailing = new Mailing(repo, mockLogger.Object);

            mailing.GetNextWeekCelebrants(new DateTime(1980, 1, 1));
            
            mockLogger.Verify(x => x.Write("found Person - email: h.simpson@fox.com name: Homer birthday: 1970/01/01"));
        }        
        
        
        [Test]
        public void WhenAddingNewPerson_AndListIsEmpty_ThenLoggerWritesFileName()
        {

            var homer = new Person("Homer", "h.simpson@fox.com", new DateTime(1970, 1, 1));
            
            var mockLogger = new Mock<ILogger>();
            var mockFileSystem = new Mock<MacFileSystem>();

            mockFileSystem.Setup(x => x.SaveToFile(It.IsAny<string>(), new string[0]));
            mockFileSystem.Setup(x => x.GetFiles()).Returns(new string[0]);
            mockFileSystem.Setup(x => x.MakeFileName(It.IsAny<string>())).Returns("/some/path.txt");
            
            var repo = new PersonRepository(mockLogger.Object, mockFileSystem.Object);
            
            var mailing = new Mailing(repo, mockLogger.Object);

            mailing.AddRecipient(homer);
            
            mockLogger.Verify(x => x.Write("filename: /some/path.txt"));
        }
    }
}