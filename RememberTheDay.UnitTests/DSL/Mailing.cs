using System.Collections.Generic;
using Moq;

namespace RememberTheDay.UnitTests.DSL
{
    
    public class MailingBuilder
    {
        private IPersonRepo _repo;
        private ILogger _logger;
        private ISender _sender;
        private List<Person> personList = new List<Person>();
        
        
        public MailingBuilder StoreIn(IPersonRepo repo)
        {
            _repo = repo;
            return this;
        }

        
        public MailingBuilder StoreInMemory()
        {
            _repo = new PersonMemoryRepository();
            return this;
        }
        
        public MailingBuilder With(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public MailingBuilder WithLoggerStub()
        {
            _logger = TheLogger.Stub();
            return this;
        }

        public MailingBuilder AddSimpsons()
        {
            foreach (var person in The.Simpsons())
            {
                personList.Add(person);
            }
            
            return this;
        }

        public MailingBuilder SendBy(ISender sender)
        {
            _sender = sender;
            return this;

        }
        
        public Mailing Please()
        {
            var mailing = new Mailing(_repo, _logger, _sender);
            foreach (var person in personList)
            {
                mailing.AddRecipient(person);
            }

            return mailing;
        }
    } 
}