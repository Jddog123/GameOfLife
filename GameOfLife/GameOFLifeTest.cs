using FluentAssertions;

namespace GameOfLife;

public class JuegoDeLaVidaTest
{
    [Fact]
    public void Dada_UnaCelulaVivaLaSiguienteGeneracion_Debe_Morir()
    {
        //Arrange
        var juego = new JuegoDeLaVida(dimension: 6);
        juego.AsignarCelula(2, 2);
        //Act
        juego.SiguienteGeneracion();
        //Assert
        juego.EstaViva(2, 2).Should().BeFalse();
    }
    
    
}

public class JuegoDeLaVida
{
    private int[,] _tablero;
    public JuegoDeLaVida(int dimension)
    {
        _tablero =  new int[dimension, dimension];
    }
    public void AsignarCelula(int fila, int columna)
    {
        _tablero[fila, columna] = 1;
    }

    public void SiguienteGeneracion()
    {
        _tablero[2, 2] = 0;
    }

    public bool  EstaViva(int fila, int columna)
    {
        return _tablero[fila, columna] == 1;
    }
}