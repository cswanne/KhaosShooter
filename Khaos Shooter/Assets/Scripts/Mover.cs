using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float minSpeed;
    public float maxSpeed;
    Rigidbody body;


	void Start ()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
        body.velocity = -transform.right * Random.Range(minSpeed, maxSpeed);
    }
	
	
}
