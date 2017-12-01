using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMove : MonoBehaviour
{

    public float moveSpeed =  1f;
    Rigidbody body;

    public void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        float x = 0f, y = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) x = moveSpeed;
        if (Input.GetKey(KeyCode.LeftArrow)) x = -moveSpeed;
        if (Input.GetKey(KeyCode.UpArrow)) y = moveSpeed;
        if (Input.GetKey(KeyCode.DownArrow)) y = -moveSpeed;
        body.AddForce(moveSpeed, 0f, 0f, ForceMode.Force);
    }

}
