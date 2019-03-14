using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //añadido para llamar a Text, dentro de Canvas (UI), porque GUItext está obsoleto

public class GameController : MonoBehaviour {

    public GameObject[] hazards; //array de elementos, para poder usar los 3 prefabs diferentes de asteroides
    public Vector3 spawnValues; //los ponemos desde el Inspector de Unity (y = 0 y Z = 16, fuera del campo. X queremos que sea un valor aleatorio y lo haremos aquí
    private int hazardCount = 4; //la primera oleada tendrá 4 hazards, y las siguientes 1 más cada una
    public Vector2 spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text waveText;
    private int waveCount = 1;

    private bool gameOver;
    private bool restart;
    private int score;

    public GameObject restartButton;

    // Use this for initialization
    void Start () {
        gameOver = false;
        restart = false;
        restartText.text = "";  //al principio, no queremos que aparezca nada escrito
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        restartButton.SetActive(false);
        StartCoroutine(SpawnWaves());
    }

    /*
    void Update()   //https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/ending-game?playlist=17147
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R)) //si hemos apretado la R (después de haber muerto, y nos lo haya sugerido RestartText...
            {
                Application.LoadLevel(Application.loadedLevel);     //volvemos a cargar este mismo nivel 
            }
        }
    }
    */

    IEnumerator SpawnWaves ()   //co-rutina, ver aquí: https://unity3d.com/es/learn/tutorials/topics/scripting/coroutines
    {
        waveText.text = "Wave: " + waveCount; //para que salga Wave 1 desde el principio
        yield return new WaitForSeconds(startWait); //pausa antes de que el juego comience (la estableceremos en el Inspector)
        while (true) //infinite loop, para crear otra oleada de asteroides una vez se acaben los de la primera
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)]; //coge uno de los modelos de hazards (asteroides y TIEs) aleatoriamente
                //Vector3 spawnPosition = new Vector3(x, y, z);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //x: valor aleatorio entre la x y -x que pongamos en el inspector (límites del campo)
                Quaternion spawnRotation = Quaternion.identity; //Quaternion.identity significa sin rotación (el asteroide ya va a tener una de por sí)
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));     //espera para lanzar otro asteroide (para que no se pause el juego entero, necesitamos usar las co-rutinas)
            }
            yield return new WaitForSeconds(waveWait);  //espera antes de mandar la siguiente oleada

            if (gameOver)   //si gameOver == true
            {
                restartButton.SetActive(true);
                //restartText.text = "Press 'R' for Restart";
                restart = true;
                break;  //para el bucle de oleadas de hazards
            }

            hazardCount++; //PARA QUE LA SIGUIENTE OLEADA TENGA UN HAZARD MAS
            waveCount++;
            waveText.text = "Wave: " + waveCount;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
//https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/game-controller?playlist=17147
//https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/counting-points-and-displaying-score?playlist=17147