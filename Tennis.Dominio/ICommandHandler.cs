namespace Tennis.Dominio;

public interface ICommandHandler<Tcommand>
{
    Task Handle(Tcommand command);
}
