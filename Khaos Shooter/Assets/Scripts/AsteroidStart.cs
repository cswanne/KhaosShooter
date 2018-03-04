using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStart : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            MonoBehaviour script = this.transform.parent.GetComponent<MonoBehaviour>();
            (script as Asteroid).startStop();
        }
    }
}
