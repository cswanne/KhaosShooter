using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;
    private float scale = 0.3f;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Unable to find 'GameController' script");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy" && this.tag != "Boulder") {
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject self = transform.gameObject;
            Destroy(self);
            for (var i = 0; i < 4; i++) {
                GameObject clone = Instantiate(self, transform.position, transform.rotation, transform.parent);
                clone.tag = "Boulder";
                CapsuleCollider cc = clone.GetComponent<CapsuleCollider>();
                //cc.enabled = false;
                clone.transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
                Rigidbody rb = clone.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0));
            }
        } else if (collision.collider.transform.tag != "Boulder" && this.tag == "Boulder") {
            Destroy(transform.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) //reference other - will be the shot or the player (or maybe even another asteroid?)
    {
        if(other.tag == "Boundary")
        {
            return; //This if statement just means that if the tag is Boundary then ignore and move on
        }

        Instantiate(explosion, transform.position, transform.rotation);
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject); //asteriod
        Destroy(this.gameObject); //bullet
    }

}
