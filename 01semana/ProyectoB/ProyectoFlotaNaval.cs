class Program
{
    // Clase que representa a un jugador en el juego de Batalla Naval
    class Jugador
    {
        public int NumJugador { get; private set; }     // Número identificador del jugador (1 o 2)
        public int PuntosJugador { get; set; }          // Puntos acumulados durante la partida
        public char[,] TableroFlotaNaval { get; private set; }  // Tablero donde se ubican los barcos del jugador
        public char[,] TableroAtaque { get; private set; }      // Tablero para registrar ataques realizados al oponente
        public int CantidadMisiles { get; set; }        // Cantidad de misiles disponibles para atacar
        public string[] CoordenadasAtacadas { get; private set; }  // Registro de coordenadas ya utilizadas
        public int ContadorCoordenadasAtacadas { get; private set; }  // Contador de ataques realizados
        public bool SeRindio { get; set; }              // Indica si el jugador se rindió durante la partida
        
        // Constructor de la clase Jugador que inicializa sus propiedades
        public Jugador(int numJugador)
        {
            NumJugador = numJugador;                    // Asigna el número del jugador
            PuntosJugador = 0;                          // Inicializa puntos en cero
            TableroFlotaNaval = new char[7, 7];         // Crea matriz para ubicar sus barcos
            TableroAtaque = new char[7, 7];             // Crea matriz para realizar ataques
            CantidadMisiles = 15;                       // Establece la cantidad inicial de misiles
            CoordenadasAtacadas = new string[15];       // Arreglo para guardar coordenadas ya usadas
            ContadorCoordenadasAtacadas = 0;            // Inicializa contador de ataques en cero
            SeRindio = false;                           // Inicialmente el jugador no se ha rendido
            
            // Prepara los tableros con las coordenadas e información inicial
            InicializarTableros();
        }
        
        // Método auxiliar que inicializa ambos tableros del jugador
        void InicializarTableros()
        {
            // Llama al método de inicialización para cada tablero
            InicializarTablero(TableroFlotaNaval);
            InicializarTablero(TableroAtaque);
        }
        
        // Configura un tablero con coordenadas y agua en las casillas interiores
        void InicializarTablero(char[,] tablero)
        {
            for (int fila = 0; fila < 7; fila++)
            {
                for (int columna = 0; columna < 7; columna++)
                {
                    if (fila == 0 && columna > 0)
                    {
                        // Establece números de columna (1-6) en la primera fila
                        switch (columna)
                        {
                            case 1:
                                tablero[fila, columna] = '1';
                                break;
                            case 2:
                                tablero[fila, columna] = '2';
                                break;
                            case 3:
                                tablero[fila, columna] = '3';
                                break;
                            case 4:
                                tablero[fila, columna] = '4';
                                break;
                            case 5:
                                tablero[fila, columna] = '5';
                                break;
                            case 6:
                                tablero[fila, columna] = '6';
                                break;
                        }
                    }
                    else if (columna == 0 && fila > 0)
                    {
                        // Establece letras de fila (A-F) en la primera columna
                        switch (fila)
                        {
                            case 1:
                                tablero[fila, columna] = 'A';
                                break;
                            case 2:
                                tablero[fila, columna] = 'B';
                                break;
                            case 3:
                                tablero[fila, columna] = 'C';
                                break;
                            case 4:
                                tablero[fila, columna] = 'D';
                                break;
                            case 5:
                                tablero[fila, columna] = 'E';
                                break;
                            case 6:
                                tablero[fila, columna] = 'F';
                                break;
                        }
                    }
                    else if (fila == 0 && columna == 0)
                    {
                        // Deja vacía la esquina superior izquierda
                        tablero[fila, columna] = ' ';
                    }
                    else
                    {
                        // Rellena el interior del tablero con símbolo de agua
                        tablero[fila, columna] = '~';
                    }
                }
            }
        }
        
        // Permite al jugador elegir la configuración de barcos para su flota
        public void ConfigurarBarcos()
        {
            // Limpia la pantalla y muestra título para la configuración
            Console.Clear();
            Console.WriteLine($"=== CONFIGURACIÓN DE BARCOS - JUGADOR {NumJugador} ===");
            
            Random random = new Random();               // Generador de números aleatorios
            int opcionMapa = 0;                         // Variable para almacenar mapa seleccionado
            bool configuracionAceptada = false;         // Control para bucle de selección
            
            // Bucle que continúa hasta que el jugador acepte una configuración
            while (!configuracionAceptada)
            {
                // Genera un número aleatorio entre 1 y 10 para la configuración
                opcionMapa = random.Next(1, 11);
                
                // Aplica la configuración de barcos al tablero según la opción
                AplicarConfiguracionPredeterminada(opcionMapa);
                
                // Muestra el mapa generado al jugador para su evaluación
                Console.WriteLine("Mapa generado:");
                DibujarTableroFlotaNaval();
                
                // Solicita confirmación al jugador sobre la configuración mostrada
                Console.WriteLine("\nIndique si desea mantener esta configuración:");
                Console.WriteLine("1. Sí, mantener flota");
                Console.WriteLine("2. No, regenerar flota");
                
                // Obtiene y valida la elección del jugador
                int opcion = Program.ValidarEntradaMenu(2);
                configuracionAceptada = opcion == 1;   // Solo acepta si eligió opción 1
            }
            
            // Muestra mensaje de confirmación final
            Console.WriteLine($"¡Configuración de barcos del Jugador {NumJugador} aceptada!");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        // Aplica una configuración predefinida de barcos según el patrón elegido
         void AplicarConfiguracionPredeterminada(int opcion)
        {
            // Limpia el tablero de posibles barcos previos
            for (int fila = 1; fila < 7; fila++)
            {
                for (int columna = 1; columna < 7; columna++)
                {
                    TableroFlotaNaval[fila, columna] = '~';
                }
            }

            // Según la opción, coloca los barcos en posiciones predefinidas
            switch (opcion)
            {
                case 1:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[3, 2] = '✦';
                    TableroFlotaNaval[3, 3] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[1, 1] = '★';
                    TableroFlotaNaval[1, 2] = '★';
                    TableroFlotaNaval[1, 3] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[2, 5] = '✸';
                    TableroFlotaNaval[3, 5] = '✸';
                    TableroFlotaNaval[4, 5] = '✸';
                    TableroFlotaNaval[5, 5] = '✸';
                    break;

                case 2:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[6, 5] = '✦';
                    TableroFlotaNaval[6, 6] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[5, 3] = '★';
                    TableroFlotaNaval[5, 4] = '★';
                    TableroFlotaNaval[5, 5] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[2, 2] = '✸';
                    TableroFlotaNaval[3, 2] = '✸';
                    TableroFlotaNaval[4, 2] = '✸';
                    TableroFlotaNaval[5, 2] = '✸';
                    break;

                case 3:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[5, 1] = '✦';
                    TableroFlotaNaval[5, 2] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[3, 4] = '★';
                    TableroFlotaNaval[3, 5] = '★';
                    TableroFlotaNaval[3, 6] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[2, 3] = '✸';
                    TableroFlotaNaval[3, 3] = '✸';
                    TableroFlotaNaval[4, 3] = '✸';
                    TableroFlotaNaval[5, 3] = '✸';
                    break;

                case 4:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[1, 5] = '✦';
                    TableroFlotaNaval[1, 6] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[6, 4] = '★';
                    TableroFlotaNaval[6, 5] = '★';
                    TableroFlotaNaval[6, 6] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[2, 1] = '✸';
                    TableroFlotaNaval[3, 1] = '✸';
                    TableroFlotaNaval[4, 1] = '✸';
                    TableroFlotaNaval[5, 1] = '✸';
                    break;

                case 5:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[1, 2] = '✦';
                    TableroFlotaNaval[1, 3] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[2, 4] = '★';
                    TableroFlotaNaval[2, 5] = '★';
                    TableroFlotaNaval[2, 6] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[3, 6] = '✸';
                    TableroFlotaNaval[4, 6] = '✸';
                    TableroFlotaNaval[5, 6] = '✸';
                    TableroFlotaNaval[6, 6] = '✸';
                    break;

                case 6:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[5, 2] = '✦';
                    TableroFlotaNaval[5, 3] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[4, 4] = '★';
                    TableroFlotaNaval[4, 5] = '★';
                    TableroFlotaNaval[4, 6] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[1, 2] = '✸';
                    TableroFlotaNaval[2, 2] = '✸';
                    TableroFlotaNaval[3, 2] = '✸';
                    TableroFlotaNaval[4, 2] = '✸';
                    break;

                case 7:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[4, 4] = '✦';
                    TableroFlotaNaval[4, 5] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[1, 1] = '★';
                    TableroFlotaNaval[1, 2] = '★';
                    TableroFlotaNaval[1, 3] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[3, 3] = '✸';
                    TableroFlotaNaval[4, 3] = '✸';
                    TableroFlotaNaval[5, 3] = '✸';
                    TableroFlotaNaval[6, 3] = '✸';
                    break;

                case 8:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[3, 1] = '✦';
                    TableroFlotaNaval[3, 2] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[2, 3] = '★';
                    TableroFlotaNaval[2, 4] = '★';
                    TableroFlotaNaval[2, 5] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[1, 6] = '✸';
                    TableroFlotaNaval[2, 6] = '✸';
                    TableroFlotaNaval[3, 6] = '✸';
                    TableroFlotaNaval[4, 6] = '✸';
                    break;

                case 10:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[1, 2] = '✦';
                    TableroFlotaNaval[1, 3] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[1, 4] = '★';
                    TableroFlotaNaval[1, 5] = '★';
                    TableroFlotaNaval[1, 6] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[2, 4] = '✸';
                    TableroFlotaNaval[3, 4] = '✸';
                    TableroFlotaNaval[4, 4] = '✸';
                    TableroFlotaNaval[5, 4] = '✸';
                    break;

                case 9:
                    //Submarino (2 casillas)
                    TableroFlotaNaval[2, 3] = '✦';
                    TableroFlotaNaval[2, 4] = '✦';
                    //Fragata (3 casillas)
                    TableroFlotaNaval[3, 2] = '★';
                    TableroFlotaNaval[3, 3] = '★';
                    TableroFlotaNaval[3, 4] = '★';
                    //Destructor (4 casillas)
                    TableroFlotaNaval[3, 5] = '✸';
                    TableroFlotaNaval[4, 5] = '✸';
                    TableroFlotaNaval[5, 5] = '✸';
                    TableroFlotaNaval[6, 5] = '✸';
                    break;
            }
        }
        
        // Método que muestra ambos tableros del jugador (flota y ataques)
        public void DibujarTableros()
        {
            // Muestra el tablero donde están ubicados los barcos del jugador
            Console.WriteLine("\nTu Flota Naval:");
            DibujarTableroFlotaNaval();
            
            // Muestra el tablero donde el jugador registra sus ataques al enemigo
            Console.WriteLine("\nTu Tablero de Ataque:");
            DibujarTableroAtaque();
        }
        
        // Muestra en pantalla el tablero de barcos del jugador
        public void DibujarTableroFlotaNaval()
        {
            // Recorre y dibuja cada celda del tablero de flota
            for (int fila = 0; fila < 7; fila++)
            {
                for (int columna = 0; columna < 7; columna++)
                {
                    Console.Write(TableroFlotaNaval[fila, columna] + " ");
                }
                Console.WriteLine();   // Salto de línea al terminar cada fila
            }
        }
        
        // Muestra en pantalla el tablero de ataques realizados por el jugador
        public void DibujarTableroAtaque()
        {
            // Recorre y dibuja cada celda del tablero de ataques
            for (int fila = 0; fila < 7; fila++)
            {
                for (int columna = 0; columna < 7; columna++)
                {
                    Console.Write(TableroAtaque[fila, columna] + " ");
                }
                Console.WriteLine();   // Salto de línea al terminar cada fila
            }
        }
        
        // Permite al jugador seleccionar coordenadas para lanzar un misil
        public string LanzarMisil()
        {
            bool coordenadaValida = false;    // Control para validación de coordenadas
            string coordenada = "";           // Variable para almacenar la coordenada elegida
            
            // Bucle que continúa hasta obtener una coordenada válida
            while (!coordenadaValida)
            {
                // Muestra información sobre misiles restantes
                Console.WriteLine($"\nMisiles restantes: {CantidadMisiles}");
                Console.Write("Ingrese coordenada de ataque (Ejemplo: A-1): ");
                coordenada = Console.ReadLine()?.Trim() ?? "";
                
                // Valida el formato y que no se haya atacado previamente la posición
                if (ValidarFormato(coordenada) && ValidarPosicion(coordenada))
                {
                    // Registra la coordenada como válida y la guarda
                    coordenadaValida = true;
                    CoordenadasAtacadas[ContadorCoordenadasAtacadas] = coordenada;
                    ContadorCoordenadasAtacadas++;
                    CantidadMisiles--;  // Reduce el contador de misiles disponibles
                }
                else if (!ValidarFormato(coordenada))
                {
                    // Notifica error en el formato de la coordenada
                    Console.WriteLine("Formato inválido. Debe ser una letra (A-F) seguida de un guion y un número (1-6). Ejemplo: A-1");
                }
                else
                {
                    // Notifica que la coordenada ya fue utilizada previamente
                    Console.WriteLine("Ya has atacado esta coordenada. Intenta otra.");
                }
            }
            
            return coordenada;   // Devuelve la coordenada válida seleccionada
        }
        
        // Verifica que el formato de la coordenada sea válido
        bool ValidarFormato(string coordenada)
        {
            // Verifica que la longitud sea exactamente 3 caracteres
            if (coordenada.Length != 3)
                return false;
                
            // Extrae cada componente de la coordenada para validación
            char fila = coordenada[0];
            char separador = coordenada[1];
            char columna = coordenada[2];
            
            // Comprueba que la fila sea una letra entre A y F
            bool filaValida = (fila >= 'A' && fila <= 'F');
            // Comprueba que el separador sea un guion
            bool separadorValido = (separador == '-');
            // Comprueba que la columna sea un número entre 1 y 6
            bool columnaValida = (columna >= '1' && columna <= '6');
            
            // La coordenada es válida solo si todos los componentes lo son
            return filaValida && separadorValido && columnaValida;
        }
        
        // Verifica que la coordenada no haya sido utilizada anteriormente
        bool ValidarPosicion(string coordenada)
        {
            // Busca en el registro de coordenadas atacadas previamente
            for (int i = 0; i < ContadorCoordenadasAtacadas; i++)
            {
                // Si encuentra coincidencia, la coordenada ya fue usada
                if (CoordenadasAtacadas[i] == coordenada)
                {
                    return false;
                }
            }
            return true;   // Si no encuentra coincidencia, la coordenada es nueva
        }
        
        // Verifica si todos los barcos del jugador han sido derribados
        public bool TodosLosBarcosDerribados()
        {
            // Inicializa variables de control suponiendo que todos están hundidos
            bool submarinoHundido = true;
            bool fragataHundida = true;
            bool destructorHundido = true;
            
            // Recorre todo el tablero buscando barcos restantes
            for (int fila = 1; fila < 7; fila++)
            {
                for (int columna = 1; columna < 7; columna++)
                {
                    // Si encuentra un símbolo de barco, marca que ese tipo no está hundido
                    if (TableroFlotaNaval[fila, columna] == '✦')
                        submarinoHundido = false;
                    else if (TableroFlotaNaval[fila, columna] == '★')
                        fragataHundida = false;
                    else if (TableroFlotaNaval[fila, columna] == '✸')
                        destructorHundido = false;
                }
            }
            
            // Devuelve true solo si todos los tipos de barcos están hundidos
            return submarinoHundido && fragataHundida && destructorHundido;
        }
    }

    // Variable global para controlar el estado actual del juego
    static bool estadoJuego = true;

    // Método principal que inicia la ejecución del programa
    static void Main()
    {
        // Muestra el menú principal del juego
        Console.WriteLine("=== BATALLA NAVAL ===");
        Console.WriteLine("1. Jugar");
        Console.WriteLine("2. Instrucciones");
        Console.WriteLine("3. Terminar juego");

        // Obtiene y valida la opción seleccionada por el usuario
        int opcion = ValidarEntradaMenu(3);

        // Ejecuta la acción correspondiente a la opción elegida
        switch (opcion)
        {
            case 1:
                IniciarJuego();  // Comienza una nueva partida
                break;
            case 2:
                MostrarInstrucciones();  // Muestra las reglas del juego
                break;
            case 3:
                Console.WriteLine("¡Gracias por jugar! Hasta pronto.");  // Finaliza la aplicación
                break;
        }
    }

    // Configura e inicia una nueva partida del juego
    static void IniciarJuego()
    {
        // Reinicia el estado del juego al iniciar una nueva partida
        estadoJuego = true;
        int contadorTurnos = 0;  // Contador para limitar el número máximo de turnos

        // Crea los dos jugadores que participarán en la partida
        Jugador jugador1 = new Jugador(1);
        Jugador jugador2 = new Jugador(2);

        // Permite a los jugadores configurar la posición de sus barcos
        jugador1.ConfigurarBarcos();
        jugador2.ConfigurarBarcos();

        // Ciclo principal del juego que continúa hasta límite de turnos o finalización
        while (estadoJuego && contadorTurnos < 30)
        {
            contadorTurnos++;  // Incrementa el contador de turnos
            Console.Clear();   // Limpia la pantalla para el nuevo turno

            // Turno del jugador 1
            Console.WriteLine($"=== TURNO {contadorTurnos} - JUGADOR 1 ===");
            ProcesarTurno(jugador1, jugador2);  // Ejecuta acciones del jugador 1

            // Verifica si el juego terminó durante el turno del jugador 1
            if (!estadoJuego)
            {
                FinalizarJuego(jugador1, jugador2, contadorTurnos);
                break;  // Sale del ciclo si el juego ha terminado
            }

            // Pausa para que el jugador pueda ver los resultados de su turno
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            // Turno del jugador 2
            Console.WriteLine($"=== TURNO {contadorTurnos} - JUGADOR 2 ===");
            ProcesarTurno(jugador2, jugador1);  // Ejecuta acciones del jugador 2

            // Verifica si el juego terminó durante el turno del jugador 2
            if (!estadoJuego)
            {
                FinalizarJuego(jugador1, jugador2, contadorTurnos);
                break;  // Sale del ciclo si el juego ha terminado
            }

            // Pausa para que el jugador pueda ver los resultados de su turno
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();

            // Verifica si alguno de los jugadores ha perdido todos sus barcos
            if (jugador1.TodosLosBarcosDerribados() || jugador2.TodosLosBarcosDerribados())
            {
                estadoJuego = false;  // Marca el fin del juego
                FinalizarJuego(jugador1, jugador2, contadorTurnos);
                break;  // Sale del ciclo al finalizar el juego
            }
        }

        // Si se alcanzó el límite máximo de turnos, finaliza el juego
        if (estadoJuego && contadorTurnos >= 30)
        {
            FinalizarJuego(jugador1, jugador2, contadorTurnos);
        }
    }

    // Gestiona las acciones de un jugador durante su turno
    static void ProcesarTurno(Jugador atacante, Jugador defensor)
    {
        // Muestra la información de los tableros del jugador activo
        Console.WriteLine($"Tu flota naval, Jugador {atacante.NumJugador}:");
        atacante.DibujarTableros();

        // Presenta las opciones disponibles para el jugador en su turno
        Console.WriteLine("¿Qué deseas hacer?");
        Console.WriteLine("1. Atacar");
        Console.WriteLine("2. Rendirse");

        // Obtiene y valida la opción elegida por el jugador
        int opcion = ValidarEntradaMenu(2);

        if (opcion == 1)
        {
            // Opción de ataque
            string coordenada = atacante.LanzarMisil();  // Obtiene coordenadas de ataque

            // Convierte la coordenada a índices de la matriz del tablero
            int[] indices = ConvertirCoordenada(coordenada);

            // Verifica si el ataque impactó en algún barco
            if (defensor.TableroFlotaNaval[indices[0], indices[1]] == '✦' ||
                defensor.TableroFlotaNaval[indices[0], indices[1]] == '★' ||
                defensor.TableroFlotaNaval[indices[0], indices[1]] == '✸')
            {
                // Guarda el tipo de barco impactado
                char barcoImpactado = defensor.TableroFlotaNaval[indices[0], indices[1]];
                atacante.TableroAtaque[indices[0], indices[1]] = '●';  // Marca el acierto

                Console.WriteLine("¡ACIERTO! Has impactado un barco enemigo.");

                // Marca el daño en el tablero del defensor
                defensor.TableroFlotaNaval[indices[0], indices[1]] = '●';

                // Verifica si el impacto ha hundido completamente el barco
                bool barcoHundido = false;

                // Según el tipo de barco impactado, verifica si se ha hundido
                switch (barcoImpactado)
                {
                    case '✦': // Submarino (2 casillas)
                        barcoHundido = VerificarBarcoHundido(defensor, '✦');
                        if (barcoHundido)
                        {
                            Console.WriteLine("¡HUNDIDO! Has hundido el submarino enemigo (+2 puntos)");
                            atacante.PuntosJugador += 2;  // Otorga puntos por hundir submarino
                        }
                        break;
                    case '★': // Fragata (3 casillas)
                        barcoHundido = VerificarBarcoHundido(defensor, '★');
                        if (barcoHundido)
                        {
                            Console.WriteLine("¡HUNDIDO! Has hundido la fragata enemiga (+3 puntos)");
                            atacante.PuntosJugador += 3;  // Otorga puntos por hundir fragata
                        }
                        break;
                    case '✸': // Destructor (4 casillas)
                        barcoHundido = VerificarBarcoHundido(defensor, '✸');
                        if (barcoHundido)
                        {
                            Console.WriteLine("¡HUNDIDO! Has hundido el destructor enemigo (+4 puntos)");
                            atacante.PuntosJugador += 4;  // Otorga puntos por hundir destructor
                        }
                        break;
                }

                // Verifica si todos los barcos han sido hundidos para terminar el juego
                if (defensor.TodosLosBarcosDerribados())
                {
                    estadoJuego = false;  // Marca el fin del juego
                }
            }
            else
            {
                // El ataque no impactó ningún barco (agua)
                atacante.TableroAtaque[indices[0], indices[1]] = '✖';  // Marca el fallo
                Console.WriteLine("¡FALLASTE! Has disparado al agua.");
            }

            // Muestra el tablero actualizado después del ataque
            Console.WriteLine("Tablero de ataque actualizado:");
            atacante.DibujarTableroAtaque();
        }
        else
        {
            // Opción de rendirse
            Console.WriteLine($"El Jugador {atacante.NumJugador} se ha rendido.");
            atacante.SeRindio = true;  // Marca al jugador como rendido
            estadoJuego = false;       // Termina el juego
        }
    }

    // Verifica si un tipo específico de barco ha sido completamente hundido
    static bool VerificarBarcoHundido(Jugador jugador, char tipoBarco)
    {
        // Recorre todo el tablero buscando casillas del tipo de barco indicado
        for (int fila = 1; fila < 7; fila++)
        {
            for (int columna = 1; columna < 7; columna++)
            {
                // Si encuentra una casilla intacta del barco, no está hundido
                if (jugador.TableroFlotaNaval[fila, columna] == tipoBarco)
                {
                    return false;  // El barco aún no está completamente hundido
                }
            }
        }
        return true;  // No quedan casillas del barco, está completamente hundido
    }

    // Muestra los resultados finales y termina la partida
    static void FinalizarJuego(Jugador jugador1, Jugador jugador2, int contadorTurnos)
    {
        Console.Clear();  // Limpia la pantalla para mostrar resultados
        Console.WriteLine("=== FIN DEL JUEGO ===");

        // Determina el motivo de finalización y al ganador
        if (jugador1.SeRindio)
        {
            // Jugador 1 se rindió, gana el Jugador 2
            Console.WriteLine("¡El Jugador 1 se ha rendido! Jugador 2 gana.");
            jugador2.PuntosJugador += 5;  // Bonificación por rendición del oponente
        }
        else if (jugador2.SeRindio)
        {
            // Jugador 2 se rindió, gana el Jugador 1
            Console.WriteLine("¡El Jugador 2 se ha rendido! Jugador 1 gana.");
            jugador1.PuntosJugador += 5;  // Bonificación por rendición del oponente
        }
        else if (jugador1.TodosLosBarcosDerribados())
        {
            // Todos los barcos del Jugador 1 fueron hundidos
            Console.WriteLine("¡El Jugador 2 ha ganado! Todos los barcos del Jugador 1 han sido hundidos.");
        }
        else if (jugador2.TodosLosBarcosDerribados())
        {
            // Todos los barcos del Jugador 2 fueron hundidos
            Console.WriteLine("¡Felicidades Jugador 1! Has hundido todos los barcos del Jugador 2.");
        }
        else if (contadorTurnos >= 30)
        {
            // Se alcanzó el límite de turnos, determina ganador por puntos
            if (jugador1.PuntosJugador > jugador2.PuntosJugador)
            {
                Console.WriteLine("¡Felicidades Jugador 1! Has ganado por puntuación.");
            }
            else if (jugador2.PuntosJugador > jugador1.PuntosJugador)
            {
                Console.WriteLine("¡Felicidades Jugador 2! Has ganado por puntuación.");
            }
            else
            {
                Console.WriteLine("¡Empate! Ambos jugadores tienen la misma puntuación.");
            }
        }

// Muestra las estadísticas finales de la partida para ambos jugadores
Console.WriteLine("\n=== ESTADÍSTICAS ===");
Console.WriteLine($"Jugador 1: {jugador1.PuntosJugador} puntos, {15 - jugador1.CantidadMisiles} misiles utilizados");
Console.WriteLine($"Jugador 2: {jugador2.PuntosJugador} puntos, {15 - jugador2.CantidadMisiles} misiles utilizados");

Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");

// Espera a que el usuario presione una tecla cualquiera
Console.ReadKey();
// Limpia la pantalla antes de mostrar el menú principal
Console.Clear();
// Vuelve al menú principal llamando al método Main
Main();
    }

    // Método que muestra las instrucciones del juego al usuario
    static void MostrarInstrucciones()
    {
        // Limpia la pantalla antes de mostrar las instrucciones
        Console.Clear();
        Console.WriteLine("=== INSTRUCCIONES DE BATALLA NAVAL ===");
        Console.WriteLine("1. Cada jugador tiene 3 barcos: Submarino, Fragata y Destructor.");
        Console.WriteLine("2. Se tiene dos tableros: flota naval y tablero de ataque.");
        Console.WriteLine("   - Flota naval: Se tiene el mapa de los barcos a defender. Se mostrará si alguno de los barcos fue atacado por el enemigo");
        Console.WriteLine("   - Tablero de ataque: Según la simbología, mostrará los aciertos y fallos de los misiles lanzados");
        Console.WriteLine("3. El objetivo es hundir todos los barcos del enemigo antes que él hunda los tuyos.");
        Console.WriteLine("4. Por turnos, cada jugador dispara un misil en las coordenadas que elija.");
        Console.WriteLine("5. La simbología utilizada es:");
        Console.WriteLine("   - ✦ : Submarino, dos casillas horizontales (2 puntos al hundirlo)");
        Console.WriteLine("   - ★ : Fragata, tres casillas horizontales (3 puntos al hundirla)");
        Console.WriteLine("   - ✸ : Destructor, cuatro casillas verticales (4 puntos al hundirlo)");
        Console.WriteLine("   - ● : Acierto (barco impactado)");
        Console.WriteLine("   - ✖ : Fallo (disparo al agua)");
        Console.WriteLine("   - ~ : Agua (casilla no atacada)");
        Console.WriteLine("6. Cada jugador tiene máximo 15 misiles.");
        Console.WriteLine("7. Gana quien hunda todos los barcos enemigos o tenga más puntos después de 30 turnos.");
        Console.WriteLine("8. Si un jugador se rinde, el otro gana automáticamente con 5 puntos de bonificación.");
        Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
        Console.ReadKey();
        // Limpia la pantalla antes de mostrar el menú principal
        Console.Clear();
        // Vuelve al menú principal llamando al método Main
        Main();
    }

    // Método que valida que la entrada del usuario sea un número dentro del rango permitido
    public static int ValidarEntradaMenu(int numeroLimite)
    {
        // Variable para controlar el bucle de validación
        bool validezNumero = false;
        // Variable para almacenar el número ingresado por el usuario
        int numeroUsuario = 0;

        // Bucle que continúa hasta que se ingrese un número válido
        while (!validezNumero)
        {
            // Solicita al usuario ingresar un número dentro del rango especificado
            Console.Write($"Ingrese un número entre 1 y {numeroLimite}: ");

            // Intenta convertir la entrada del usuario a un número entero
            if (int.TryParse(Console.ReadLine(), out numeroUsuario))
            {
                // Verifica que el número esté dentro del rango permitido
                if (numeroUsuario > 0 && numeroUsuario <= numeroLimite)
                {
                    // Si es válido, marca la bandera para salir del bucle
                    validezNumero = true;
                }
                else
                {
                    // Mensaje de error si el número está fuera del rango
                    Console.WriteLine($"El número debe estar entre 1 y {numeroLimite}.");
                }
            }
            else
            {
                // Mensaje de error si la entrada no es un número válido
                Console.WriteLine("Ingrese un número válido.");
            }
        }

        // Devuelve el número válido ingresado por el usuario
        return numeroUsuario;
    }

    // Método que convierte una coordenada en formato texto (ej: "A-1") a índices numéricos para la matriz
    static int[] ConvertirCoordenada(string coordenada)
    {
        // Formato esperado: "A-1", "B-3", etc.
        // Array para almacenar los índices de fila y columna
        int[] indices = new int[2];

        // Extrae los caracteres de fila y columna de la coordenada
        char filaChar = coordenada[0];
        char columnaChar = coordenada[2];

        // Convierte el carácter de fila (A-F) a su índice numérico (1-6)
        switch (filaChar)
        {
            case 'A':
                indices[0] = 1;
                break;
            case 'B':
                indices[0] = 2;
                break;
            case 'C':
                indices[0] = 3;
                break;
            case 'D':
                indices[0] = 4;
                break;
            case 'E':
                indices[0] = 5;
                break;
            case 'F':
                indices[0] = 6;
                break;
        }

        // Convierte el carácter de columna ('1'-'6') a su valor numérico (1-6)
        indices[1] = int.Parse(columnaChar.ToString());

        // Devuelve el array con los índices convertidos
        return indices;
    }
}

