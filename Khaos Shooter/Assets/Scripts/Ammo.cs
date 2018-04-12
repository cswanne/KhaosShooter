using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public Transform explosion;

    void Update()
    {
        transform.Rotate(0, 10 * Time.deltaTime, 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (collision.collider.transform.tag == "Player") {
            Assistant.updateAmmo(10);
            Destroy(gameObject);
        }
    }
}
