using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController; //instanciamos un script GameController. ver el link de Start

    void Start()        //https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/counting-points-and-displaying-score?playlist=17147
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Destroy everything that enters the trigger       ...de https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name); //ya no hace falta, era con lo que nos dimos cuenta de que el boundary estaba destruyendo el asteroide desde el segundo 1 y viceversa
        //if (other.tag == "Boundary") //más lento computacionalmente
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;     //si es el mesh collider del Boundary el que ha "entrado" en el mesh collider del asteroide, salimos de esta función con return, para que no se destruyan
        }

        if (explosion != null) //el prefab EnemyBolt también usa este script, pero no queremos que explote al destruirse, por lo que hacemos esto para poder dejar la asignación en blanco
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        gameController.AddScore(scoreValue);

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            gameController.GameOver();  //https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/ending-game?playlist=17147
        }

        Destroy(other.gameObject);  //destruye el gameObject de other, que es el disparo laser
        Destroy(gameObject);    //Destruye el gameObject, que es el objeto de este script, o sea, el asteroide
    }
}
