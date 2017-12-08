using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMove : MonoBehaviour {

    public float maxTurnSpeed = 50f;
    public float maxMoveSpeed = 50f;
    public float jumpForce = 50f;
    public float maxStrafeSpeed = 50f;

    Rigidbody body;
    Camera cam;

    // Use this for initialization
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        // Get the horizontal and vertical axis
        var walk = Input.GetAxis("LeftThumbY") * maxMoveSpeed;
        var turn = Input.GetAxis("RightThumbX") * maxTurnSpeed;
        var strafe = Input.GetAxis("LeftThumbX") * maxStrafeSpeed;
        var look = Input.GetAxis("RightThumbY") * maxTurnSpeed;


        //Move transform via translate across the object's z axis
        transform.Translate(0, 0, walk * Time.deltaTime);

        //Rotate around the object's y axis
        transform.Rotate(0, turn * Time.deltaTime, 0);

        //Strafe
        transform.Translate(strafe * Time.deltaTime, 0, 0);

        //Look around with the camera
        cam.transform.Rotate(look * Time.deltaTime,0, 0);

        Debug.Log(walk);


        if (Input.GetButtonDown("AButton"))
        {
            Jump();
        }

    }

    void Jump()
    {
        body.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Force);
    }
}
