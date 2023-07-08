namespace ApplicationLayer.Services.Interfaces;

public interface IMessageSourceService
{
    Guid AddEmail(string login);
    Guid AddPhone(string number);
    Guid AddMessenger(string userId);
}