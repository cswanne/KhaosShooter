using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{

    public Transform fuelCanister;
    private Transform clone = null;
    private int nextCanisterSeconds = 4;

    private void NewCanister()
    {
        if (clone == null) {
            Vector3 pos = new Vector3(0, 0, 0);
            GameObject asteriod = FindClosestAsteriod(pos.y);
            if (asteriod != null && asteriod.transform.position.x > pos.x && asteriod.transform.position.x < pos.x + 100) {
                Assistant.canisterDestroyTime = Time.time;
                return;
            }
            Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
            clone = Instantiate(fuelCanister, pos, rot) as Transform;
        }
    }

    private GameObject FindClosestAsteriod(float positionY)  {
        GameObject[] asteriods = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject obj in asteriods) {
            float diff = (obj.transform.position.y - positionY);
            Vector3 compare = new Vector3(0, obj.transform.position.y, 0);
            float curDistance = compare.sqrMagnitude;
            if (diff >= -1 && diff <= 1 && curDistance < distance) { 
                closest = obj;
                distance = curDistance;
            }
        }
        return closest;    
    }

    void Update()
    {
        if (Assistant.gameOver) return;
        if (Assistant.canisterDestroyTime == 0 || Time.time > Assistant.canisterDestroyTime + nextCanisterSeconds) { 
            NewCanister();
        }
    }

}
