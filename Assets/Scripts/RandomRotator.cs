using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;

    // Use this for initialization //START: sólo se ejecuta una vez, al crear el objeto
    void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.angularVelocity = Random.insideUnitSphere * tumble;      //https://docs.unity3d.com/ScriptReference/Random-insideUnitSphere.html
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
