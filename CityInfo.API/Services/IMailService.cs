namespace CityInfo.API.LocalMailService;

public interface IMailService
{
    void Send(string subject, string message);
}