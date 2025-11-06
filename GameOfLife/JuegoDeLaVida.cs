namespace GameOfLife;

public class JuegoDeLaVida
{
    private readonly bool[,] _tablero;
    private readonly int _ultimaFila;
    private readonly int _ultimaColumna;
    private int _dimension;

    public JuegoDeLaVida(int dimension)
    {
        _dimension = dimension;
        _tablero = new bool[dimension, dimension];
        _ultimaFila = _tablero.GetLength(0) - 1;
        _ultimaColumna = _tablero.GetLength(1) - 1;
    }

    public void AsignarCelula(int fila, int columna)
    {
        _tablero[fila, columna] = true;
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

    public bool TieneVecinoVivo(int filaVecino, int columnaVecino)
    {
        if (VecinoEstaPorFuera(filaVecino, columnaVecino))
            return false;
        
        return _tablero[filaVecino, columnaVecino];
    }

    private bool VecinoEstaPorFuera(int fila, int columna)
    {
        return columna > _ultimaColumna ||
               fila > _ultimaFila || 
               columna < 0 || 
               fila < 0;
    }

    public bool[,] SiguienteGeneracion()
    {
        bool[,] nuevaGeneracion = new bool[_dimension,_dimension];
        int cantidadVecinosVivos = ContarVecinosVivos(0,0);
        
        if (cantidadVecinosVivos is 3 or 2) 
            nuevaGeneracion[0, 0] = true;
            
        return nuevaGeneracion;
        
    }
}