
namespace Tennis.Dominio;

public interface ICommandRouter
{
    public Task InvokeAsync<TCommand>(TCommand comand) where TCommand : class;
}
