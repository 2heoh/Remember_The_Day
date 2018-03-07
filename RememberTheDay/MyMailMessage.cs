namespace RememberTheDay
{
    public class MyMailMessage
    {
        public string Subject;
        public string Message;
        public string[] SendTo;

        public MyMailMessage() { }
        
        public MyMailMessage(string _subject, string _message, string[] _sendTo) 
        {
            Subject = _subject;
            Message = _message;
            SendTo = _sendTo;
        }

        public void Send()
        {
            
        }
    }

    public static class MailMesageFabric
    {
        public static MyMailMessage Create(Person person, string[] emails)
        {
            return new MyMailMessage(
                CreateSubject(person.Name),
                CreateBody(person),
                emails
            );
        }

        private static string CreateSubject(string name)
        {
            return $"Tada! {name} has birthday this week!";
        }

        private static string CreateBody(Person person)
        {
            return $"Guys! {person.Name} has birthday at {person.BirthDay}, let's buy him something!";
        }        
    } 
}