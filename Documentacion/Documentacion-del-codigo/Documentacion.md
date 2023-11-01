# Documentacion
Aqui puedes revisar el esqueleto del juego, el [GDD](/Documentacion/GDD_CubeAway.pdf)

En los siguientes parrafos encontraras la documentacion del codigo.

## GameManager
Funcion que dentro de un rango de 0.5f a 2f crea de manera aleatoria un obstaculo
```
 IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float waitTime = Random.Range(0.5f, 2f);

            yield return new WaitForSeconds(waitTime);

            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
        }
    }
```
 Metodo que aumenta en uno el valor del puntaje del juego.
```
  void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }
```

Metodo principal del juego, es llamado cuando se inicia la escena, activa al personaje y desactiva el boton de la UI, de la misma manera que incia la corutina para spawnear obstaculos e inicia el contador del puntaje.
```
 public void gameStart()
    {
        player.SetActive(true);
        playButton.SetActive(false);

        StartCoroutine("SpawnObstacles");

        InvokeRepeating("ScoreUp", 2f, 1f);
    }

```
Metodo que al cerrar la aplicacion guarda el progreso para el ultimo max score

```
 private void OnApplicationQuit()
    {
       
```
 Metodo que guarda en el dispositivo del usuario el ultimo puntaje y el puntaje maximo.
```

    public void SaveMe()
    {

        PlayerPrefs.SetInt("maxScore", maxScore);     
        PlayerPrefs.SetInt("score", score);

        PlayerPrefs.Save();
    }
```
Recibe un color en forma hexadecimal string y lo convierte a Color
```
public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, a);
    }
```
## Player

Inicializa una variable para el uso posterior del rigid  body en el codigo.
```
private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
```
Mecanica principal del juego, acorde a si se cumple la condicion de interaccion con la pantalla y el valor booleano verdadero de 'CANJUMP' permite el salto del jugador.
```
 void Update()
    {
        if (Input.GetMouseButtonDown(0) && CANJUMP)
        {
            //Jump
            rb.AddForce(Vector3.up * JUMPFORCE, ForceMode.Impulse);
        }
    }
```
Metodo para modificar el valor de 'CANJUMP', al momento en el que player colisiona con el suelo, se activa el condicional para permitir el salto.

```
 private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            CANJUMP = true;
        }
    }
```
Metodo para modificar el valor de 'CANJUMP', al momento en el que player NO esta en colision con el suelo, cambia el valor de 'CANJUMP' a falso.
```
 private void OnCollisionExit(Collision collision)
    {
        CANJUMP = false;
    }
```
Detecta la colision del jugador con un obstaculo, salva los valores del puntaje y reinicia la escena principal.
```
 private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            GameManager.SaveMe();
            SceneManager.LoadScene("Game");
        }
    }
```

## Obstacle
Metodo que hace avanzar a los obstaculos hacia el jugador
```
    void Update()
    {
        transform.Translate(Vector3.forward * SPEED * Time.deltaTime);
    }
```
Metodo que cuando un obstaculo deja de ser visible, destruye el obstaculo.
```
 private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
```
## Color Picker

Modifica el valor de rojo en un preview de RGB
```
    public void OnRedSliderValueChanged()
    {
        selectedColor.r = redSlider.value;
        UpdatePreviewColor();
    }
```
Modifica el valor de verde en un preview de RGB
```
   public void OnGreenSliderValueChanged()
    {
        selectedColor.g = greenSlider.value;
        UpdatePreviewColor();
    }
```
Modifica el valor de azul en un preview de RGB
```
    public void OnBlueSliderValueChanged()
    {
        selectedColor.b = blueSlider.value;
        UpdatePreviewColor();
    }
```
Modifica el valor de alfa en un preview de RGB
```
   public void OnAlphaSliderValueChanged()
    {
        selectedColor.a = alphaSlider.value;
        UpdatePreviewColor();
    }}
```

Metodo que cambia el color de una imagen en la UI con el de los valores modificados por lo metodos SliderValueChanged
```
private void UpdatePreviewColor()
    {
        previewPanel.color = selectedColor;
    }
```

Metodo generico que regresa el color que esta presente en el panel de preview
```
public Color SendColor()
    {
        return selectedColor;
    }
```
## Themes Behavior

El metodo toma una color de la clase Color y lo convierte en una string con el valor hexadecimal del mismo.
```
 public static string ColorToHex(Color color)
    {
        Color32 color32 = (Color32)color;
        return color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");
    
```
El metodo va a tomar una string clave para guardar el nombre y el color en hexadecimal al llamar ColorToHex() y lo guarda en las preferencias del usuario.
```
private void SaveColor(string name,Color color)
    {
        string colorHex = ColorToHex(color);

        // Save the color string to Player Preferences
        PlayerPrefs.SetString(name, colorHex);
        PlayerPrefs.Save();
        Debug.Log("I saved: " + name + " " + colorHex);
    }
```

Al recibir un click un boton de la UI en la ventana de temas, el metodo recibe el color del metodo SendColor() de ColorPicker y lo convierte en el color por defecto del boton, de la misma manera, guarda el valor del con SaveColor()
```
private void ChangeColorOnClick()
    {
        Color newColor = ColorPicker.SendColor();
        buttonToChange.image.color = newColor;

        if (buttonToChange.name == "CubeColor")
        {
            player.GetComponent<Renderer>().material.color = newColor;
            SaveColor("cubecolor", newColor);   
        }
        if (buttonToChange.name == "BGColor")
        {
            cameraMain.backgroundColor = newColor;
            SaveColor("bgcolor",newColor);
        }
        if (buttonToChange.name == "ObstacleColor")
        {
            obstacle.GetComponent<Renderer>().sharedMaterial.color = newColor;
            SaveColor("obstaclecolor",newColor);
        }
        if (buttonToChange.name == "RoadColor")
        {
            road.GetComponent<Renderer>().material.color = newColor;
            SaveColor("roadcolor", newColor);
        }

    }
```


## UIcontroller

Metodo llamado por el GameStart, desactiva todos los componentes de la UI para la partida.

```
    public void DeactivateComponentsStart()
    {
        playButton.SetActive(false);
        maxScoreUI.SetActive(false);
        ThemesButton.SetActive(false);
        OptionsButton.SetActive(false);
        lastScoreText.text = "";

    }
```
Metodo que controla el click de salida y entrada de la ventana de temas, al darle click desactiva y activa los componentes para hacer focus en la ventana de temas.
```
   public void ThemeClick()
    {
        // state of ui
        playState = playButton.activeSelf;
        optionsState = OptionsButton.activeSelf;
        maxScoreUIState = maxScoreUI.activeSelf;
        themeWindowState = ThemeWindow.activeSelf;

        playButton.SetActive(!playState);
        OptionsButton.SetActive(!optionsState);
        maxScoreUI.SetActive(!maxScoreUIState);
        ThemeWindow.SetActive(!themeWindowState);
    }
```
Metodo que controla el click de salida y entrada de la ventana de opciones, al darle click desactiva y activa los componentes para hacer focus en la ventana de opciones.
```
  public void OptionsClick()
    {
        // state of ui
        playState = playButton.activeSelf;
        themesState = ThemesButton.activeSelf;
        maxScoreUIState = maxScoreUI.activeSelf;
        optionsWindowState = OptionsWindow.activeSelf;

        playButton.SetActive(!playState);
        ThemesButton.SetActive(!themesState);
        maxScoreUI.SetActive(!maxScoreUIState);
        OptionsWindow.SetActive(!optionsWindowState);

    }
```

```
void beTransparent(GameObject other)
    {
        Color col = other.GetComponent<Image>().color;
        col.a = 0.5f;
        other.GetComponent<Image>().color = col;
    }
```

Metodo que muestra en el canvas para la UI el puntaje maximo, este es obtenido del dispositivo del usuario si existe.

```
    void MaxScoreMenu()
    {
        if (PlayerPrefs.HasKey("maxScore"))
        {
            maxScore = PlayerPrefs.GetInt("maxScore");
        }

        GameObject maxText = GameObject.Find("maxScoreText");
        maxText.GetComponent<TextMeshProUGUI>().text = maxScore.ToString();
    }
```

Metodo que muestra en el menu el puntaje de la ultima partida despues de jugar, no muestra nada en la primer partida al iniciar la escena.

```
 void ScoreMenu()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            Debug.Log(score);
            if (PlayerPrefs.GetInt("score") > 0)
            {
                lastScoreText.text = PlayerPrefs.GetInt("score").ToString();
            }
        }
    }
```

