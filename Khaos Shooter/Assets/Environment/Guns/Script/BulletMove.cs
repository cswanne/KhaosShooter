using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {


    public float speed;
    Rigidbody body;
    

	void Start ()
    {

        body = this.gameObject.GetComponent<Rigidbody>();
        body.velocity = transform.right * speed;
    }
	
	
}
