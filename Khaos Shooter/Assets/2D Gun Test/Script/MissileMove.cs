using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour {


    public float startSpeed;
    public float missileSpeed;
    Rigidbody body;
    

	void Start ()
    {

        body = this.gameObject.GetComponent<Rigidbody>();
        body.velocity = transform.right * startSpeed;
        StartCoroutine("missile");
    }
	
	IEnumerator missile()
    {
        yield return new WaitForSeconds(2);
        body.velocity = transform.right * missileSpeed;
    }
}
