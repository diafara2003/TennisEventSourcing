namespace Tennis.Dominio.Marcador.Comandos;

public record CrearMarcador(Guid IdMarcador,string puntaje);
public class CrearMarcadorHandler(IEventStore eventStore) : ICommandHandler<CrearMarcador>
{
    //El método debe llamarse Handle o HandleAsync, debe ser public, y debe recibir como primer parámetro el tipo de mensaje.
    public Task Handle(CrearMarcador command)
    {
        var marcadorCreado = new Eventos.MarcadorCreado(command.IdMarcador,command.puntaje);

        eventStore.AppendEvent(marcadorCreado.IdMarcador, marcadorCreado);

        return Task.CompletedTask;
    }
}

