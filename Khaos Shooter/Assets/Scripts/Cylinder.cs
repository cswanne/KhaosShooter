using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

    private CommonProc commonProc;
    private Rigidbody2D body;
    private GameObject asteroid = null;
    private bool hadTarget = false;
    [HideInInspector]
    public bool attached = false;

    void Start () {
        GameObject lm = GameObject.Find("LevelManager");
        commonProc = lm.GetComponent<CommonProc>();
        body = this.gameObject.GetComponent<Rigidbody2D>();
        commonProc.cylinder = this.gameObject;
    }

    void Update () {
        if (asteroid == null && !hadTarget) {
            asteroid = commonProc.findClosestGameObject(this.gameObject, "Enemy");
            hadTarget = true;
        }
        if (asteroid != null && !attached) {
            attached = commonProc.trackToObject(body, asteroid, 10f);
        } else if (asteroid != null && attached) {
            Rigidbody2D aBody = asteroid.GetComponent<Rigidbody2D>();
            body.angularVelocity = aBody.angularVelocity;
            body.velocity = aBody.velocity;
        }
        else if (hadTarget) {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        commonProc.cylinder = null;
    }
}
