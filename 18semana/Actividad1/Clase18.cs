
public class EstudianteCurso
{
    string _nombreEstudiante;
    double _promedioEstudiante;
    double[] _notasRegistradasEstudiante = new double[10];
    double[]? _notasAprobadas;
    double[]? _notasNoAprobadas;
    double _notaAcumulada = 0;

    public EstudianteCurso(string nombreEstudiante)
    {
        this._nombreEstudiante = nombreEstudiante;
    }

    public static double ValidarNota()
    {
        double numeroDelUsuario = 0;
        bool validezNumero;

        do
        {
            Console.Write("Ingrese un número decimal entre 0 y 100: ");
            string entradaDeUsuario = Console.ReadLine()!;

            if (double.TryParse(entradaDeUsuario, out numeroDelUsuario))
            {
                if (numeroDelUsuario >= 0 && numeroDelUsuario <= 100)
                {
                    validezNumero = true;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. El número debe estar entre 0 y 100. Inténtalo de nuevo.");
                    validezNumero = false;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Asegúrate de ingresar un número decimal válido. Inténtalo de nuevo.");
                validezNumero = false;
            }

        } while (!validezNumero);

        return numeroDelUsuario;
    }

    public double[] ObtenerNotas()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Ingrese la nota #{i + 1} del estudiante {this._nombreEstudiante}");
            double numeroIngresado = ValidarNota();
            this._notasRegistradasEstudiante[i] = numeroIngresado;
        }
        Console.Clear();
        return this._notasRegistradasEstudiante!;
    }

    public double ObtenerPromedio()
    {
        ObtenerNotas();
        foreach (double nota in this._notasRegistradasEstudiante)
        {
            _notaAcumulada += nota;
        }

        this._promedioEstudiante = _notaAcumulada / 10;
        return Math.Round(this._promedioEstudiante, 2);
    }

    public void VerificarNotas()
    {
        int conteoNotasAprobadas = 0;
        int conteoNotasNoAprobadas = 0;
        foreach (double nota in this._notasRegistradasEstudiante)
        {
            if (nota >= 65)
                conteoNotasAprobadas++;
            else
                conteoNotasNoAprobadas++;
        }

        _notasAprobadas = new double[conteoNotasAprobadas];
        _notasNoAprobadas = new double[conteoNotasNoAprobadas];

        int iAprobadas = 0;
        int iNoAprobadas = 0;

        foreach (double nota in this._notasRegistradasEstudiante)
        {
            if (nota >= 65)
                _notasAprobadas[iAprobadas++] = nota;
            else
                _notasNoAprobadas[iNoAprobadas++] = nota;
        }
    }

    public void MostrarNotasAprobadas()
    {
        Console.WriteLine($"Notas aprobadas por {_nombreEstudiante}:");
        foreach (double nota in _notasAprobadas!)
        {
            Console.Write(nota + " ");
        }
        Console.WriteLine();
    }

    public void MostrarNotasNoAprobadas()
    {
        Console.WriteLine($"Notas NO aprobadas por {_nombreEstudiante}:");
        foreach (double nota in _notasNoAprobadas!)
        {
            Console.Write(nota + " ");
        }
        Console.WriteLine();
    }

}

public class Clase18
{
    static int ValidarNumeroUsuario(int numeroLimite)
    {
        bool validezNumero;
        int numeroUsuario;
        do
        {
            Console.Write("Ingrese un número positivo entero entre las opciones dadas: ");
            string entradaDeUsuario = Console.ReadLine()!;

            if (int.TryParse(entradaDeUsuario, out numeroUsuario))
            {
                if ((numeroUsuario > 0) && (numeroUsuario <= numeroLimite))
                {
                    validezNumero = true;
                }
                else
                {
                    Console.WriteLine("\nEntrada inválida, escoge un número dentro de las opciones dadas. Inténtalo de nuevo.");
                    validezNumero = false;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida, no es un número o es un número con decimales. Inténtalo de nuevo.");
                validezNumero = false;
            }
        }
        while (!validezNumero);

        return numeroUsuario; 
    }

    public static void Main()
    {
        EstudianteCurso[] grupo = new EstudianteCurso[10];
        double sumaPromedios = 0;
        for (int i = 0; i < grupo.Length; i++)
        {
            Console.WriteLine($"Ingresa el nombre del estudiante #{i + 1}:");
            string nombreIngresado = Console.ReadLine()!;

            grupo[i] = new EstudianteCurso(nombreIngresado);
            double promedio = grupo[i].ObtenerPromedio();
            sumaPromedios += promedio;
        }

        int opcionUsuario = 0;
        bool statusPrograma = true;

        do
        {
            Console.WriteLine("1. Mostrar nombre y notas aprobadas estudiantes");
            Console.WriteLine("2. Mostrar nombre y notas no aprobadas estudiantes");
            Console.WriteLine("3. Mostrar promedio de notas de grupo");
            Console.WriteLine("4. Salir del programa");
            opcionUsuario = ValidarNumeroUsuario(4);

            switch (opcionUsuario)
            {
                case 1:
                    for (int i = 0; i < grupo.Length; i++)
                    {
                        grupo[i].VerificarNotas();
                        grupo[i].MostrarNotasAprobadas();
                        Console.WriteLine("Presiona ENTER para continuar");
                        Console.ReadKey();

                    }
                    statusPrograma = true;
                    break;

                case 2:
                    for (int i = 0; i < grupo.Length; i++)
                    {
                        grupo[i].VerificarNotas();
                        grupo[i].MostrarNotasNoAprobadas();
                        Console.WriteLine("Presiona ENTER para continuar");
                        Console.ReadKey();

                    }
                    statusPrograma = true;
                    break;

                case 3:
                    double promedioGrupo = sumaPromedios / grupo.Length;
                    Console.WriteLine($"Promedio general del grupo: {Math.Round(promedioGrupo, 2)}");
                    statusPrograma = true;
                    Console.WriteLine("Presiona ENTER para continuar");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Gracias por utilizar este programa, presiona ENTER para continuar");
                    Console.ReadKey();
                    statusPrograma = false;
                    break;

            }

        }

        while (statusPrograma);



    }
}

