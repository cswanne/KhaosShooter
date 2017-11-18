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
                
            }
        
    }

    
    
    IEnumerator LevelTransition(int currentLevelIndex)
    {
        //stop the asteroids spawning
        FindObjectOfType<GameController>().spawn = false;
                      
        yield return new WaitForSeconds(transitionWait);
        Debug.Log("IENUMERATOR next level started");
        UpdateLevelIndex(currentLevelIndex);
        yield return null;
        
    }

    public void UpdateLevelIndex(int currentLevelIndex)
    {
        currentLevelIndex++;
        SaveManager.Instance.CompleteLevel(currentLevelIndex);
        
        SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);
    }

}
