using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{


    public float startSpeed;
    public float missileSpeed;
    Rigidbody2D body;
    Animator anim;

    private GameObject player;
    private GameObject target;
    public GameObject explosion;
    public int hitPoints = 2;
    public int fuel = 10;

    private bool onMyWay;
    private float nextActionTime;
    private CommonProc commonProc;


    void Start()
    {
        GameObject lm = GameObject.Find("LevelManager");
        commonProc = lm.GetComponent<CommonProc>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            target = player;
        if (target == null) Destroy(gameObject);
        anim = this.gameObject.GetComponent<Animator>();
        body = this.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = transform.right * startSpeed;

        StartCoroutine("Missile");
        onMyWay = false;
        nextActionTime = Time.time;
        fuel = 10;
    }

    IEnumerator Missile()
    {
        yield return new WaitForSeconds(2);
        body.velocity = transform.right * missileSpeed;
        yield return new WaitForSeconds(0.5f);
        onMyWay = true;
    }

    void Update()
    {
        if (target == null && fuel > 0) {
            target = player;
        }
        if (onMyWay && target != null) {
            commonProc.trackToObject(body, target, 7f);
        }
        if (Time.time > nextActionTime) {
            nextActionTime += 1;
            fuel -= 1;
            //Missile, no more fuel
            if (fuel < 0) {
                target = null;
                body.angularVelocity = transform.right.x * 30f;
                body.gravityScale = 0.5f;
                Destroy(gameObject, 4);
                //anim.SetTrigger("StopTrigger"); not working
            }
        }

        if (Assistant.lookForChaff) {
            if (target == player || target == null) {
                GameObject[] chaff = GameObject.FindGameObjectsWithTag("Chaff");
                foreach (GameObject go in chaff) {
                    target = go;
                    break;
                }
            }
        }

        if (commonProc.cylinder != null && (target == player || target == null)) {
            Cylinder script = commonProc.cylinder.GetComponent<Cylinder>();
            if (script.attached) {
                target = commonProc.cylinder;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bolt") {
            hitPoints--;
            GameObject clone = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(clone, 1);
            explosion.transform.localScale = explosion.transform.localScale * 0.1f;
        } else {
            hitPoints = 0;
        }
        if (hitPoints <= 0) {
            GameObject clone = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(clone, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Chaff") {
            GameObject clone = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(clone, 1);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
