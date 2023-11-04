# MazeCell Class

La clase MazeCell representa una celda en un laberinto. Cada instancia de MazeCell tiene propiedades para gestionar las paredes y su estado de visita.

## Propiedades

### IsVisited (Propiedad Pública de Solo Lectura)
Indica si la celda ha sido visitada o no.
```csharp
public bool IsVisited { get; private set; }
```

## Métodos

### Visit
Marca la celda como visitada y desactiva el objeto de bloqueo no visitado.
```csharp
public void Visit(){
    IsVisited = true;
    _UnvisitedBlock.SetActive(false);
}
```

### ClearLeftWall
Elimina la pared izquierda de la celda.
```csharp
public void ClearLeftWall()
{
    _LeftWall.SetActive(false);
}

### ClearRightWall
Elimina la pared derecha de la celda.
```csharp
public void ClearRightWall()
{
    _RightWall.SetActive(false);
}

### ClearFrontWall
Elimina la pared frontal de la celda.
```csharp
public void ClearFrontWall()
{
    _FrontWall.SetActive(false);
}

### ClearBackWall
Elimina la pared trasera de la celda.
```csharp
public void ClearBackWall()
{
    _BackWall.SetActive(false);
}
```

# MazeGenerator Class

La clase MazeGenerator se encarga de generar un laberinto proceduralmente utilizando el algoritmo de exploración recursiva.

## Propiedades

### mazeCellPrefab (Campo serializado)
El prefab de la celda del laberinto que se instanciará.
```csharp
[SerializeField]
private MazeCell _mazeCellPrefab;
```

### mazeWidth (Campo serializado)
El ancho del laberinto en términos de celdas.
```csharp
[SerializeField]
private int _mazeWidth;
```

### mazeDepth (Campo serializado)
La profundidad del laberinto en términos de celdas.
```csharp
[SerializeField]
private int _mazeDepth;
```

## Métodos

### Awake
Método que se llama al inicio del juego para desactivar la niebla en el entorno.
```csharp
private void Awake()
{
    RenderSettings.fog = false; // Disable fog
}
```

### Start
Método de inicio del juego que crea la cuadrícula de celdas del laberinto y comienza la generación del laberinto.
```csharp
private IEnumerator Start()
{
    _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

    for (int x = 0; x < _mazeWidth; x++)
    {
        for(int z = 0; z < _mazeDepth; z++)
        {
            _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
        }
    }
    yield return GenerateMaze(null, _mazeGrid[0, 0]);
}
```

### GenerateMaze
Método que genera el laberinto utilizando la exploración recursiva.
```csharp
private IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
{
    currentCell.Visit();
    ClearWalls(previousCell, currentCell);

    yield return new WaitForSeconds(0.05f);

    MazeCell nextCell;

    do
    {
        nextCell = GetNextUnvisitedCell(currentCell);

        if (nextCell != null)
        {
            yield return GenerateMaze(currentCell, nextCell);
        }
    } while (nextCell != null);
}
```

### GetNextUnvisitedCell
Método que obtiene la próxima celda no visitada de manera aleatoria.
```csharp
private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
{
    var unvisitedCells = GetUnvisitedCells(currentCell);

    return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
}
```

### GetUnvisitedCells
Método que obtiene las celdas no visitadas adyacentes a la celda actual.
```csharp
private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
{
    int x = (int)currentCell.transform.position.x;
    int z = (int)currentCell.transform.position.z;

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

### ClearWalls
Método que elimina las paredes entre dos celdas adyacentes.
```csharp
private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
{
    if (previousCell == null)
    {
        return;
    }
    if(previousCell.transform.position.x < currentCell.transform.position.x)
    {
        previousCell.ClearRightWall();
        currentCell.ClearLeftWall();
        return;
    }
    if (previousCell.transform.position.x > currentCell.transform.position.x)
    {
        previousCell.ClearLeftWall();
        currentCell.ClearRightWall();
        return;
    }

    if (previousCell.transform.position.z < currentCell.transform.position.z)
    {
        previousCell.ClearFrontWall();
        currentCell.ClearBackWall();
        return;
    }
    if (previousCell.transform.position.z > currentCell.transform.position.z)
    {
        previousCell.ClearBackWall();
        currentCell.ClearFrontWall();
        return;
    }
}
```
