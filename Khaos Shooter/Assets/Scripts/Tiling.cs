using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offsetX = 2;
    private bool hasRightBuddy = false;
    private bool hasLeftBuddy = false;
    private float spriteWidth = 0;
    private Camera cam;
    private Transform aTransform;

    void Awake()
    {
        cam = Camera.main;
        aTransform = this.transform;
    }

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * aTransform.lossyScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLeftBuddy || !hasRightBuddy) {
            float camHorzExtend = cam.orthographicSize * Screen.width / Screen.height;
            float edgeVisiblePosRight = (aTransform.position.x + (spriteWidth / 2)) - camHorzExtend;
            float edgeVisiblePosLeft = (aTransform.position.x - (spriteWidth / 2)) + camHorzExtend;

            if (cam.transform.position.x >= edgeVisiblePosRight - offsetX && !hasRightBuddy) {
                makeNewBuddy(1);
                hasRightBuddy = true;

            } else if (cam.transform.position.x <= edgeVisiblePosLeft + offsetX && !hasLeftBuddy) {
                makeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }

    }

    void makeNewBuddy(int rightOrLeft)
    {
        double adjust = 0;
        if (rightOrLeft == 1) { adjust = 0.01; } else { adjust = -0.02; };
        float newX = (float)((aTransform.position.x + (spriteWidth * rightOrLeft)) - adjust);
        Vector3 newPosition = new Vector3(newX, aTransform.position.y, aTransform.position.z);
        Transform newBuddy = Instantiate(aTransform, newPosition, aTransform.rotation) as Transform;
        newBuddy.parent = aTransform.parent;
        //Make the two sprite duplicate look ok when put next to each other
        newBuddy.localScale = new Vector3(1, 1, 1);
        if (rightOrLeft < 0) {
            newBuddy.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            newBuddy.rotation = Quaternion.Euler(0, 0, 0);
        };
        newBuddy.parent = aTransform.parent;
        if (rightOrLeft > 0) {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        } else {
            newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }
}
