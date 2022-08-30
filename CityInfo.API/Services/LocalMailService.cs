namespace CityInfo.API.LocalMailService
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "HamdiNawfel@gmail;com";
        private string _mailFrom = "HamdiNawfel@gmail;com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }
    }
}
