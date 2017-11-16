using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { set; get; }

    public float transitionWait = 3;

     
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
                currentLevelIndex++;
                SaveManager.Instance.CompleteLevel(currentLevelIndex);
                SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);

            }
        
    }

    
    IEnumerator LevelTransition(int currentLevelIndex)
    {

        FindObjectOfType<GameController>().spawn = false;
        FindObjectOfType<GameController>().StopSpawnWaves();
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        //Time.timeScale = 0.2f;
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
