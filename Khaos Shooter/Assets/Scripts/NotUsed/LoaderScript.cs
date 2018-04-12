using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoaderScript : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minLogoTime = 4.0f;
        
    
    // Use this for initialization
	void Start ()
    {
        //By design there is only going to be one canvas group in the scene, so we can quickly and easily load it
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //Start the fadeGroup on full opaque
        fadeGroup.alpha = 1.0f;

        //Could pre-load the game at this point
        //$$

        //Find out and store how long it take for the game to load.
        //If the game load time is very quick, we will apply a min logo display time
        if (Time.time < minLogoTime)
            loadTime = minLogoTime;
        else
            loadTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //fade out the canvasgroup to present the logo screen
        if(Time.time < minLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        //Once the logo screen has been presented for the calculated time fade the canvas group back in to full opaque and load the Main Menu scene
        if(Time.time > minLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene(1);
            }
        }

	}
}
