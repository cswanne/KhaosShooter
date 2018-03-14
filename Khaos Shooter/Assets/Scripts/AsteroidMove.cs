using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    Rigidbody2D body;


    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = -transform.right * Random.Range(minSpeed, maxSpeed);
        body.angularVelocity = Random.Range(90, 180);
    }


}
