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

    public void AsignarCelula(int fila, int columna)
    {
        _tablero[fila, columna] = 1;
    }

    public int ContarVecinosVivos(int fila, int columna)
    {
        int contador = 0;

        if (TieneVecinoSuperiorVivo(fila, columna))
            contador++;

        if (TieneVecinoSuperiorDerechoVivo(fila, columna))
            contador++;

        if (TieneVecinoSuperiorIzquierdoVivo(fila, columna))
            contador++;

        if (TieneVecinoIzquierdoVivo(fila, columna))
            contador++;

        if (TieneVecinoInferiorIzquierdoVivo(fila, columna))
            contador++;

        if (TieneVecinoInferiorVivo(fila, columna))
            contador++;

        if (TieneVecinoInferiorDerechoVivo(fila, columna))
            contador++;

        if (TieneVecinoDerechoVivo(fila, columna))
            contador++;

        return contador;
    }

    private bool TieneVecinoSuperiorIzquierdoVivo(int fila, int columna)
    {
        if (fila - 1 < 0 || columna - 1 < 0)
            return false;
        return _tablero[fila - 1, columna - 1] == 1;
    }

    private bool TieneVecinoIzquierdoVivo(int fila, int columna)
    {
        if (columna - 1 < 0)
            return false;
        return _tablero[fila, columna - 1] == 1;
    }

    private bool TieneVecinoInferiorIzquierdoVivo(int fila, int columna)
    {
        if (fila + 1 > _ultimaFila || columna - 1 < 0)
            return false;
        return _tablero[fila + 1, columna - 1] == 1;
    }

    private bool TieneVecinoInferiorVivo(int fila, int columna)
    {
        if (fila + 1 > _ultimaFila)
            return false;

        return _tablero[fila + 1, columna] == 1;
    }

    private bool TieneVecinoInferiorDerechoVivo(int fila, int columna)
    {
        if (fila + 1 > _ultimaFila || columna + 1 > _ultimaColumna)
            return false;

        return _tablero[fila + 1, columna + 1] == 1;
    }

    private bool TieneVecinoDerechoVivo(int fila, int columna)
    {
        if (columna + 1 > _ultimaColumna)
            return false;

        return _tablero[fila, columna + 1] == 1;
    }

    private bool TieneVecinoSuperiorDerechoVivo(int fila, int columna)
    {
        if (fila - 1 < 0 || columna + 1 > _ultimaColumna)
            return false;

        return _tablero[fila - 1, columna + 1] == 1;
    }

    private bool TieneVecinoSuperiorVivo(int fila, int columna)
    {
        if (fila - 1 < 0)
            return false;
        return _tablero[fila - 1, columna] == 1;
    }
}