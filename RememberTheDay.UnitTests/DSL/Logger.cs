using Moq;

namespace RememberTheDay.UnitTests.DSL
{
    class LoggerStub : ILogger
    {
        public void Write(string message)
        {

        }
    }    

    public static class TheLogger
    {
        public static Mock<ILogger> Mock()
        {
            return new Mock<ILogger>();
        }

        public static ILogger Stub()
        {
            return new LoggerStub();
        }

        public static ILogger STDIO()
        {
            return new ConsoleLogger();
        }
        
    }
}