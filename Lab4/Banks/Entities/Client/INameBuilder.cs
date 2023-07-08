namespace Banks.Entities.Client;

public interface INameBuilder
{
    public IClientBuilder WithName(string firstName, string lastName);
}