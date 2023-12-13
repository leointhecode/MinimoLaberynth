# MinimoLaberynth
 **Título del Juego: "MinimoLaberynth"**

**Experiencia Deseada:**
"MinimoLaberynth" busca ofrecer una experiencia única al combinar la generación procedural de laberintos con avanzadas técnicas de inteligencia artificial para la implementación de enemigos. El jugador se sumerge en un mundo de laberintos generados proceduralmente, donde puede perderse, explorar y disfrutar de un ambiente que ayuda a escapar del estrés diario. La meta principal es que los jugadores se relajen ganan puntos en un desafío entretenido.

**Descripción General del Juego:**
"Laberinto de la Serenidad" es un juego de laberintos en 3D que utiliza la generación procedural para crear niveles únicos en cada partida en las que tendra que sumar puntos y evitar a los enemigos. El entorno del juego tiene una estética minimalista con colores sólidos, buscando transmitir una sensación de tranquilidad.

**Lentes:**

Lente de la Emoción: El juego busca evocar emociones de relajación, asombro y satisfacción a medida que los jugadores resuelven los laberintos. La presencia de enemigos agrega una capa de emoción al introducir elementos de sorpresa y estrategia, desafiando a los jugadores a mantener la calma en situaciones dinámicas.

Lente del Comportamiento: Los jugadores deben demostrar habilidades de resolución de problemas, paciencia y exploración para superar los obstáculos del laberinto y avanzar. La incorporación de enemigos introduce un componente estratégico, ya que los jugadores deben evitar ser capturados para mantener su calificación.

Lente de la Estética: El juego utiliza una estética visual y sonora que fomenta la relajación, con un estilo minimalista y una música suave de fondo. A medida que los enemigos persiguen al jugador, la música podria intensificarse para mantener la coherencia estética y aumentar la emoción.

Lente del Espacio: Cada laberinto se siente único y se adapta a la temática del entorno, creando un espacio atractivo para la exploración. La presencia de enemigos contribuye a la dinámica del espacio, ya que los jugadores deben tener en cuenta la ubicación de los enemigos al planificar su ruta.

Objetivo del Juego: La meta principal de "MinimoLaberynth" es guiar al jugador a través de un laberinto generado proceduralmente hacia 'puntos', fomentando la relajación y la diversión. deben evitar que los enemigos les roben puntos para mantener una puntuación alta.

**Reglas:**

Los jugadores deben navegar por el laberinto recogiendo la mayor cantidad de puntos posibles
No hay límite de tiempo, permitiendo a los jugadores disfrutar del juego sin presión.
Los enemigos pueden robar puntos al jugador si son capturados, incentivando a los jugadores a evitar encuentros directos y adoptar estrategias cautelosas.

**Procedimientos:**

Los laberintos se generan proceduralmente en cada partida para garantizar la variedad, utilizando algoritmos avanzados que crean estructuras laberínticas complejas y desafiantes.
Los jugadores pueden utilizar controles simples para moverse en el laberinto, interactuando con el entorno generado de manera dinámica.

Tambien se utilizan tecnicas de busqueda de caminos para los enemigos con caracter de buscador y steering behaviors de patrullaje y persecucion a los enemigos cazadores.
Finalmente se agrego una maquina de estado para que el comportamiento fuese variado.

**Recursos:**

Gráficos de alta calidad para crear un entorno visualmente atractivo y relajante, con detalles generados proceduralmente para mantener la frescura en cada partida.
Controles intuitivos que permiten una experiencia de juego sin complicaciones, permitiendo a los jugadores centrarse en la exploración y resolución de problemas.

**Límites:**
No se permite la interacción violenta ni la competencia directa entre jugadores.

**Implementación de Enemigos:**

Para intensificar la experiencia, se ha introducido un enemigo tipo "cazador" que utiliza un avanzado comportamiento de steering, combinando patrullaje y persecución. Este enemigo sigue estratégicamente al jugador, adaptándose a sus movimientos y creando un desafío dinámico.

Adicionalmente, se ha implementado un enemigo tipo "buscador" que utiliza técnicas de inteligencia artificial de búsqueda de caminos. Este enemigo planifica rutas eficientes para perseguir los puntos, añadiendo capas de complejidad a la exploración y aumentando la tensión.

Estas adiciones enriquecen la experiencia de "MinimoLaberynth", proporcionando desafíos emocionantes y estratégicos mientras se navega por los laberintos generados proceduralmente.

**Algoritmo**
Generacion procedural
```
Función GenerateMaze(previousCell, currentCell)
    Marcar currentCell como visitada
    Eliminar las paredes entre previousCell y currentCell

    Esperar un corto tiempo para la animación

    Repetir mientras haya celdas no visitadas:
        Obtener nextCell como una celda no visitada adyacente a currentCell

        Si nextCell no es nulo:
            Llamar recursivamente GenerateMaze(currentCell, nextCell)

Función GetNextUnvisitedCell(currentCell)
    Obtener una lista de celdas no visitadas adyacentes a currentCell
    Barajar aleatoriamente la lista
    Devolver la primera celda de la lista barajada

Función GetUnvisitedCells(currentCell)
    Para cada celda adyacente a currentCell:
        Si la celda no ha sido visitada, agregarla a la lista de celdas no visitadas

    Devolver la lista de celdas no visitadas

Función ClearWalls(previousCell, currentCell)
    Si previousCell es nulo (primera celda):
        No hacer nada
    Si previousCell está a la izquierda de currentCell:
        Eliminar la pared derecha de previousCell y la pared izquierda de currentCell
    Si previousCell está a la derecha de currentCell:
        Eliminar la pared izquierda de previousCell y la pared derecha de currentCell
    Si previousCell está en la parte frontal de currentCell:
        Eliminar la pared trasera de previousCell y la pared frontal de currentCell
    Si previousCell está en la parte trasera de currentCell:
        Eliminar la pared frontal de previousCell y la pared trasera de currentCell

Función Principal
    Desactivar la niebla (fog) en el entorno
    Crear una cuadrícula de celdas de laberinto (_mazeGrid) con el ancho y la profundidad especificados

    Para cada celda en la cuadrícula:
        Crear una instancia de MazeCell en la posición correspondiente

    Llamar a GenerateMaze con la primera celda como currentCell


```

steering behavior y maquina de estado

```

    void Update()
    {
        PatrolAgent();
        PursuePlayer();
    }

    void PursuePlayer()
    {
        calcula posicion del jugador
        if (distanceToPlayer < pursuitRadius)
        {
            perseguir jugador;
        }
    }

    void PatrolAgent()
    {
        if (jugador no esta en el pursuitradius) //done with path
        {
            Vector3 point;
            dame un punto aleatorio en mi rango de patrullaje //pass in our centre point and radius of area
            {
               traza un rayo hacia el //so you can see with gizmos
                ve al punto aleatorio
            }
        }

    }

```

busqueda de caminos (se uso un navmesh)
```

void Update()
    {

        encuentra el punto a obtener;
        dirigete al punto en el camino mas corto;
    }


```
**Implementacion del codigo**
Para ello visita nuestro MD [documentacion](Documentacion.md)
