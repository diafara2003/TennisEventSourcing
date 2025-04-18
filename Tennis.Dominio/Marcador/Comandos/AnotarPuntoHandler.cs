namespace Tennis.Dominio.Marcador.Comandos;

public record SumarPunto(Guid IdMarcador, string jugador);
public class AnotarPuntoHandler(IEventStore eventStore) : ICommandHandler<SumarPunto>
{
    public async Task Handle(SumarPunto command)
    {
        var marcador = await eventStore.GetAggregateRoot<Marcador>(command.IdMarcador);

        if (marcador is null)
            throw new InvalidOperationException("Marcador no existente");

        if(marcador.Finalizado)
            throw new InvalidOperationException("La partida ha terminado");

        marcador.SumarMarcador(command.jugador);

        if (marcador.MarcadorTerminado())
        {
            var marcadorTerminado = new Eventos.MarcadorFinalizado(command.IdMarcador, marcador.Puntaje);
            eventStore.AppendEvent(command.IdMarcador, marcadorTerminado);
        }
        else
        {
            var puntoAnotado = new Eventos.PuntoSumado(command.IdMarcador, command.jugador, marcador.Puntaje);

            eventStore.AppendEvent(command.IdMarcador, puntoAnotado);
        }           

    }

}
