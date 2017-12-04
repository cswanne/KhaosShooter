using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private float startTime = 0f;
    private float stayAliveCounter = 2;
    private Light light1;
    private Light light2;

    private void Start()
    {
        startTime = Time.time;
        Light[] lights = transform.GetComponentsInChildren<Light>();
        light1 = lights[0];
        light2 = lights[1];
    }


    void Update()
    {
        if (Time.time > startTime + stayAliveCounter) {
            Destroy(this.gameObject);
        }

        light1.enabled = !(Time.deltaTime * 10 % 2 == 0);
        light2.enabled = !(Time.deltaTime * 10 % 3 == 0);
    }



}
