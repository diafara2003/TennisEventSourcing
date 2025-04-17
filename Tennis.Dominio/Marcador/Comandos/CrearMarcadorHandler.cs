namespace Tennis.Dominio.Marcador.Comandos;

public record CrearMarcador(Guid IdMarcador,string puntaje);
public class CrearMarcadorHandler(IEventStore eventStore) : ICommandHandler<CrearMarcador>
{
    public Task Handler(CrearMarcador command)
    {
        var marcadorCreado = new Eventos.MarcadorCreado(command.IdMarcador,command.puntaje);

        eventStore.AppendEvent(marcadorCreado.IdMarcador, marcadorCreado);

        return Task.CompletedTask;
    }
}
