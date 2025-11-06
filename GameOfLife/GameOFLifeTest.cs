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
    public void DadaUnaCeLulaConUnVecinoAlAIzquierdaYotroAlaDerecha_Debe_RetornarDos()
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
}

public class JuegoDeLaVida
{
    private readonly int[,] _tablero;
    private readonly int _ultimaFila;
    private readonly int _ultimaColumna;

    public JuegoDeLaVida(int dimension)
    {
        _tablero = new int[dimension, dimension];
        _ultimaFila = _tablero.GetLength(0) - 1;
        _ultimaColumna = _tablero.GetLength(1) - 1;
    }

    public void AsignarCelula(int fila, int columna) => _tablero[fila, columna] = 1;

    public int ContarVecinosVivos(int fila, int columna)
    {
        int contador = 0;

        int derecha = columna + 1;
        int inferior = fila + 1;
        int izquierda = columna - 1;
        int superior = fila - 1;

        if (TieneVecinoVivo(superior, columna)) contador++;
        if (TieneVecinoVivo(superior,  derecha)) contador++;
        if (TieneVecinoVivo(superior, izquierda)) contador++;
        if (TieneVecinoVivo(fila, izquierda)) contador++;
        if (TieneVecinoVivo(inferior, izquierda)) contador++;
        if (TieneVecinoVivo(inferior, columna)) contador++;
        if (TieneVecinoVivo(inferior, derecha)) contador++;
        if (TieneVecinoVivo(fila, derecha)) contador++;

        return contador;
    }

    private bool TieneVecinoVivo(int fila, int columna)
    {
        bool tieneVecinoSuperior;
        
        if (EstoyPorFuera(fila, columna))
            tieneVecinoSuperior = false;
        else
            tieneVecinoSuperior = _tablero[fila, columna] == 1;
        return tieneVecinoSuperior;
    }

    public bool EstoyPorFuera(int fila, int columna)
    {
        int casillaDerecha = columna + 1;
        int casillaInferior = fila + 1;
        int casillaIzquierda = columna - 1;
        int casillaSuperior = fila - 1;

        return casillaDerecha > _ultimaColumna ||
               casillaInferior > _ultimaFila || 
               casillaIzquierda < 0 || 
               casillaSuperior < 0;
    }
}