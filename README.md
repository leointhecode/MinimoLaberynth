# MinimoLaberynth
 **Título del Juego: "MinimoLaberynth"**

**Experiencia Deseada:**
"MinimoLaberynth" busca transmitir una experiencia de relajación, diversión y desafío. El jugador se sumergirá en un mundo de laberintos generados de forma procedural, donde podrá perderse, explorar, y disfrutar de un ambiente que lo ayude a escapar del estrés diario. La meta principal es que los jugadores se relajen, mientras también disfrutan de un desafío entretenido.

**Descripción General del Juego:**
El juego "Laberinto de la Serenidad" es un juego de laberintos en 3D que utiliza la generación procedural para crear niveles únicos en cada partida. El entorno del juego tiene una estética relajante minimalista con colores solidos.

**Lentesl:**
1. **Lente de la Emoción:** El juego busca evocar emociones de relajación, asombro y satisfacción a medida que los jugadores resuelven los laberintos.

2. **Lente del Comportamiento:** Los jugadores deben demostrar habilidades de resolución de problemas, paciencia y exploración para superar los obstáculos del laberinto y avanzar.

3. **Lente de la Estética:** El juego utiliza una estética visual y sonora que fomenta la relajación, con estilo minimalista y una música suave de fondo.

4. **Lente del Espacio:** Cada laberinto se siente único y se adapta a la temática del entorno, creando un espacio atractivo para la exploración.

**Objetivo del Juego:**
El objetivo principal de " MinimoLaberynth"" es guiar al jugador a través de un laberinto generado proceduralmente hacia la salida, fomentando la relajación y la diversión. No hay límite de tiempo ni enemigos, lo que permite que los jugadores avancen a su propio ritmo.

**Reglas:**
1. Los jugadores deben navegar por el laberinto y encontrar la salida.
2. No hay límite de tiempo ni vidas, por lo que los jugadores pueden explorar y disfrutar del juego sin presión.
3. El juego recompensa la exploración y el descubrimiento con elementos visuales.

**Procedimientos:**
- Los laberintos se generan proceduralmente en cada partida para garantizar la variedad.
- Los jugadores pueden utilizar controles simples para mover el laberinto
- El juego registra el tiempo que toma a los jugadores completar cada laberinto y les permite competir por tiempos más bajos.

**Recursos:**
- Gráficos de alta calidad para crear un entorno visualmente atractivo y relajante.
- Música ambiental suave y efectos de sonido que complementan la estética del juego.
- Controles intuitivos que permiten una experiencia de juego sin complicaciones.

**Límites:**
- No se permite la interacción violenta ni la competencia directa entre jugadores.

**Conflicto:**
El conflicto en el juego se centra en el desafío de navegar por los laberintos y superar obstáculos, pero no hay conflicto interpersonal ni violencia en el juego.

**Resultados o Desenlace:**
El juego no tiene un desenlace específico, ya que los jugadores pueden seguir explorando y disfrutando de nuevos laberintos generados proceduralmente durante el tiempo que deseen. Los resultados se miden en términos de tiempo y eficiencia al completar los laberintos, lo que permite a los jugadores competir consigo mismos y con otros.

**Personajes:**
Eventualmente se podrán modificar las texturas del personaje “bolita”

**Tecnica Elegida**
El algoritmo comienza creando una cuadrícula de celdas no visitadas del laberinto, después se genera el laberinto a partir de la celda superior izquierda encontrando una celda vecina no visitada al azar y derribando la pared. El proceso continúa hasta que todas las celdas han sido visitadas exactamente una vez y el laberinto está terminado. El componente final de la celda del laberinto se añade a la escena, y el objeto celda del laberinto se crea con todos los componentes necesarios del objeto del juego, incluyendo un script para ayudar al algoritmo generador del laberinto a derribar paredes y crear el laberinto.

**Algoritmo**
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
**Implementacion del codigo**
Para ello visita nuestro MD [documentacion](Documentacion.md)
