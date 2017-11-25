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
    public GameObject explosion;
    public Transform shotSpawn;
    private float nextFire = 0.5f;
    public float fireRate = 0.25f;
    public Boundary boundary;
    private GameController gameController;

    public bool keyboardControl = false;
    
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

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            keyboardControl = !keyboardControl;
            Debug.Log("Keyboard controls enabled = " + keyboardControl);
        }

        if ((Input.GetButton("AButton") || Input.GetKey(KeyCode.Space)) && Assistant.currentAmmo > 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Assistant.currentAmmo--;
            //fireAudio1.Play();
        }
        
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("LeftThumbX"); 
        float moveVertical = Input.GetAxis("LeftThumbY");
        float boost = Input.GetAxis("RightTrigger");
        float noFuel = 1f;

        if (Assistant.currentFuel == 0) noFuel = 0.25f;
        if (keyboardControl) {
            keyboardControls(((keyboardControl && Input.GetKey(KeyCode.Z)) ? kbMoveSpeed * 2 * noFuel : kbMoveSpeed * noFuel));
        };

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        //This is a boost and brake function
        if (boost > 0) movement *= boostMovementSpeed;
        else if (boost < 0) movement *= brakeMovementSpeed;
        else movement *= moveSpeed;
        body.velocity = movement * noFuel;

        //Keeping the player within the contraints of the game panel
        body.position = new Vector3(Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax),0.0f);

        //Add some bank to the ship as it moves left and right
        //Dont want to tilt on x or y. Want the tilt on z to be relative to the speed we are moving at
        body.rotation = Quaternion.Euler(0.0f, 90.0f, body.velocity.y * tilt);

        if (movement != Vector3.zero) {
            Assistant.updateFuel(-1);
        }

    }


    public void keyboardControls(float speed)
    {
        float x = 0f, y = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) x = speed;
        if (Input.GetKey(KeyCode.LeftArrow)) x = -speed;
        if (Input.GetKey(KeyCode.UpArrow)) y = speed;
        if (Input.GetKey(KeyCode.DownArrow)) y = -speed;
        body.AddForce(x, y, 0f, ForceMode.Force);
        if (x + y != 0) {
            Assistant.updateFuel(-1);
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy") {
            Instantiate(explosion, collision.collider.transform.position, collision.collider.transform.rotation);
            Destroy(gameObject);
            gameController.GameOver();
        }
    }
}
