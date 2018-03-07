using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RememberTheDay.UnitTests.DSL;

namespace RememberTheDay.UnitTests
{
    [TestFixture]
    public class MailingTests
    {

        [Test]
        public void WhenAddingRecipient_ThanRepoContainsThisPerson()
        {
            // arrange
            var homer = The.Person("Homer")
                           .WithEmail("h.simpson@fox.com")
                           .Born(23.FebraryOf(1970));
            
            var mailing = The.Mailing().StoreInMemory().WithLoggerStub().Please();
            
            // act
            mailing.AddRecipient(homer);

            // assert 
            CollectionAssert.AreEqual(new List<Person> {homer}, mailing.Repo.GetList());
        }

        [Test]
        public void WhenAddingSameRecipientTwice_ThanThrowsExceptionAlreadyExists()
        {
            // arrange
            var homer = The.Person("Homer")
                           .WithEmail("h.simpson@fox.com")
                           .Born(23.FebraryOf(1970));

            var repo = The.Repo().InMemory().Please();
            
            var mailing = The.Mailing()
                             .StoreIn(repo)
                             .With(TheLogger.Stub()).Please();
            
            mailing.AddRecipient(homer);
            
            // act & assert
            Assert.Throws<AlreadyExistsException>(() =>  mailing.AddRecipient(homer));

        }

        [Test]
        public void WhenGettingListOfCelebrantsNextWeek_AndWeHaveOnlyOneBirthDay_ThenWeGetOnlyOnePerson()
        {
            // arrange
            var repo = The.Repo().InMemory().Please();
            
            var mailing = The.Mailing().StoreIn(repo).With(TheLogger.Stub()).Please();

            var homer = The.Person("Homer")
                           .WithEmail("h.simpson@fox.com")
                           .Born(23.FebraryOf(1970));
            
            mailing.AddRecipient(homer);

            var marge = The.Person("Marge")
                           .WithEmail("m.simpson@fox.com")
                           .Born(8.MarchOf(1973));                
            
            mailing.AddRecipient(marge);            

            var bart = The.Person("Bart")
                .WithEmail("bart.simpson@gmail.com")
                .Born(19.MarchOf(1989));                
            
            mailing.AddRecipient(bart);            
            
            // act
            var result = mailing.GetNextWeekCelebrants(20.FebraryOf(2017));

            // assert
            Assert.AreEqual(new List<Person> {homer}, result);
        }

        [Test]
        public void WhenAddingNewPerson_LoggerWritesHisNameAndBirthDayOnce()
        {
            var mockLogger = TheLogger.Mock();
            var mailing = The.Mailing().StoreInMemory().With(mockLogger.Object).Please();

            var homer = The.Person("Homer")
                           .WithEmail("h.simpson@fox.com")
                           .Born(23.FebraryOf(1970));
            
            // act
            mailing.AddRecipient(homer);

            mockLogger.Verify(x => x.Write("added Person: Homer 23.02.1970"), Times.Once());
        }

        [Test]
        public void WhenGettingNextWeekCelebrants_ThenLoggerOnceWritesThatThereIsTwoPersons()
        {
            var mockLogger = TheLogger.Mock();
            var mailing = The.Mailing().StoreInMemory().With(mockLogger.Object).Please();

            var homer = The.Person("Homer")
                .WithEmail("h.simpson@fox.com")
                .Born(1.FebraryOf(1970));

            var marge = The.Person("Marge")
                .WithEmail("m.simpson@fox.com")
                .Born(31.DecemberOf(1970));
            
            mailing.AddRecipient(homer);
            mailing.AddRecipient(marge);

            mailing.GetNextWeekCelebrants(30.DecemberOf(2017));

            mockLogger.Verify(x => x.Write("found 2 person(s)"), Times.Once());
        }


        [Test]
        public void WhenGettingListOfPersons_AndListIsEmpty_ThenLoggerWritesThatNoPersonFound()
        {
            var mockLogger = TheLogger.Mock();
            var repo = The.Repo().InMacFileSystem().Empty().With(mockLogger.Object).Please();
            
            var mailing = The.Mailing().StoreIn(repo).With(mockLogger.Object).Please();

            // act
            mailing.GetNextWeekCelebrants(1.JanuaryOf(1980));

            mockLogger.Verify(x => x.Write("found 0 person(s)"));
        }

        [Test]
        public void WhenGettingListOfPersons_AndThereIsOnePerson_ThenLoggerWritesHisEmailNameAndBirthday()
        {
            var mockLogger = TheLogger.Mock();
            
            var repo = The.Repo()
                          .InMacFileSystem()
                          .HasFile("/path/to/personfile.txt")
                          .InIt(new[] {"h.simpson@fox.com", "Homer", "1970/01/01"})
                          .With(mockLogger.Object)
                          .Please();

            var mailing = The.Mailing().StoreIn(repo).With(mockLogger.Object).Please();

            mailing.GetNextWeekCelebrants(1.JanuaryOf(1980));

            mockLogger.Verify(x => x.Write("found Person - email: h.simpson@fox.com name: Homer birthday: 1970/01/01"));
        }


        [Test]
        public void WhenAddingNewPerson_AndListIsEmpty_ThenLoggerWritesFileName()
        {

            var mockLogger = TheLogger.Mock();

            var repo = The.Repo()
                .InMacFileSystem()
                .Empty()
                .Write(new string[0])
                .ToFile("/some/path.txt")
                .With(mockLogger.Object)
                .Please();            

            var mailing = The.Mailing().StoreIn(repo).With(mockLogger.Object).Please();

            var homer = The.Person("Homer")
                .WithEmail("h.simpson@fox.com")
                .Born(1.FebraryOf(1970));
            
            // act
            mailing.AddRecipient(homer);

            // assert
            mockLogger.Verify(x => x.Write("filename: /some/path.txt"));
        }
        
        [Test]
        public void WhenOneBirthdayNextWeek_ThenWeGetMailMessageWithoutThatPersonInAddressList()
        {

            var repo = The.Repo()
                .InMemory()
                .With(TheLogger.Stub())
                .Please();            

            var mailing = The.Mailing()
                .StoreIn(repo)
                .With(TheLogger.Stub())
                .AddSimpsons()
                .Please();

            // act
            var forNextWeek = mailing.CreateBirthDayMessagesForNextWeek(4.MarchOf(2017));

            Assert.True(The.MessageList(forNextWeek).First().NotContainInAddress("m.simpson@fox.com"));
        }

        [Test]
        public void WhenTwoBirthDaysNextWeek_AndWeGetTwoLetters_ThenSecondHasAllAddressesExceptCelebrator()
        {
            var repo = The.Repo()
                .InMemory()
                .With(TheLogger.Stub())
                .Please();            

            var mailing = The.Mailing()
                .StoreIn(repo)
                .With(TheLogger.Stub())
                .AddSimpsons()
                .Please();    
            
            var forNextWeek = mailing.CreateBirthDayMessagesForNextWeek(2.MarchOf(2017));
            
            Assert.True(The.MessageList(forNextWeek).Number(2).ContainEmailsAllExcept("meggie.simpson@gmail.com"));
        }
        
        [Test]
        public void WhenTwoBirthDaysNextWeek_AndWeSendTwoLetters_ThenEmailSenderCalledTwice()
        {
            var logger = TheLogger.Stub();
            
            var repo = The.Repo()
                .InMemory()
                .With(logger)
                .Please();

            var Email = The.Sender().Email();
            
            var mailing = The.Mailing()
                .StoreIn(repo)
                .With(logger)
                .AddSimpsons()
                .SendBy(Email.Please())
                .Please();    
            
            //act
            mailing.GetMessagesAndSend(2.MarchOf(2017));
            
            // assert
            Email.SentTimes(2);
        }        
    }
}