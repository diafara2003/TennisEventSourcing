
namespace Tennis.Dominio.Marcador;

public interface IEventStore
{
    void AppendEvent(Guid aggregateId, object eventData);
    Task saveChangesAsync();
    Task<TAggregateRoot?> GetAggregateRoot<TAggregateRoot>(Guid aggregateId) where TAggregateRoot : AggegateRoot, new();
}
