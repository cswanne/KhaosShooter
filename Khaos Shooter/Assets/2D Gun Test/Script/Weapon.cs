using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0f;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    public Transform firePoint;
    public Transform endPoint;
    public float fireDistance = 15;

    //public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public GameObject BulletPrefab;
    //public Transform HitPrefab;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint found");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // To test the shoot function and raycast use the below function call
        //Shoot();



        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // create a new vector 2 as the target / end point of the ray
        Vector2 endPosition = new Vector2((endPoint.position).x, (endPoint.position).y);

        // create a new vector 2 as the starting point of the ray
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        // get some feedback to show that this function SHOOT has been called correctly
        Debug.Log("Fired");
        //Set up the Raycast to fire from the start poistion in the direction of the end position for a defined distance, only for a certain layer

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, endPosition - firePointPosition, fireDistance, whatToHit);

        // draws a green line when shoot is called
        Debug.DrawLine(firePointPosition, endPosition, Color.green);

        // monitor if the ray hits another collider and if true draw a red line and output what we hit
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, endPosition, Color.red);

            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                player.DamagePlayer(Damage);
                Debug.Log("Object hit " + hit.collider.name + " and did " + Damage + " Damage");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (endPosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }

            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;

            }


            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        /*
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);

        }

        Destroy(trail.gameObject, 0.05f);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 1f);
        }
        */
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.06f);

        Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        //bullet.parent = firePoint;
        //Destroy(bullet.gameObject, 3.0f);

    }


}

