

using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace Tennis.EventStore;

public static class MartenEventStoreExtensions
{
    public static MartenServiceCollectionExtensions.MartenConfigurationExpression AddMartenConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
     return   services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("TenisEventStore")!);
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
