namespace Tennis.Dominio.Test.Marcador;

public class CrearMarcadorSpecifications : CommandHandlerTest<CrearMarcador>
{
   
    protected override ICommandHandler<CrearMarcador> Handler => new CrearMarcadorHandler(eventStore);

    [Fact]
    public void Cuando_crea_un_marcador_debe_emitir_un_evento_MarcadorCreado()
    {
       
        When(
            new CrearMarcador(base._aggregateId, "Love")
            );

        Then(
            new Eventos.MarcadorCreado(base._aggregateId, "Love")
            );

    }

  
}
