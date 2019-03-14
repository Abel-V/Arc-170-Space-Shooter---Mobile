using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;     //declaración de variables globales para todas las funciones

	// Use this for initialization
	void Start () {         // Start: función/método de inicialización, que no devuelve (return) nada de nada, por eso se pone void

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;    //transform.forward equivale a que se va a mover hacia el eje z

        //rb.velocity = transform.forward * speed * Time.deltaTime;
        //lo multiplico por * Time.deltaTime para smoothearlo, y que no haya trancones (sino, se mueve por cada frame, pero si un frame tarda más en procesarse, hay trancones)
        //lo desactivo pero podría usarlo (aumentando speed bastante, eso si)
    }

    /*  Update se ejecutaría una vez por cada frame. Esto sería otra función/método, en caso de necesitarla.
     *  UPDATE: para actualizar movimiento de cosas no-físicas, etc. Intervalos pueden variar (si un frame tarda más en procesarse que otro)
     *  FIXEDUPDATE: para actualizar movimiento de cosas bajo leyes de la física (Rigidbodys). Intervalos constantes
    
    // Update is called once per frame
    void Update () {
		
	}

    // Si quisiera hacer una función con return (que no sea void):

    int MultiplyByTwo (int number)
    {
        return number*2;
    }

    */
}