using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollide : MonoBehaviour {

    public GameObject explosion;
    public int hitPoints = 3;
    public float minSpeed;
    public float maxSpeed;
    public int damage = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        Destroy(this.gameObject, 30);
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * Random.Range(minSpeed, maxSpeed);
    }

    private void Fragment(GameObject asteriod)
    {
        float scale = 0.3f;
        int force = 3;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(asteriod);
        for (var i = 1; i < 7; i++) {
            GameObject clone = Instantiate(asteriod, transform.position, transform.rotation, transform.parent);
            clone.tag = "Boulder";
            clone.transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
            Destroy(clone.gameObject, 20);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.mass = 1;
            rb.constraints = 0;
            int aForceX = Random.Range(-(i * force), (i * force));
            int aForceY = Random.Range(-(i * force), (i * force));
            rb.AddForce(new Vector2(aForceX, aForceY), ForceMode2D.Impulse);
            rb.angularVelocity = 180;
        }
    }

    private void Update()
    {
        if (rb.angularVelocity != 180) {
            rb.angularVelocity = 180;
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string contactTag = collision.gameObject.tag;
        if (contactTag == "Bolt" && this.tag == "Enemy") { 
            hitPoints--;
            Instantiate(explosion, transform.position, transform.rotation);
            if (hitPoints <= 0) { 
                Destroy(collision.gameObject); //bolt
                Fragment(this.gameObject);
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
