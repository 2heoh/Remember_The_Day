namespace RememberTheDay
{
    public interface ISender
    {
        void Send(MyMailMessage message);
        
        void setLogger(ILogger logger);
    }
}