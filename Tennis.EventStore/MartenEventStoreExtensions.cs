

using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace Tennis.EventStore;

public static class MartenEventStoreExtensions
{
    public static MartenServiceCollectionExtensions.MartenConfigurationExpression AddMartenConfiguration(this IServiceCollection services, 
        string connectionString
        )
    {
     return   services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.UseSystemTextJsonForSerialization();
            options.Events.StreamIdentity = StreamIdentity.AsGuid;
            options.AutoCreateSchemaObjects = AutoCreate.All;
        }).UseLightweightSessions();

        
    }

    public static void AddMartenEventStore(this IServiceCollection service)
    {
        service.AddScoped<Dominio.IEventStore, MasterEventStore>();
    }
}
