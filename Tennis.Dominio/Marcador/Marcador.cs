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
        if (EsLove()) return "Love";
        if (EsDeuce()) return "Deuce";

        if (EsVentaja())
        {
            return EsVentajaOJuego();
        }

        return $"{PuntajeJugador1.ConvertirPuntoToMarcador()}-{PuntajeJugador2.ConvertirPuntoToMarcador()}";
    }

    private bool EsLove() => PuntajeJugador1 == 0 && PuntajeJugador2 == 0;
    private bool EsDeuce() => PuntajeJugador1 >= 3 && PuntajeJugador2 >= 3 && PuntajeJugador1 == PuntajeJugador2;
    private bool EsVentaja() => (PuntajeJugador1 >= 4 || PuntajeJugador2 >= 4);


    private string EsVentajaOJuego()
    {
        string resultado = "";
        int diferenciaPuntaje = Math.Abs(PuntajeJugador1 - PuntajeJugador2);

        if (diferenciaPuntaje == 1)
            resultado = PuntajeJugador1 > PuntajeJugador2 ? "Advantage-Jugador1" : "Advantage-Jugador2";

        if (diferenciaPuntaje >= 2)
            resultado = PuntajeJugador1 > PuntajeJugador2 ? "Game-Jugador1" : "Game-Jugador2";


        return resultado;
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
