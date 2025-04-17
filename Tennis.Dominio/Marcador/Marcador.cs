

namespace Tennis.Dominio.Marcador;

public class Marcador : AggegateRoot
{
    public Guid Id { get; private set; }
    private int PuntajeJugador1 { get; set; } = 0;
    private int PuntajeJugador2 { get; set; } = 0;
    public string Puntaje { get; set; } = "";
    public bool Finalizado { get; set; } = false;

    public void SumarMarcador(string quienGanoPunto)
    {
        if (quienGanoPunto == "Jugador1") PuntajeJugador1++;
        else PuntajeJugador2++;

        Puntaje = CalcularPuntaje();
    }
    private string CalcularPuntaje()
    {
        if (PuntajeJugador1 == 0 && PuntajeJugador2 == 0) return "Love";
        if (PuntajeJugador1 >= 3 && PuntajeJugador2 >= 3 && PuntajeJugador1 == PuntajeJugador2) return "Deuce";

        int diferenciaPuntaje = Math.Abs(PuntajeJugador1 - PuntajeJugador2);

        if ((PuntajeJugador1 >= 4 || PuntajeJugador2 >= 4) && diferenciaPuntaje == 1)
        {
            return PuntajeJugador1 > PuntajeJugador2 ? "Advantage-Jugador1" : "Advantage-Jugador2";
        }

        if ((PuntajeJugador1 >= 4 || PuntajeJugador2 >= 4) && diferenciaPuntaje >= 2)
        {
            return PuntajeJugador1 > PuntajeJugador2 ? "Game-Jugador1" : "Game-Jugador2";
        }

        return $"{PuntajeJugador1.ConvertirPuntoToMarcador()}-{PuntajeJugador2.ConvertirPuntoToMarcador()}";
    }

    public bool MarcadorTerminado()
    {
        if (PuntajeJugador1 >= 4 || PuntajeJugador2 >= 4)
        {
            int diferenciaPuntaje = Math.Abs(PuntajeJugador1 - PuntajeJugador2);
            return diferenciaPuntaje >= 2;
        }
        return false;
    }
    public void Apply(Eventos.MarcadorCreado @event)
    {
        Id = @event.IdMarcador;
        PuntajeJugador1 = 0;
        PuntajeJugador2 = 0;
        Puntaje = @event.puntaje;
    }

    public void Apply(Eventos.PuntoSumado @event)
    {
        if (@event.jugador == "Jugador1") PuntajeJugador1 += 1;
        else PuntajeJugador2 += 1;

        Puntaje = CalcularPuntaje();
    }

    public void Apply(Eventos.MarcadorFinalizado @event)
    {
        Finalizado = true;
    }

}
