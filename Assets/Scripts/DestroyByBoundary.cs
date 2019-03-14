using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    // de: https://docs.unity3d.com/ScriptReference/Collider.OnTriggerExit.html
    void OnTriggerExit (Collider other) {
        Destroy(other.gameObject);
	}
	
}
