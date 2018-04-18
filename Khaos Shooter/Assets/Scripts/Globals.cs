using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    [HideInInspector]
    public GameObject cylinder = null;
    public bool lookForChaff = false;


    public int currentFuel = 1000;
    public int currentAmmo = 50;

    public void updateFuel(int value)
    {
        currentFuel += value;
        if (currentFuel < 0) currentFuel = 0;
        else if (currentFuel > 1000) currentFuel = 1000;
    }

    public void updateAmmo(int value)
    {
        currentAmmo += value;
        if (currentAmmo < 0) currentAmmo = 0;
        else if (currentAmmo > 50) currentAmmo = 50;
    }

}
