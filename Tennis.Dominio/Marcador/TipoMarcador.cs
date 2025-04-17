

namespace Tennis.Dominio.Marcador;

public static class TipoMarcador
{
    public static  string ConvertirPuntoToMarcador(this int puntaje)
    {
        return puntaje switch
        {
            1 => "15",
            2 => "30",
            3 => "40",
            _ => "love"
        };
    }
}
