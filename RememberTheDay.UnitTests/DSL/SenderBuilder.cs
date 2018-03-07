using System;
using Moq;

namespace RememberTheDay.UnitTests.DSL
{

    public class SenderBuilder
    {
        public Mock<ISender> _sender;
        
        public SenderBuilder Email()
        {
            _sender = new Mock<ISender>();
            return this;
        }

        public void SentTimes(int times)
        {
            _sender.Verify(x => x.Send(It.IsAny<MyMailMessage>()), Times.Exactly(times));
        }

        public ISender Please()
        {
            return _sender.Object;
        }
    }
}