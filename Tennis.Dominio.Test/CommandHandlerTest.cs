using FluentAssertions;

namespace Tennis.Dominio.Test;

public abstract class CommandHandlerTest<Tcommand>()
{
    protected readonly Guid _aggregateId = Guid.NewGuid();

    protected TestStore eventStore = new();
    protected readonly string jugador1 = "Jugador1";
    protected readonly string jugador2 = "Jugador2";
    protected abstract ICommandHandler<Tcommand> Handler { get; }

    protected void Given(params object[] events)
        => eventStore.AppendPreviousEvent(_aggregateId, events);

    protected void When(Tcommand command)
        => Handler.Handler(command)
        .GetAwaiter()
        .GetResult();

    protected void Then(params object[] expectedEvents)
    {
        var newEvents = eventStore.GetNewEvents(_aggregateId);

        for (int i = 0; i < newEvents.Count(); i++)
        {
            newEvents[i].Should().BeOfType(expectedEvents[i].GetType());
            newEvents[i].Should().BeEquivalentTo(expectedEvents[i]);
        }
    }
}
