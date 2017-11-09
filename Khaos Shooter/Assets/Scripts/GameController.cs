using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text levelText;
    
    //public Text gameOverText;
    //public Text restartText;
    private bool gameOver;
    private bool restart;

    public int currentLevelIndex;

    private void Start()
    {
        gameOver = false;
        restart = false;
        //gameOverText.text = "";
        scoreText.text = ("Score: " + SaveManager.Instance.state.score.ToString());
        levelText.text = ("Level: " + (SaveManager.Instance.state.currentLevelIndex - 1));
        currentLevelIndex = SaveManager.Instance.state.currentLevelIndex;
        StartCoroutine (SpawnWaves());
        Debug.Log("Current level index = " + SaveManager.Instance.state.currentLevelIndex);
    }

    private void Update()
    {
        if (restart)
        {
            SaveManager.Instance.ResetSave();

            if (Input.GetButton("Start"))
            {
                SceneManager.LoadScene(1);
                SaveManager.Instance.Load();
            }
        }

        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                //maybe break out into a menu?
                //restartText.text = "Press Start to try again";
                //Destroy all game objects
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                    GameObject.Destroy(enemy);
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        SaveManager.Instance.state.score += newScoreValue;
        updateScore();
    }

    void updateScore() //just updates the score text object
    {
        scoreText.text = ("Score: " + SaveManager.Instance.state.score);
        LevelManager.Instance.LevelControl(currentLevelIndex);
    }

    public void GameOver()
    {
        //gameOverText.text = "Game Over!";
        gameOver = true;
    }

  
}
