
namespace Tennis.Dominio.Test;

public class TestStore : IEventStore
{
    private readonly Dictionary<Guid, List<object>> _previoudEvents = new();
    private readonly Dictionary<Guid, List<object>> _newEvents = new();

    public void AppendPreviousEvent(Guid aggregateId, object[] eventData)
    => _previoudEvents.Add(aggregateId, eventData.ToList());


    public List<object> GetNewEvents(Guid aggregateId)
        => _newEvents.GetValueOrDefault(aggregateId, []);

    public void AppendEvent(Guid aggregateId, object eventData)
    {
        var eventos = _newEvents.GetValueOrDefault(aggregateId, []);
        eventos.Add(eventData);
        _newEvents[aggregateId] = eventos;
    }

    public Task<TAggregateRoot?> GetAggregateRoot<TAggregateRoot>(Guid aggregateId) where TAggregateRoot : AggegateRoot, new()
    {
        if (_previoudEvents.ContainsKey(aggregateId) == false)
            return Task.FromResult<TAggregateRoot?>(null);

        TAggregateRoot aggregateRoot = new();

        foreach (var evento in _previoudEvents[aggregateId])
        {
            aggregateRoot.Apply((dynamic)evento);
        }

        return Task.FromResult<TAggregateRoot?>(aggregateRoot);
    }

    public Task saveChangesAsync()
    {
        throw new NotImplementedException();
    }
}