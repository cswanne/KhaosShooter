using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public GameObject shot;
    public GameObject explosion;
    public GameObject shield;
    public GameObject chaff;
    public GameObject cylinder;
    public Transform shotSpawn;
    public Boundary boundary;
    [HideInInspector]
    public bool shieldUp = false;

    private CommonProc commonProc;
    private Globals globals;
    private float noFuel = 0;
    private float x = 0f, y = 0f;
    private float moveSpeedX = 30f;
    private float moveSpeedY = 20f;
    private float fireRate = 0.25f;
    private float nextFire = 0.5f;
    private float nextKey = 0f;
    private float yRotation = 0;
    private float forceRate = 0.1f;
    private float nextForce = 0.0f;
    private bool pressingKey = false;
    private int hitPoints = 100;
    private bool giftOnBoard = false;

    private float shieldDuration = 1.2f;
    private float shieldFadeTime;
    private int damageInflicting = 0;
    private bool damageZoomOut = true;
    Vector2 m_CurrentVelocity;
    SpriteRenderer shield1;
    TextMesh damageText;

    Rigidbody2D body;
    //AudioSource fireAudio1;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        GameObject lm = GameObject.Find("LevelManager");
        commonProc = lm.GetComponent<CommonProc>();
        globals = lm.GetComponent<Globals>();
        body = this.gameObject.GetComponent<Rigidbody2D>();
        //fireAudio1 = this.gameObject.GetComponent<AudioSource>();
        nextKey = Time.time;
        shield1 = transform.Find("Shield1").GetComponent<SpriteRenderer>();
        shield1.color = new Color(255f, 255f, 255f, 0);
        damageText = transform.Find("DamageIndicator").GetComponent<TextMesh>();
        nextForce = Time.time + forceRate;
    }

    private void Update()
    {
        if ((Input.GetButton("AButton") || Input.GetKey(KeyCode.Space)) && Assistant.currentAmmo > 0 && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Assistant.currentAmmo--;
            //fireAudio1.Play();
        };

        noFuel = 1f;

        if (Assistant.currentFuel == 0) noFuel = 0.25f;
        if (Time.time > nextKey) {
            keyboardControls(Input.GetKey(KeyCode.Z), noFuel);
            nextKey += 0.05f;
        }

        //Fade part of the shield in and out
        if (shieldUp && shield1.enabled) {
            float t = (Time.time - shieldFadeTime) / (shieldDuration / 2);
            shield1.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0, 1, t));
        } else if (!shieldUp && shield1.enabled) {
            float t = (Time.time - shieldFadeTime) / (shieldDuration / 2);
            float fade = Mathf.SmoothStep(1, 0, t);
            shield1.color = new Color(1f, 1f, 1f, fade);
            if (fade == 0)
                shield1.enabled = false;
        }
        //if (Time.time > nextForce) {
        //    body.AddForce(new Vector2(x, y), ForceMode2D.Force);
        //    nextForce = Time.time + forceRate;
        //}

        Vector2 newPos = Vector2.SmoothDamp(transform.position, (Vector2)transform.position + new Vector2(x, y), ref m_CurrentVelocity, 1, Mathf.Infinity, Time.deltaTime);
        transform.position = newPos;

    }

    void FixedUpdate ()
    {

        if (this.transform.position.y > 8) {
            transform.position = new Vector3(transform.position.x, 7.8f, transform.position.z);
        } else if (this.transform.position.y < -5) {
            transform.position = new Vector3(transform.position.x, -4.8f, transform.position.z);
        };


    }


    public void keyboardControls(bool boost, float noFuel)
    {
        if (pressingKey) return;


        float speedX = (boost) ? moveSpeedX * 2 * noFuel : moveSpeedX * noFuel;
        float speedY = (boost) ? moveSpeedY * 2 * noFuel : moveSpeedY * noFuel;
        x = 0f;
        y = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) {
            pressingKey = true;
            x = speedX;
            yRotation = 0;
        };
        if (Input.GetKey(KeyCode.LeftArrow)) {
            pressingKey = true;
            x = -speedX;
            yRotation = 180;
        };
        if (Input.GetKey(KeyCode.UpArrow)) {
            pressingKey = true;
            y = speedY;
        };
        if (Input.GetKey(KeyCode.DownArrow)) {
            pressingKey = true;
            y = -speedY;
        };
        if (x + y != 0) {
            pressingKey = true;
            Assistant.updateFuel((boost) ? -2 : -1);
        };
        if (Input.GetKey(KeyCode.C)) {
            pressingKey = true;
            StartCoroutine(CPressed());
        }
        if (Input.GetKey(KeyCode.S)) {
            RedAlertShieldsUp();
        };
        if (Input.GetKey(KeyCode.D)) {
            if (globals.cylinder == null) { 
                Instantiate(cylinder, transform.position, Quaternion.Euler(0, 0, 90));
            }
        };

        Quaternion rot = Quaternion.Euler(0, yRotation, 0);
        if (this.transform.rotation != rot)
            this.transform.rotation = rot;
        damageText.transform.rotation = Quaternion.identity;
        if (pressingKey) StartCoroutine(KeyPressed());

    }

    private void RedAlertShieldsUp()
    {
        if (shieldUp) return;
        SpriteRenderer sr1 = transform.Find("Shield1").GetComponent<SpriteRenderer>();
        SpriteRenderer sr2 = transform.Find("Shield2").GetComponent<SpriteRenderer>();
        shieldUp = true;
        shieldFadeTime = Time.time;
        sr1.enabled = shieldUp;
        sr2.enabled = shieldUp;
        StartCoroutine(ShieldDown(sr1, sr2));
    }

    IEnumerator ShieldDown(SpriteRenderer sr1, SpriteRenderer sr2)
    {
        yield return new WaitForSeconds(shieldDuration);
        shieldUp = false;
        shieldFadeTime = Time.time;
        //sr1 will fade out then disable
        sr2.enabled = shieldUp;
    } 

    IEnumerator KeyPressed()
    {
        yield return new WaitForSeconds(0.05f);
        pressingKey = false;
    } 

    IEnumerator CPressed()
    {
        //Debug.Log("c");

        yield return new WaitForSeconds(0.1f);

        Vector3 pos = transform.position;
        pos.x -= transform.right.x * 3f;
        GameObject clone = Instantiate(chaff, pos, transform.rotation, null);
        Destroy(clone, 2);
        Assistant.lookForChaff = true;
        StartCoroutine(StopLooking());
        yield return new WaitForSeconds(0.5f);
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
            hurtMe(script.damage, collision);
        } else if (collision.collider.transform.tag == "Missile") {
            MissileMove script = collision.gameObject.GetComponent<MissileMove>();
            hurtMe(script.damage, collision);
        } else if (collision.collider.transform.tag == "Boulder") {
            hurtMe(2, collision);
        }
    }

    private void hurtMe(int damage, Collision2D collision)
    {
        int d = damage / (shieldUp ? 2 : 1);
        damageInflicting += d;
        damageText.text = damageInflicting.ToString();
        StartCoroutine(UpdateDamageTextSize());
        hitPoints -= d;
        commonProc.updateText(hitPoints.ToString()); //not working
        Destroy(collision.gameObject);
        if (hitPoints <= 0) {
            GameObject ex = Instantiate(explosion, collision.collider.transform.position, collision.collider.transform.rotation);
            Destroy(ex, 1);
            Destroy(gameObject);
        }
    }

    IEnumerator UpdateDamageTextSize()
    {
        yield return null;
        if (damageZoomOut) {
            if (damageText.fontSize < 200) {
                damageText.fontSize += 8;
                StartCoroutine(UpdateDamageTextSize());
            } else {
                damageZoomOut = false;
                StartCoroutine(UpdateDamageTextSize());
            }
        } else if (damageText.fontSize > 56) {
            damageText.fontSize -= 12;
            StartCoroutine(UpdateDamageTextSize());
        } else { 
            damageText.text = "";
            damageInflicting = 0;
            damageZoomOut = true;
            damageText.fontSize = 36;
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

