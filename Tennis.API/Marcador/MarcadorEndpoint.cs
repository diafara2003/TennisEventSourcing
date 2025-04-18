using Tennis.Dominio;
using Tennis.Dominio.Marcador.Comandos;

namespace Tennis.API.Marcador;

public static class MarcadorEndpoint
{
    public static void MarcadorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/marcador", async (ICommandRouter router) =>
        {
            var comando = new CrearMarcador(Guid.NewGuid(), "Love");

            await router.InvokeAsync(comando);

            return Results.Ok();

        });

        app.MapPost("/marcador/{IdMarcador}/jugador{jugador}", async (Guid IdMarcador, string jugador,
            ICommandRouter router) =>
        {
            var comand = new SumarPunto(IdMarcador, jugador);

            await router.InvokeAsync(comand);

            return Results.Ok();

        });

    }
}
