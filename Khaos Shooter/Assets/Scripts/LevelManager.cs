using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { set; get; }

    public float transitionWait = 1.0f;

     
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        LevelControl(SaveManager.Instance.state.currentLevelIndex);
        
    }

    public void LevelControl(int currentLevelIndex)
    {
        
        currentLevelIndex = SaveManager.Instance.state.currentLevelIndex;

        if (SaveManager.Instance.state.score == 100)
            {
                StartCoroutine (LevelTransition(currentLevelIndex));
                
            }
        if (SaveManager.Instance.state.score == 200)
            {
                StartCoroutine(LevelTransition(currentLevelIndex));
                //currentLevelIndex++;
                //SaveManager.Instance.CompleteLevel(currentLevelIndex);
                //SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);

            }
        
    }

    private CanvasGroup fade;

    IEnumerator LevelTransition(int currentLevelIndex)
    {
        //stop the asteroids spawning
        FindObjectOfType<GameController>().spawn = false;
        FindObjectOfType<GameController>().StopSpawnWaves();

        //destroy any current asteroids on screen      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        fade = FindObjectOfType<CanvasGroup>();
        fade.alpha = 1 + Time.deltaTime;
        yield return new WaitForSeconds(transitionWait);

        Debug.Log("IENUMERATOR next level started");
        UpdateLevelIndex(currentLevelIndex);
        
    }

    public void UpdateLevelIndex(int currentLevelIndex)
    {
        currentLevelIndex++;
        SaveManager.Instance.CompleteLevel(currentLevelIndex);
        SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);
    }

}
