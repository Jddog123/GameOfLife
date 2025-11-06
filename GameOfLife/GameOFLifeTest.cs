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


    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(10)]
    public void DadoLaDimensionDeUnTableroConUnaCelulaVivaEnLaSiguienteGeneracion_Debe_EstarMuerta(int dimension)
    {
        var juego = new JuegoDeLaVida(dimension);
        juego.AsignarCelula(0, 0);
        bool[,] tableroEsperado = new bool[dimension, dimension];

        juego.SiguienteGeneracion().Should().BeEquivalentTo(tableroEsperado);
    }

    [Fact]
    public void DadaDosCelulasVivasEnElTableroLaSiguienteGeneracion_Debe_EstarMuerta()
    {
        var juego = new JuegoDeLaVida(2);
        juego.AsignarCelula(0, 0);
        juego.AsignarCelula(0, 1);
        var tableroEsperado = new bool[2, 2];

        juego.SiguienteGeneracion().Should().BeEquivalentTo(tableroEsperado);
    }

    [Fact]
    public void DadaUnaCelulaConDosVecinosLaSiguienteGeneracion_Debe_Sobrevivir()
    {
        var juego = new JuegoDeLaVida(2);
        juego.AsignarCelula(0, 0);
        juego.AsignarCelula(0, 1);
        juego.AsignarCelula(1, 0);

        var nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[0, 0].Should().BeTrue();
    }

    [Fact]
    public void DadaUnaCelulaConTresVecinosLaSiguienteGeneracion_Debe_Sobrevivir()
    {
        var juego = new JuegoDeLaVida(2);

        juego.AsignarCelula(0, 0);
        juego.AsignarCelula(0, 1);
        juego.AsignarCelula(1, 0);
        juego.AsignarCelula(1, 1);

        var nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[0, 0].Should().BeTrue();
    }

    [Fact]
    public void DadaUnaCelulaConCuatroVecinosLaSiguienteGeneracion_Debe_MorirPorSobrePoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(1,0);
        
        var  nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[1, 1].Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaConCincoVecinosLaSiguienteGeneracion_Debe_MorirPorSobrePoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(1,0);
        
        var  nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[1, 1].Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaConSeisVecinosLaSiguienteGeneracion_Debe_MorirPorSobrePoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(1,0);
        juego.AsignarCelula(0,2);
        
        var  nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[1, 1].Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaConSieteVecinosLaSiguienteGeneracion_Debe_MorirPorSobrePoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(1,0);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(2,0);
        
        var  nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[1, 1].Should().BeFalse();
    }
    
    [Fact]
    public void DadaUnaCelulaConOchoVecinosLaSiguienteGeneracion_Debe_MorirPorSobrePoblacion()
    {
        var juego = new JuegoDeLaVida(3);
        
        juego.AsignarCelula(1,1);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(0,1);
        juego.AsignarCelula(1,2);
        juego.AsignarCelula(2,1);
        juego.AsignarCelula(1,0);
        juego.AsignarCelula(0,2);
        juego.AsignarCelula(2,0);
        juego.AsignarCelula(2,2);
        
        var  nuevaGeneracion = juego.SiguienteGeneracion();
        nuevaGeneracion[1, 1].Should().BeFalse();
    }

    [Fact]
    public void DadaUnaCelulaMuertaYTieneTresVecinasVivas_Debe_NacerPorReproduccion()
    {
        var juego = new JuegoDeLaVida(2);
        
        juego.AsignarCelula(0,0);
        juego.AsignarCelula(1,0);
        juego.AsignarCelula(0,1);

        var nuevaGeneracion =  juego.SiguienteGeneracion();

        nuevaGeneracion[1, 1].Should().BeTrue();
    }
}