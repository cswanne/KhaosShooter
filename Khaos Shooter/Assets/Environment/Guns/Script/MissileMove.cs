using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour {


    public float startSpeed;
    public float missileSpeed;
    Rigidbody2D body;

    public float speed = 5;
    public float rotatingSpeed = 1;
    public GameObject target;

    private bool onMyWay;

    void Start ()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.right * startSpeed;
        StartCoroutine("missile");
        onMyWay = false;
    }
	
	IEnumerator missile()
    {
        yield return new WaitForSeconds(2);
        body.velocity = transform.right * missileSpeed;
        yield return new WaitForSeconds(0.5f);
        onMyWay = true;
    }

    // Update is called once per frame
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
        int i = 0;
    }

}
