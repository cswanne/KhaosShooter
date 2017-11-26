using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

    public Transform explosion;
    
	void Update () {
        transform.Rotate(0, 10 * Time.deltaTime, 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Assistant.canisterDestroyTime = Time.time;
        } else if (collision.collider.transform.tag == "Player") {
            Assistant.updateFuel(350);
            Destroy(gameObject);
            Assistant.canisterDestroyTime = Time.time;
        }
    }
}
