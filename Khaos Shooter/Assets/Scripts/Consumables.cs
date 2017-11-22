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
            Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
            clone = Instantiate(fuelCanister, pos, rot) as Transform;
        }
    }

    void Update()
    {
        if (Assistant.canisterDestroyTime == 0 || Time.time > Assistant.canisterDestroyTime + nextCanisterSeconds) { 
            NewCanister();
        }
    }

}
