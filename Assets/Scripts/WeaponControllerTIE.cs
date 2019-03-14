using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerTIE : MonoBehaviour {

    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    [Range(0.1f, 2f)] public float maxFireRate; //Fire rate = inverso de tiempo entre disparos
    public float maxDelay; //tiempo que tarda en hacer el primer disparo

    //Hemos hecho este WeaponController para las naves enemigas

    void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", Random.Range(0f, maxDelay), Random.Range(1/0.1f, 1/maxFireRate));
	}

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
