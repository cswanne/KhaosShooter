using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStart : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            MonoBehaviour script = this.transform.parent.GetComponent<MonoBehaviour>();
            (script as Asteroid).startStop();
        }
    }
}
