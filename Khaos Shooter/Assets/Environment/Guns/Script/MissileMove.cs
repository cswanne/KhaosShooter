using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour {


    public float startSpeed;
    public float missileSpeed;
    Rigidbody2D body;

    public float speed = 5;
    public float rotatingSpeed = 1;
    private GameObject target;
    public GameObject explosion;
    public int hitPoints = 2;

    private bool onMyWay;

    void Start ()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        if (target == null) Destroy(gameObject);
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.right * startSpeed;
        StartCoroutine("Missile");
        onMyWay = false;
    }
	
	IEnumerator Missile()
    {
        yield return new WaitForSeconds(2);
        body.velocity = transform.right * missileSpeed;
        yield return new WaitForSeconds(0.5f);
        onMyWay = true;
    }

    void Update()
    {
        if (onMyWay && target != null) {
            Vector2 pointToTarget = (Vector2)transform.position - (Vector2)target.transform.position;
            pointToTarget.Normalize();
            float value = Vector3.Cross(pointToTarget, transform.right).z;
            body.angularVelocity = rotatingSpeed * value;
            body.velocity = transform.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bolt") {
            hitPoints--;
            Instantiate(explosion, transform.position, transform.rotation);
            explosion.transform.localScale = explosion.transform.localScale * 0.1f;
        } else {
            hitPoints = 0;
        }
        if (hitPoints <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
