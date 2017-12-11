using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private float startTime = 0f;
    private float stayAliveCounter = 2;

    private void Start()
    {
        startTime = Time.time;
    }


    void Update()
    {
        if (Time.time > startTime + stayAliveCounter) {
            Destroy(this.gameObject);
            startTime = Time.time;
        }
    }


}
