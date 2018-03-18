using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMove : MonoBehaviour {

    public float speed;
	void Start ()
    {
        Rigidbody2D body;
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.forward * speed;
    }

}
