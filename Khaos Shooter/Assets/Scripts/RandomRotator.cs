using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;
    Rigidbody body;
    
	void Start ()
    {
        body = this.GetComponent<Rigidbody>();
        body.angularVelocity = Random.insideUnitSphere * tumble;
        

	}
	
	
}
