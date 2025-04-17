

namespace Tennis.Dominio.Marcador;

public class Eventos
{
    public record MarcadorCreado(Guid IdMarcador, string puntaje);
    public record PuntoSumado(Guid IdMarcador, string jugador, string puntaje);

    public record MarcadorFinalizado(Guid IdMarcador,string puntaje);

}
