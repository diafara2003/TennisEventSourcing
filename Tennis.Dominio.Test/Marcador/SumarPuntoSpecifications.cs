
using FluentAssertions;
using static Tennis.Dominio.Marcador.Eventos;

namespace Tennis.Dominio.Test.Marcador;

public class SumarPuntoSpecifications : CommandHandlerTest<SumarPunto>
{
    protected override ICommandHandler<SumarPunto> Handler => new AnotarPuntoHandler(eventStore);

    [Fact]
    public void Cuando_Un_jugador_anota_un_punto_y_no_se_ha_creado_el_marcador_debo_generar_una_excepcion_InvalidOperationException()
    {
        var caller = () => When(new SumarPunto(base._aggregateId, base.jugador1));

        caller.Should().ThrowExactly<InvalidOperationException>().WithMessage("Marcador no existente");
    }

    [Fact]
    public void Cuando_un_jugador_anota_un_punto_debo_emitir_un_evento_PuntoAnotado()
    {
        Given(new MarcadorCreado(base._aggregateId,"Love"));

        When(new SumarPunto(_aggregateId, base.jugador1));

        Then(new PuntoSumado(_aggregateId, base.jugador1,"15-love"));
    }

    [Fact]
    public void Cuando_el_jugador1_uno_tiene_15_punto_y_el_jugador2_no_tiene_punto_debo_emitir_un_evento_PuntoAnotado_y_el_marcador_debe_ser_15_love()
    {
        Given(new MarcadorCreado(_aggregateId, "Love"));
      
        When(new SumarPunto(_aggregateId, base.jugador1));
        
        Then(new PuntoSumado(_aggregateId, base.jugador1,"15-love"));
    }


    [Fact]
    public void Cuando_el_jugador1_uno_tiene_30_punto_y_el_jugador2_tiene_punto_15_debo_emitir_un_evento_PuntoAnotado_y_el_marcador_debe_ser_30_15()
    {
        Given(new MarcadorCreado(_aggregateId, "Love"),           
            new PuntoSumado(_aggregateId, base.jugador1, "15-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "30-love")
            );

        When(new SumarPunto(_aggregateId, base.jugador2));

        Then(new PuntoSumado(_aggregateId, base.jugador2, "30-15"));
    }

    [Fact]
    public void Cuando_el_jugador1_tiene_ventaja_debo_emitir_un_evento_PuntoAnotado_y_el_marcador_debe_ser_Advantage_Jugador1()
    {
        Given(new MarcadorCreado(_aggregateId, "Love"),
            new PuntoSumado(_aggregateId, base.jugador1, "15-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "30-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "40-love"),
            new PuntoSumado(_aggregateId, base.jugador2, "40-15"),
            new PuntoSumado(_aggregateId, base.jugador2, "40-30"),
            new PuntoSumado(_aggregateId, base.jugador2, "40-40")
            );
        When(new SumarPunto(_aggregateId, base.jugador1));

        Then(new PuntoSumado(_aggregateId, base.jugador1, "Advantage-Jugador1"));
    }


    [Fact]
    public void Cuando_el_jugador1_gana_debo_emitir_evento_MarcadorFinalizado()
    {
        Given(new MarcadorCreado(_aggregateId, "Love"),
            new PuntoSumado(_aggregateId, base.jugador1, "15-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "30-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "40-love"),
            new PuntoSumado(_aggregateId, base.jugador2, "40-15"),
            new PuntoSumado(_aggregateId, base.jugador2, "40-30")           
            );

        When(new SumarPunto(_aggregateId, base.jugador1));

        Then(new MarcadorFinalizado(_aggregateId, "Game-Jugador1"));
    }

    [Fact]
    public void Debo_generar_Excepcion_InvalidOperationException_cuando_el_marcador_ya_esta_finalizado()
    {
        Given(new MarcadorCreado(_aggregateId, "Love"),
            new PuntoSumado(_aggregateId, base.jugador1, "15-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "30-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "40-love"),
            new PuntoSumado(_aggregateId, base.jugador1, "60-love"),
            new MarcadorFinalizado(_aggregateId, "Game-Jugador1")
            );
        var caller = () => When(new SumarPunto(_aggregateId, base.jugador1));
        caller.Should().ThrowExactly<InvalidOperationException>().WithMessage("La partida ha terminado");
    }

}
