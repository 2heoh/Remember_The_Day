using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class RepositoryMemoryTest
    {
        [Test]
        public void WhenAddingNewPerson_LoggerCalledOnce()
        {
            var mockLogger = new Mock<ILogger>();
            
            var repo = new PersonMemoryRepository(mockLogger.Object);
            
            repo.Add(new Person("Homer Simpson", "homer@simpson.com"));
            
            mockLogger.Verify( x => x.Write(It.IsAny<string>()), Times.Once() );
        }

        [Test]
        public void WhenGettingPersonList_LoggerCalledTwice()
        {
            var mockLogger = new Mock<ILogger>();

            var repo = new PersonMemoryRepository(mockLogger.Object);

            repo.GetList();

            mockLogger.Verify(x => x.Write(It.IsAny<string>()), Times.Once());
        }
        
        [Test]
        public void WhenRemovingPerson_LoggerHasNotCalled()
        {
            var mockLogger = new Mock<ILogger>();
            
            var repo = new PersonMemoryRepository(mockLogger.Object);
            
            repo.Remove(new Person("Homer Simpson", "homer@simpson.com"));
            
            mockLogger.Verify( x => x.Write(It.IsAny<string>()), Times.Never() );
        }
        
        [Test]
        public void WhenAddingRecipient_ThanCalledPersonRepoAdd()
        {
            var mockRepo = new Mock<IPersonRepo>();
            mockRepo.Setup(x => x.GetList()).Returns(new List<Person>());
            
            var mailing = new Mailing(mockRepo.Object);
            
            mailing.addRecipient(new Person("Homer Simpson", "homer@simpson.com"));
            
            mockRepo.Verify( x => x.Add(It.IsAny<Person>()), Times.Once());
        }  
    }

    
}