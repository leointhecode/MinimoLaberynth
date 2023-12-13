# Documentacion
En los siguientes parrafos encontraras la documentacion del codigo.

# Laberinto

## MazeCell
Marca la celda como visitada, estableciendo la propiedad IsVisited en true.
Desactiva visualmente un bloque no visitado asociado a la celda al desactivar el objeto _UnvisitedBlock.
```
 public void Visit(){
        IsVisited = true;
        _UnvisitedBlock.SetActive(false);
    }
```
Desactiva visualmente la pared izquierda de la celda al desactivar el objeto _LeftWall.
```
 public void ClearLeftWall()
    {
        _LeftWall.SetActive(false);
    }
```
Desactiva visualmente la pared derecha de la celda al desactivar el objeto _RightWall.
```
public void ClearRightWall()
    {
        _RightWall.SetActive(false);
    }
```
Desactiva visualmente la pared frontal de la celda al desactivar el objeto _FrontWall.
```
public void ClearFrontWall()
    {
        _FrontWall.SetActive(false);
    }

```
Desactiva visualmente la pared trasera de la celda al desactivar el objeto _BackWall.
```
public void ClearBackWall()
    {
        _BackWall.SetActive(false);
    }
```

## MazeGenerator
Desactiva la niebla en la escena.
Inicializa la cuadrícula del laberinto y crea instancias de celdas de laberinto en la escena.
Llama a la función GenerateMaze(null, _mazeGrid[0, 0]) para comenzar la generación del laberinto.
Finalmente, construye la malla de navegación (NavMesh) en la superficie asociada al objeto.
```
void Awake()
    {
        RenderSettings.fog = false; // Disable fog

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for(int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
                _mazeGrid[x, z].transform.localPosition = new Vector3(x, 0, z);
            }
        }
        GenerateMaze(null, _mazeGrid[0, 0]);
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
```
Recursivamente genera el laberinto utilizando el algoritmo de "Depth-First Search" (DFS).
Marca la celda actual como visitada.
Llama a la función ClearWalls(previousCell, currentCell) para eliminar las paredes entre la celda actual y la celda anterior.
Encuentra y visita las celdas vecinas no visitadas, repitiendo el proceso hasta que no haya más celdas no visitadas.
```
private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {   
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }
```
Devuelve la siguiente celda no visitada entre las celdas vecinas de la celda actual.
Utiliza una ordenación aleatoria para determinar el orden en que se exploran las celdas vecinas.
```
 private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();

    }
```
Devuelve una colección de celdas vecinas no visitadas de la celda actual.
```
 private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if(x+1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if(cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x-1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if(cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z+1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if(cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }
```
Elimina las paredes entre la celda actual y la celda anterior, según la dirección relativa de las celdas.
Limpia las paredes derecha/izquierda o adelante/atrás, dependiendo de la posición relativa de las celdas.
```
private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }
        if(previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }

    }
```
# Puntaje

## points
Este método se ejecuta cuando un objeto con un collider entra en el collider de este objeto.
Verifica si el objeto que entró tiene la etiqueta "Agent" (por ejemplo, un agente del juego). Si es así, se destruye el objeto actual y se llama a la función RespawnPoint() para hacer respawn del objeto.
También verifica si el objeto que entró tiene la etiqueta "Player" (jugador). Si es así, se destruye el objeto actual, se incrementa la puntuación llamando a CountPoint(), y luego se llama a RespawnPoint() para hacer respawn del objeto.
```

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Agent"))
        {
            Destroy(gameObject);
            RespawnPoint();
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            CountPoint();
            RespawnPoint();
        }

    }
```
Busca un objeto en la escena con la etiqueta "Respawn".
Si se encuentra un objeto, llama a la función SpawnObject() del componente SpawnObjectOnNavMesh asociado al objeto encontrado, lo que debería manejar la lógica de reaparición del objeto en una posición válida del NavMesh.
Si no se encuentra el objeto con la etiqueta "Respawn", muestra un mensaje de error en la consola.
```
 void RespawnPoint()
    {
        GameObject spawnObj = GameObject.FindGameObjectWithTag("Respawn"); 

        if(spawnObj != null)
        {
            spawnObj.GetComponent<SpawnObjectOnNavMesh>().SpawnObject();
        }
        else
        {
            Debug.LogError("Other GameObject reference is null. Assign a valid reference in the Unity Editor.");
        }
    }
```
Busca un objeto en la escena con la etiqueta "PointTextOBJ".
Llama a la función IncreasePoint() del componente PointManager asociado al objeto encontrado, lo que debería manejar la lógica de incrementar la puntuación del jugador.
```
 void CountPoint()
    {
        GameObject pointObj = GameObject.FindGameObjectWithTag("PointTextOBJ");
        pointObj.GetComponent<PointManager>().IncreasePoint();
    }
```
## PointManager

Modifica un componente 'TextMeshProUGUI' para agregar puntos en un marcador.
```
 public void IncreasePoint()
    {
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        ++point;
        textMeshPro.text = "" + point;
    }
```

## SpawnObjectOnNavMesh
Obtiene un punto aleatorio en el NavMesh llamando a la función RandomPointOnNavMesh().
Instancia (Instantiate()) el objeto especificado (objectToSpawn) en la posición aleatoria obtenida.
```
public void SpawnObject()
    {
        // Get a random point on the NavMesh
        Vector3 randomPoint = RandomPointOnNavMesh();

        // Instantiate the object at the random point
        Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
    }
```
Calcula un punto aleatorio dentro del NavMesh utilizando triangulación de la malla de navegación (NavMeshTriangulation).
Obtiene un índice aleatorio de triángulo en la malla de navegación.
Recupera los vértices del triángulo seleccionado.
Genera coordenadas bariocéntricas aleatorias dentro del triángulo.
Calcula el punto aleatorio dentro del triángulo utilizando las coordenadas bariocéntricas.
Devuelve el punto aleatorio dentro del NavMesh.
```
Vector3 RandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Get a random triangle index
        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3);

        // Get the vertices of the selected triangle
        Vector3[] triangleVertices = {
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3]],
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 1]],
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 2]]
        };

        // Get random barycentric coordinates within the triangle
        Vector2 randomBarycentric = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        randomBarycentric.Normalize();

        // Calculate the random point within the triangle
        Vector3 randomPoint = triangleVertices[0] +
                              randomBarycentric.x * (triangleVertices[1] - triangleVertices[0]) +
                              randomBarycentric.y * (triangleVertices[2] - triangleVertices[0]);

        return randomPoint;
    }
```

# Jugador
```
void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate moveDirection
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;


        // Move the cube
        transform.Translate(new Vector3(- moveDirection.x, 0f, - moveDirection.y) * moveSpeed * Time.deltaTime);
    }
```

## PlayerNavigation
Este método se llama en cada frame.
Obtiene la entrada del teclado horizontal (Input.GetAxis("Horizontal")) y vertical (Input.GetAxis("Vertical")).
Calcula la dirección normalizada del movimiento (moveDirection) utilizando un Vector2.
Mueve el objeto (presumiblemente el jugador) en la escena en función de la dirección del movimiento y la velocidad especificada.
```
void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate moveDirection
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;


        // Move the cube
        transform.Translate(new Vector3(- moveDirection.x, 0f, - moveDirection.y) * moveSpeed * Time.deltaTime);
    }
```
Se ejecuta cuando el objeto con este script colisiona con otro objeto que tiene un collider.
Verifica si el objeto con el que colisionó tiene la etiqueta "Agent".
Si es así, llama al método GameFinish() para manejar la lógica de finalización del juego.
```
void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            // Handle game finish logic here (e.g., show game over screen, restart level, etc.)
            GameFinish();
        }
    }
```
Contiene la lógica para finalizar el juego.
En este caso, simplemente imprime "Game Over" en la consola usando Debug.Log().
Puedes agregar más lógica para finalizar el juego, como mostrar una pantalla de Game Over, reiniciar el nivel, etc.
```
 void GameFinish()
    {
        // Add code to finish the game (e.g., display game over UI, reset level, etc.)
        Debug.Log("Game Over");
        // You can add more game finish logic here.
}
```

# Enemigos

## AgentStrong
Este método se llama en cada frame.
Llama a dos métodos, PatrolAgent() y PursuePlayer(), para gestionar el comportamiento del agente.
```
void Update()
    {
        PatrolAgent();
        PursuePlayer();
    }
```
Calcula la distancia entre el agente y el jugador (distanceToPlayer).
Si la distancia al jugador es menor que el radio de persecución (pursuitRadius), establece la posición de destino del NavMeshAgent para que persiga al jugador.
En este método, se comenta la línea "// Implementar dash attack", indicando que aquí debería implementarse la lógica para un ataque rápido o "dash".
```
void PursuePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < pursuitRadius)
        {
            GetComponent<NavMeshAgent>().destination = player.position;
            // Implementar dash attack
        }
    }
```
Verifica si el agente ha alcanzado su destino actual (dentro de la distancia de parada del NavMeshAgent).
Si es así, genera un nuevo punto de destino aleatorio dentro de un radio de patrulla y establece ese punto como destino para el NavMeshAgent.
```
void PatrolAgent()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(agent.transform.position, patrolRadius, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

    }
```
Genera un punto aleatorio en una esfera alrededor de un centro dado (center) dentro de un radio especificado (patrolRadius).
Utiliza Random.insideUnitSphere para obtener un punto aleatorio en la esfera.
Utiliza NavMesh.SamplePosition para proyectar el punto a la superficie del NavMesh y garantizar que esté en un lugar caminable.
```
bool RandomPoint(Vector3 center, float patrolRadius, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * patrolRadius; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
```

## AgentHunter
Este método se llama en cada frame.
Busca un objeto en la escena con la etiqueta "Point" utilizando GameObject.FindGameObjectWithTag("Point").
Establece la posición de destino del componente NavMeshAgent asociado a este objeto al transform de la posición del objeto "Point".
En resumen, este script hace que el agente se mueva hacia el objeto etiquetado como "Point" en la escena utilizando la navegación del NavMesh.
```
void Update()
    {

        GameObject point = GameObject.FindGameObjectWithTag("Point");
        GetComponent<NavMeshAgent>().destination = point.transform.position;
    }
```
