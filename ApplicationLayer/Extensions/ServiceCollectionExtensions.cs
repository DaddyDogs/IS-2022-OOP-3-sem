using ApplicationLayer.Services.Implementations;
using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<ISupervisorService, SupervisorService>();
        collection.AddScoped<IValidationService, ValidationService>();
        collection.AddScoped<IMessageSourceService, MessageSourceService>();

        return collection;
    }
}