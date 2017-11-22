using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(0, 0, 0) ;
        var rot = transform.rotation;
        rot.x += Time.deltaTime * 0.1f;
        transform.rotation = rot;


        var pos = transform.position;
        pos.x += Time.deltaTime * 0.1f;
        pos.z += Time.deltaTime * 1f;
        transform.position = pos;

    }
}

