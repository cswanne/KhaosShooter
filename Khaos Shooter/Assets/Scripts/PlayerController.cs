﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    private float noFuel = 0;
    private float x = 0f, y = 0f;

    public float kbMoveSpeed = 300.0f;
    public GameObject shot;
    public GameObject explosion;
    public GameObject shield;
    public GameObject chaff;
    public Transform shotSpawn;
    public float fireRate = 0.25f;
    public Boundary boundary;

    private float nextFire = 0.5f;
    private GameController gameController;
    private float yRotation = 90;
    private bool applyingForce = false;
    private int hitPoints = 100;
    private bool giftOnBoard = false;


    Rigidbody2D body;
    //AudioSource fireAudio1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        //Find this player's rigidbody component
        body = this.gameObject.GetComponent<Rigidbody2D>();
        //fireAudio1 = this.gameObject.GetComponent<AudioSource>();

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    private void Update()
    {
        if ((Input.GetButton("AButton") || Input.GetKey(KeyCode.Space)) && Assistant.currentAmmo > 0 && Time.time > nextFire)  {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Assistant.currentAmmo--;
            //fireAudio1.Play();
        };

        noFuel = 1f;

        if (Assistant.currentFuel == 0) noFuel = 0.25f;
        keyboardControls(Input.GetKey(KeyCode.Z), noFuel);
    }

    void FixedUpdate ()
    {
        body.AddForce(new Vector2(x, y), ForceMode2D.Impulse);


        if (this.transform.position.y > 8) {
            transform.position = new Vector3(transform.position.x, 7.8f, transform.position.z);
        } else if (this.transform.position.y < -5) {
            transform.position = new Vector3(transform.position.x, -4.8f, transform.position.z);
        };

    }


    public void keyboardControls(bool boost, float noFuel)
    {
        if (applyingForce) return;

        float speed = (boost) ? kbMoveSpeed * 2 * noFuel : kbMoveSpeed * noFuel;
        x = 0f;
        y = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) {
            x = speed;
            yRotation = 0;
            this.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        };
        if (Input.GetKey(KeyCode.LeftArrow)) {
            x = -speed;
            yRotation = 180;
            this.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        };
        if (Input.GetKey(KeyCode.UpArrow)) {
            y = speed;
        };
        if (Input.GetKey(KeyCode.DownArrow)) {
            y = -speed;
        };
        if (x + y != 0) {
            Assistant.updateFuel((boost) ? -2 : -1);
        };
        if (Input.GetKey(KeyCode.C)) {
            Vector3 pos = transform.position;
            pos.x -= transform.right.x * 3f;
            GameObject clone = Instantiate(chaff, pos, transform.rotation, null);
            Destroy(clone, 2);
            Assistant.lookForChaff = true;
            StartCoroutine(StopLooking());
        }
    }

    IEnumerator StopLooking()
    {
        yield return new WaitForSeconds(2);
        Assistant.lookForChaff = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.tag == "Enemy") {
            AsteroidCollide script = collision.gameObject.GetComponent<AsteroidCollide>();
            hitPoints -= script.damage;
            Destroy(collision.gameObject);
            if (hitPoints <= 0) {
                Instantiate(explosion, collision.collider.transform.position, collision.collider.transform.rotation);
                Destroy(gameObject);
                //gameController.GameOver();
            }
        } else if (collision.collider.transform.tag == "Boulder") {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 86.33f));
            GameObject clone = Instantiate(shield, transform.position, rot, transform);
            clone.transform.parent = transform;
        }
    }

    private void giftImage(bool show)
    {
        GameObject giftImage = GameObject.Find("Canvas/Panel/GiftImage");
        if (giftImage != null) {
            MonoBehaviour gi = giftImage.GetComponent<MonoBehaviour>();
            gi.enabled = show;
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ammo") {
            Assistant.maxAmmo();
            Destroy(collision.gameObject);
        } else if (collision.transform.tag == "Fuel") {
            Assistant.addFuel();
            Destroy(collision.gameObject);
        } else if (collision.transform.name == "Gift2D" && !giftOnBoard) {
            giftOnBoard = true;
            giftImage(giftOnBoard);
            Destroy(collision.gameObject);
        } else if (collision.transform.name == "Bucket" && giftOnBoard) {
            giftOnBoard = false;
            giftImage(giftOnBoard);
        }
    }
}
