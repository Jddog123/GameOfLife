namespace GameOfLife;

public class JuegoDeLaVida
{
    private bool[,] _tablero;
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

        if (CelulaEstaViva(vecinoSuperior, columna))
            contador++;

        if (CelulaEstaViva(vecinoSuperior, vecinoDerecho))
            contador++;

        if (CelulaEstaViva(vecinoSuperior, vecinoIzquierdo))
            contador++;

        if (CelulaEstaViva(fila, vecinoIzquierdo))
            contador++;

        if (CelulaEstaViva(vecinoInferior, vecinoIzquierdo))
            contador++;

        if (CelulaEstaViva(vecinoInferior, columna))
            contador++;

        if (CelulaEstaViva(vecinoInferior, vecinoDerecho))
            contador++;

        if (CelulaEstaViva(fila, vecinoDerecho))
            contador++;

        return contador;
    }

    public bool CelulaEstaViva(int fila, int columna)
    {
        if (VecinoEstaPorFuera(fila, columna))
            return false;

        return _tablero[fila, columna];
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
        bool[,] nuevaGeneracion = (bool[,] )_tablero.Clone();
        int fila = 1;
        int columna = 1;

        nuevaGeneracion[fila,columna] = CalcularNuevaGeneracionCelula(fila, columna);
        _tablero = (bool[,] )nuevaGeneracion.Clone();
        
        return nuevaGeneracion;
    }

    private bool CalcularNuevaGeneracionCelula(int fila, int columna)
    {
        int cantidadVecinosVivos = ContarVecinosVivos(fila,columna);

        if (_tablero[fila, columna])
        {
            if(cantidadVecinosVivos is 0 or 1 or >= 4)
                return false;
        }
        else
        {
            if(cantidadVecinosVivos == 3)
                return true;
        }

        return _tablero[fila, columna];
    }
}