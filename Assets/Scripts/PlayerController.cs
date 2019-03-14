using System.Collections;   //namespaces
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //esto lo hacemos para que la clase Boundary aparezca como un desplegable en la ventana Inspector de Unity
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {     // : MonoBehaviour indica que esta clase hereda de MonoBehaviour (padre). 

    internal int playerControllerId;

    [Range(0f, 30f)] public float speed; 
    //[Range(0f, 30f)] = ATRIBUTO, para limitar el valor de Speed. En el inspector, aparecerá como una barrita que se puede arrastrar de 0 a 30. 10 está bien.
    public Boundary boundary;   //Boundary = tipo, definido por el nombre de la clase. boundary = nombre de nuestra referencia
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public float fireRate;          
    //public: esta variable se podrá ver desde fuera de esta clase y desde otros scripts. También aparecerá en la ventana Inspector de Unity, donde se le podrá dar valor
    private float nextFire;         
    //Private: solo se podrá ver/usar dentro de esta clase. SI AL DECLARARLA SOLO ESCRIBIMOS float nextFire, SERÁ PRIVATE IGUALMENTE, POR DEFECTO
    private bool ready1 = true;
    private bool ready2 = false;

    private Quaternion calibrationQuaternion;

    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton;

    private void Start()
    {
        CalibrateAccelerometer();
    }


    //parte de los disparos
    void Update()   //Unity ejecuta esta función justo antes de actualizar cada frame
    {
        Disparar();
        
    }

    void FixedUpdate()
    {
        /*      //PARA .EXE
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        */

        Rigidbody rb = GetComponent<Rigidbody>();
        /*
        Vector3 accelerationRaw = Input.acceleration; //acelerómetro del móvil
        Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        */
        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        rb.velocity = movement * speed; //aquí también podría usarse la multiplicación por Time.deltaTime para smoothear

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    //calibración del acelerómetro
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    private void Disparar()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (areaButton.CanFire() && Time.time > nextFire && ready1 == true)       // cogido de: https://docs.unity3d.com/ScriptReference/Input.GetButton.html
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            audioSource.Play();

            ready1 = false; //parte añadida por mí para la 2a arma
            ready2 = true;
        }

        //parte añadida por mí para la 2a arma
        if (areaButton.CanFire() && Time.time > nextFire && ready2 == true) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
            audioSource.Play();

            ready2 = false;
            ready1 = true;
        }

        //PARA .EXE
        /*
        if (Input.GetButton("Fire1") && Time.time > nextFire && ready1 == true)       // cogido de: https://docs.unity3d.com/ScriptReference/Input.GetButton.html
        {
            nextFire = Time.time + fireRate;
            // GameObject clone = 
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation); // as GameObject;
            //Instantiate(object, position, rotation);
            audioSource.Play();

            ready1 = false; //parte añadida por mí para la 2a arma
            ready2 = true;
        }

        //parte añadida por mí para la 2a arma
        if (Input.GetButton("Fire1") && Time.time > nextFire && ready2 == true) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
            audioSource.Play();

            ready2 = false;
            ready1 = true;
        }
        */
    }

}

/* 
 Más info sobre todo esto en: https://unity3d.com/es/learn/tutorials/projects/space-shooter-tutorial/moving-player?playlist=17147
 y la parte de los disparos, añadida posteriormente, en: https://unity3d.com/es/learn/tutorials/projects/space-shooter/shooting-shots?playlist=17147
*/