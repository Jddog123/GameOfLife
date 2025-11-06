using FluentAssertions;

namespace GameOfLife;

public class JuegoDeLaVidaTest
{
    private readonly JuegoDeLaVida _juego;

    public JuegoDeLaVidaTest()
    {
        _juego = new JuegoDeLaVida(6);
    }

    [Fact]
    public void DadaUnaCelulaSiNoTieneVecinos_Debe_RetornarCero()
    {
        _juego.AsignarCelula(2, 2);

        _juego.ContarVecinosVivos(2, 2).Should().Be(0);
    }


    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(3, 2)]
    [InlineData(3, 3)]
    [InlineData(2, 3)]
    [InlineData(1, 3)]
    [InlineData(1, 2)]
    public void DadaUnaCelulaConUnVecino_DebeRetornarUno(int filaVecino, int columnaVecino)
    {
        _juego.AsignarCelula(2, 2);
        _juego.AsignarCelula(filaVecino, columnaVecino);

        _juego.ContarVecinosVivos(2, 2).Should().Be(1);
    }

    [Fact]
    public void DadaUnaCeLulaConUnVecinoAlAIzquierdaYOtroAlaDerecha_Debe_RetornarDos()
    {
        _juego.AsignarCelula(2, 1);
        _juego.AsignarCelula(2, 2);
        _juego.AsignarCelula(2, 3);

        _juego.ContarVecinosVivos(2, 2).Should().Be(2);
    }

    [Fact]
    public void DadaUnaCeLulaConSieteVecinos_Debe_RetornarSiete()
    {
        _juego.AsignarCelula(2, 1);
        _juego.AsignarCelula(3, 1);
        _juego.AsignarCelula(3, 2);
        _juego.AsignarCelula(3, 3);
        _juego.AsignarCelula(2, 3);
        _juego.AsignarCelula(1, 3);
        _juego.AsignarCelula(1, 2);

        _juego.AsignarCelula(2, 2);

        _juego.ContarVecinosVivos(2, 2).Should().Be(7);
    }

    [Fact]
    public void DadaUnaCeLulaConOchoVecinos_Debe_RetornarOcho()
    {
        _juego.AsignarCelula(1, 1);
        _juego.AsignarCelula(2, 1);
        _juego.AsignarCelula(3, 1);
        _juego.AsignarCelula(3, 2);
        _juego.AsignarCelula(3, 3);
        _juego.AsignarCelula(2, 3);
        _juego.AsignarCelula(1, 3);
        _juego.AsignarCelula(1, 2);

        _juego.AsignarCelula(2, 2);

        _juego.ContarVecinosVivos(2, 2).Should().Be(8);
    }

    [Fact]
    public void DadaUnaCelulaEnLaEsquinaSuperiorIzquierdaDelTablero_Debe_EstarMuertoLosVecinosFueraDelBorde()
    {
        _juego.AsignarCelula(0, 0);

        _juego.ContarVecinosVivos(0, 0).Should().Be(0);
    }

    [Fact]
    public void DadaUnaCelularEnLaEsquinaInferiorDerechaDelTablero_Debe_EstarMuertoLosVecinosFueraDelBorde()
    {
        _juego.AsignarCelula(5, 5);

        _juego.ContarVecinosVivos(5, 5).Should().Be(0);
    }


    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_SinVecinos_Debe_MorirPorInfrapoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1, 1);
        juego.SiguienteGeneracion();
        
        juego.CelulaEstaViva(1,1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con1Vecino_Debe_MorirPorInfrapoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con2Vecinos_Debe_PasarALaSiguienteGeneracion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeTrue();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con3Vecinos_Debe_PasarALaSiguienteGeneracion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1,1).Should().BeTrue();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con4Vecinos_Debe_MorirPorSobrepoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con5Vecinos_Debe_MorirPorSobrepoblacion()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con6Vecinos_Debe_MorirPorSobrepoblacion()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2, 1);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con7Vecinos_Debe_MorirPorSobrepoblacion()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2, 1);
        juego.AsignarCelula(2, 0);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1_Con8Vecinos_Debe_MorirPorSobrepoblacion()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2, 1);
        juego.AsignarCelula(2, 0);
        juego.AsignarCelula(1, 0);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_SinVecinos_Debe_MantenerseMuerta()
    {
        var juego = new JuegoDeLaVida(3);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con1Vecino_Debe_MantenerseMuerta()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con2Vecinos_Debe_MantenerseMuerta()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        
        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con3Vecinos_Debe_NacerPorReproduccion()
    {
        var  juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeTrue();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con4Vecinos_Debe_MantenerseMuerta()
    {
        var   juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        
        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con5Vecinos_Debe_MantenerseMuerta()
    {
        var   juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2,1);
        
        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con6Vecinos_Debe_MantenerseMuerta()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(2,0);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con7Vecinos_Debe_MantenerseMuerta()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(2,0);
        juego.AsignarCelula(1,0);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaEnLaPosicion1_1Muerta_Con8Vecinos_Debe_MantenerseMuerta()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(2,0);
        juego.AsignarCelula(1,0);
        juego.AsignarCelula(0,0);

        juego.SiguienteGeneracion();

        juego.CelulaEstaViva(1, 1).Should().BeFalse();
    }

    [Fact]
    public void DadaDosCelulasVecinas_Debe_MorirAmbasPorInfrapoblacion()
    {
        var  juego = new JuegoDeLaVida(2);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(1,1);
        bool[,] generacionEsperada = new bool[2, 2];
    
        bool[,] nuevaGeneracion = juego.SiguienteGeneracion();
        
        nuevaGeneracion.Should().BeEquivalentTo(generacionEsperada);
    }

    [Fact]
    public void DadaLaComposicionRPentominoYEnLaQuintaGeneracion_Debe_TenerLaComposicionEsperada()
    {
        var juego = new JuegoDeLaVida(6);
        juego.AsignarCelula(1, 1);
        juego.AsignarCelula(1, 2);
        juego.AsignarCelula(2, 2);
        juego.AsignarCelula(2, 3);
        juego.AsignarCelula(3, 2);

        bool[,] generacionEsperada = new  bool[6, 6];
                
        generacionEsperada[0,2] = true;
        generacionEsperada[1,0] = true;
        generacionEsperada[1,1] = true;
        generacionEsperada[1,3] = true;
        generacionEsperada[1,4] = true;
        generacionEsperada[2,1] = true;
        generacionEsperada[2,4] = true;
        generacionEsperada[3,2] = true;
        generacionEsperada[3,3] = true;
            
        juego.SiguienteGeneracion();
        juego.SiguienteGeneracion();
        juego.SiguienteGeneracion();
        juego.SiguienteGeneracion();
        var quintaGeneracion = juego.SiguienteGeneracion();

        quintaGeneracion.Should().BeEquivalentTo(generacionEsperada);
    }
}