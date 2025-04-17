

namespace Tennis.Dominio.Marcador;

public interface ICommandHandler<Tcommand>
{
    Task Handler(Tcommand command);
}
