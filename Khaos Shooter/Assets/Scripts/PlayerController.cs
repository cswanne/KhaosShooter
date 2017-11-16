using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    
    public float moveSpeed = 10.0f;
    public float kbMoveSpeed = 300.0f;
    private float boostMovementSpeed = 20f;
    private float brakeMovementSpeed = 5f;
    public float tilt = 0.0f;
   

    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire = 0.5f;
    public float fireRate = 0.25f;
    public Boundary boundary;

    public bool keyboardControl;

    Rigidbody body;
    //AudioSource fireAudio1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  

    }

    void Start ()
    {
        //Find this player's rigidbody component
        body = this.gameObject.GetComponent<Rigidbody>();
        //fireAudio1 = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            keyboardControl = !keyboardControl;
            Debug.Log(keyboardControl);
        }

        if ((Input.GetButton("AButton") || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //fireAudio1.Play();
        }
        
    }

    void FixedUpdate ()
    {
<<<<<<< HEAD
        float moveHorizontal = Input.GetAxis("LeftThumbX") + Input.GetAxis("MouseX"); 
        float moveVertical = Input.GetAxis("LeftThumbY") + Input.GetAxis("MouseY");
        float boost = Input.GetAxis("RightTrigger") + ((Input.GetMouseButtonDown(1)) ? 1 : 0);
                           
=======

        if(keyboardControl == true)
        {
            keyboardControls();
        
        }

        float moveHorizontal = Input.GetAxis("LeftThumbX");
        float moveVertical = Input.GetAxis("LeftThumbY");
        float boost = Input.GetAxis("RightTrigger");
>>>>>>> acd5d26677d2bb1d0f6d06d844c5715e71f8033e
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        //This is a boost and brake function
        if (boost > 0)
            movement *= boostMovementSpeed;
        else if(boost < 0)
            movement *= brakeMovementSpeed;
        else
            movement *= moveSpeed;
        body.velocity = movement;

        //Keeping the player within the contraints of the game panel
        body.position = new Vector3(Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax),0.0f);

        //Add some bank to the ship as it moves left and right
        //Dont want to tilt on x or y. Want the tilt on z to be relative to the speed we are moving at
        body.rotation = Quaternion.Euler(0.0f, 90.0f, body.velocity.y * tilt);

	}


    public void keyboardControls()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(kbMoveSpeed, 0.0f, 0.0f, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(-kbMoveSpeed, 0.0f, 0.0f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(0.0f, kbMoveSpeed, 0.0f, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(0.0f,  -kbMoveSpeed, 0.0f, ForceMode.Force);
        }

    }
}
