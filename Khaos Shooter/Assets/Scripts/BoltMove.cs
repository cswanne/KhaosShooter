using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMove : MonoBehaviour {

    public float speed;
    Rigidbody2D body;


	void Start ()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int i = 0;
    }

}
