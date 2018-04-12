using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

//    public GameObject hazard;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text levelText;
    public Slider fuelSlider;
    public Slider ammoSlider;

    private Text fuelSliderText;
    private Text ammoSliderText;

    //public Text gameOverText;
    //public Text restartText;
    private bool restart;

    public bool spawn; //todo: remove. at the mo level tranistion uses it

    public int currentLevelIndex;

    private void Start()
    {
       // restart = false;
       // spawn = true;
       // //gameOverText.text = "";
       // scoreText.text = ("Score: " + SaveManager.Instance.state.score.ToString());
       // levelText.text = ("Level: " + (SaveManager.Instance.state.currentLevelIndex - 1));
       // currentLevelIndex = SaveManager.Instance.state.currentLevelIndex;
       // StartCoroutine (SpawnWaves());
       // Debug.Log("Current level index = " + SaveManager.Instance.state.currentLevelIndex);
       //// fuelSliderText = fuelSlider.GetComponentInChildren<Text>();
       //// ammoSliderText = ammoSlider.GetComponentInChildren<Text>();
    }

    private void Update()
    {
//        if (restart)
//        {
//            SaveManager.Instance.ResetSave();

//            if (Input.GetButton("Start") || Input.GetKeyDown(KeyCode.S))
//            {
//                SceneManager.LoadScene(1);
//                SaveManager.Instance.Load();
//            }
//        }

        
//        //Free game objects on game over
//        if (spawn == false || Assistant.gameOver == true) {
//            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//            foreach (GameObject obj in enemies)
//                GameObject.Destroy(obj);
//            GameObject[] misc = GameObject.FindGameObjectsWithTag("MiscObjects");
//            foreach (GameObject obj in misc)
//                GameObject.Destroy(obj);
//            restart = true;
//            spawn = false;
//        }

//        //Update consumable values
//        fuelSlider.value = Mathf.Clamp01(Assistant.currentFuel / 1000f);
//        ammoSlider.value = Mathf.Clamp01(Assistant.currentAmmo / 50f);
////        fuelSliderText.text = string.Format("{0:0}%", fuelSlider.value * 100f);
//        //ammoSliderText.text = string.Format("{0:0}%", ammoSlider.value * 100f);
    }

    //IEnumerator SpawnWaves()
    //{
    //    yield return new WaitForSeconds(startWait);


    //    GameObject hazard;

    //    while (spawn == true)
    //    {
    //        for (int i = 0; i < hazardCount; i++)
    //        {
    //            if (!spawn) continue;
    //            Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
    //            Quaternion spawnRotation = Quaternion.identity;
    //            hazard = hazards[Mathf.RoundToInt(Random.Range(0, 3))];
    //            Instantiate(hazard, spawnPosition, spawnRotation);
    //            yield return new WaitForSeconds(spawnWait);
    //        }
    //        yield return new WaitForSeconds(waveWait);
    //    }
    //}

//    public void AddScore(int newScoreValue)
//    {
//        SaveManager.Instance.state.score += newScoreValue;
//        updateScore();
//    }

//    void updateScore() //just updates the score text object
//    {
////        scoreText.text = ("Score: " + SaveManager.Instance.state.score);
////        LevelManager.Instance.LevelControl(currentLevelIndex);
//    }

//    public void GameOver()
//    {
//        //gameOverText.text = "Game Over!";
//        Assistant.gameOver = true;
//    }

 }
