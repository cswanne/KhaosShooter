using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dummy : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButton("Start"))
        {
            SceneManager.LoadScene("02_Level1");
        }
		
	}
}
