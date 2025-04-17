namespace Tennis.EventStore;

public class MasterEventStore(IDocumentSession session, IQuerySession querySession)
    : IEventStore
{
    public void AppendEvent(Guid aggregateId, object eventData)
    {
        session.Events.Append(aggregateId, eventData);
    }

    public Task<TAggregateRoot?> GetAggregateRoot<TAggregateRoot>(Guid aggregateId)
        where TAggregateRoot : AggegateRoot, new()
    {
        return querySession.Events.AggregateStreamAsync<TAggregateRoot>(aggregateId);
    }

    public Task saveChangesAsync()
    {
        return session.SaveChangesAsync();
    }
}
