using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int scoreValue;
    //public int boulderHitCount = 0;
    private GameController gameController = null;
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

    private void Fragment(GameObject asteriod)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(asteriod);
        for (var i = 0; i < 6; i++) {
            GameObject clone = Instantiate(asteriod, transform.position, transform.rotation, transform.parent);
            clone.tag = "Boulder";
            clone.transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(Random.Range(-(i * 10), (i * 10)), Random.Range(-(i * 10), (i * 10)), 0), ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy" && this.tag == "Enemy") { //two asteriods
            Fragment(transform.gameObject);
        } else if (collision.collider.transform.tag == "Boulder" && this.tag == "Enemy") { //boulder hitting asteriod
            Fragment(transform.gameObject);
        } else if (collision.collider.transform.tag == "Boulder" && this.tag == "Boulder") { //do nothing
        } else if (collision.collider.transform.tag != "Boulder" && this.tag == "Boulder") { //anything else
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
        if (gameController != null)
            gameController.AddScore(scoreValue);
        Destroy(other.gameObject); //asteriod
        Destroy(this.gameObject); //bullet
    }

}
