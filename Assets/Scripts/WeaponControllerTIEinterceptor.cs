using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerTIEinterceptor : MonoBehaviour
{

    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    [Range(0.1f, 2f)] public float maxFireRate; //Fire rate = inverso de tiempo entre disparos
    public float maxDelay; //tiempo que tarda en hacer el primer disparo
    private float delay;
    private float fireRate;

    //Hemos hecho este WeaponController para las naves enemigas

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        delay = Random.Range(0f, maxDelay);
        fireRate = Random.Range(1 / 0.1f, 1 / maxFireRate);
        InvokeRepeating("Fire1", delay, fireRate);
        InvokeRepeating("Fire2", delay + 0.2f, fireRate);
    }

    void Fire1()
    {
        Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
        audioSource.Play();
    }

    void Fire2()
    {
        Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
        audioSource.Play();
    }
}