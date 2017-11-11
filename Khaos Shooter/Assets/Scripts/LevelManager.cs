using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { set; get; }

    public float transitionWait = 10;

    private CanvasGroup transition;

    public GameController gameController;
   
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
                //UpdateLevelIndex(currentLevelIndex);
                //currentLevelIndex++;
                //SaveManager.Instance.CompleteLevel(currentLevelIndex);
                //SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);

            }
        if (SaveManager.Instance.state.score == 200)
            {
                currentLevelIndex++;
                SaveManager.Instance.CompleteLevel(currentLevelIndex);
                SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);

            }
        
    }

    public void UpdateLevelIndex(int currentLevelIndex)
    {
        currentLevelIndex++;
        SaveManager.Instance.CompleteLevel(currentLevelIndex);
        SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);
    }

    IEnumerator LevelTransition(int currentLevelIndex)
    {
        gameController.StopSpawnWaves();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        yield return new WaitForSeconds(transitionWait); 
        Debug.Log("next level started");
        //transition.alpha = 1;
        UpdateLevelIndex(currentLevelIndex);

    }
        
}
