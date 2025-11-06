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

        int vecinoSuperior = fila - 1;
        int vecinoInferior = fila + 1;
        int vecinoDerecho = columna + 1;
        int vecinoIzquierdo = columna - 1;
        
        if (TieneVecinoVivo(vecinoSuperior, columna))
            contador++;
        
        if (TieneVecinoVivo(vecinoSuperior, vecinoDerecho))
            contador++;
        
        if (TieneVecinoVivo(vecinoSuperior, vecinoIzquierdo))
            contador++;

        if (TieneVecinoVivo(fila, vecinoIzquierdo))
            contador++;
        
        if (TieneVecinoVivo(vecinoInferior, vecinoIzquierdo))
            contador++;

        if (TieneVecinoVivo(vecinoInferior, columna))
            contador++;

        if (TieneVecinoVivo(vecinoInferior, vecinoDerecho))
            contador++;

        if (TieneVecinoVivo(fila, vecinoDerecho))
            contador++;

        return contador;
    }

    private bool TieneVecinoVivo(int filaVecino, int columnaVecino)
    {
        if (VecinoEstaPorFuera(filaVecino, columnaVecino))
            return false;
        
        return _tablero[filaVecino, columnaVecino] == 1;
    }

    private bool VecinoEstaPorFuera(int fila, int columna)
    {
        return columna > _ultimaColumna ||
               fila > _ultimaFila || 
               columna < 0 || 
               fila < 0;
    }
}