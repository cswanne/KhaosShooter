using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollide : MonoBehaviour {

    public GameObject explosion;
    public int hitPoints = 3;

    private void Start()
    {
    }

    private void Fragment(GameObject asteriod)
    {
        float scale = 0.3f;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(asteriod);
        for (var i = 0; i < 6; i++) {
            GameObject clone = Instantiate(asteriod, transform.position, transform.rotation, transform.parent);
            clone.tag = "Boulder";
            clone.transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            //rb.AddForce(new Vector3(Random.Range(-(i * 10), (i * 10)), Random.Range(-(i * 10), (i * 10)), 0));
            rb.AddForce(new Vector2(Random.Range(-(i * 10), (i * 10)), Random.Range(-(i * 10), (i * 10))), ForceMode2D.Impulse);
            rb.velocity = -transform.right * 5;

            rb.angularVelocity = 180;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string contactTag = collision.gameObject.tag;
        if (contactTag == "Bolt" && this.tag == "Enemy") { //two asteriods
            hitPoints--;
            Instantiate(explosion, transform.position, transform.rotation);
            if (hitPoints <= 0) { 
                Destroy(collision.gameObject); //bolt
                Destroy(this.gameObject); //asteroid
            } else {
                Rigidbody2D body = transform.GetComponent<Rigidbody2D>();
                body.velocity = -transform.right * Random.Range(1, 10);
                body.angularVelocity = Random.Range(90, 180);
            }
        } else if (contactTag == "Enemy" && this.tag == "Enemy") { //two asteriods
            Fragment(transform.gameObject);
        } else if (contactTag == "Boulder" && this.tag == "Enemy") { //boulder hitting asteriod
            Fragment(transform.gameObject);
        } else if (contactTag == "Boulder" && this.tag == "Boulder") { //do nothing
        } else if (contactTag != "Boulder" && this.tag == "Boulder") { //anything else
            Destroy(transform.gameObject);
        }
    }

}
