using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonProc : MonoBehaviour {

    private GameObject mainText;
    private MonoBehaviour textScript;

    private void Start()
    {
        GameObject mainText = GameObject.Find("Canvas/Panel/Text");
        textScript = mainText.GetComponent<MonoBehaviour>();
    }

    public bool trackToObject(Rigidbody2D body, GameObject target, float speed, bool greenNotRed)
    {
        if (body == null || target == null) return false;
        float rotatingSpeed = 400;
        Vector3 vector = Vector3.zero;
        vector = body.transform.right;
        if (greenNotRed) vector = body.transform.up;

        Vector2 pointToTarget = (Vector2)body.transform.position - (Vector2)target.transform.position;
        pointToTarget.Normalize();
        float value = Vector3.Cross(pointToTarget, vector).z;
        body.angularVelocity = rotatingSpeed * value;
        body.velocity = vector * speed;

        return (Vector2.Distance(body.transform.position, target.transform.position) < 0.3);
    }


    public GameObject findClosestGameObject(GameObject target, string tagName)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagName);
        GameObject closest = null;
        float distance = 999;
        foreach (GameObject obj in gameObjects) {
            float dist = Vector2.Distance(obj.transform.position, target.transform.position);
            if (dist < distance) {
                distance = dist;
                closest = obj;
            }
        }
        return closest;
    }

    public void updateText(string text)
    {
        if (mainText == null) return;

    }

}
