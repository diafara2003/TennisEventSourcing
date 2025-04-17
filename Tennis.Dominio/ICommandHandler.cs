namespace Tennis.Dominio;

public interface ICommandHandler<Tcommand>
{
    Task Handler(Tcommand command);
}
