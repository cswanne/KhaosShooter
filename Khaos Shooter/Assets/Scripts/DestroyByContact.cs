﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Unable to find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other) //reference other - will be the shot or the player (or maybe even another asteroid?)
    {
        if(other.tag == "Boundary")
        {
            return; //This if statement just means that if the tag is Boundary then ignore and move on
        }

        Instantiate(explosion, transform.position, transform.rotation);
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject); //asteriod
        Destroy(this.gameObject); //bullet
    }

}
