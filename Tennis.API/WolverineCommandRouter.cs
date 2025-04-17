using Tennis.Dominio;
using Wolverine;

namespace Tennis.API;

public class WolverineCommandRouter(IMessageBus messageBus) : ICommandRouter
{
    public Task InvokeAsync<TCommand>(TCommand command) where TCommand : class
    {
        return messageBus.InvokeAsync<TCommand>(command);
    }
}
