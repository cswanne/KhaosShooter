using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public Transform explosion;
    private Globals globals;

    private void Start()
    {
        GameObject lm = GameObject.Find("LevelManager");
        globals = lm.GetComponent<Globals>();
    }

    void Update()
    {
        transform.Rotate(0, 10 * Time.deltaTime, 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy") {
            Transform clone = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(clone.gameObject, 1);
            Destroy(gameObject);
        } else if (collision.collider.transform.tag == "Player") {
            globals.updateAmmo(10);
            Destroy(gameObject);
        }
    }
}
