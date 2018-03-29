﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{


    public float startSpeed;
    public float missileSpeed;
    Rigidbody2D body;

    public float speed = 5;
    public float rotatingSpeed = 1;
    private GameObject player;
    private GameObject target;
    public GameObject explosion;
    public int hitPoints = 2;
    public int fuel = 10;

    private bool onMyWay;
    private float nextActionTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            target = player;
        if (target == null) Destroy(gameObject);
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.right * startSpeed;

        StartCoroutine("Missile");
        onMyWay = false;
        nextActionTime = Time.time;
        fuel = 10;
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
        if (target == null) {
            target = player;
        }
        if (onMyWay && target != null) {
            Vector2 pointToTarget = (Vector2)transform.position - (Vector2)target.transform.position;
            pointToTarget.Normalize();
            float value = Vector3.Cross(pointToTarget, transform.right).z;
            body.angularVelocity = rotatingSpeed * value;
            body.velocity = transform.right * speed;
        }
        if (Time.time > nextActionTime) {
            nextActionTime += 1;
            fuel -= 1;
            if (fuel < 0) {
                target = null;
                body.angularVelocity = transform.right.x * 30f;
                body.gravityScale = 0.5f;
                Destroy(gameObject, 4);
            }
        }

        if (Assistant.lookForChaff) {
            Assistant.lookForChaff = false;
            GameObject[] chaff = GameObject.FindGameObjectsWithTag("Chaff");
            foreach (GameObject go in chaff) {
                target = go;
                break;
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Chaff") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
