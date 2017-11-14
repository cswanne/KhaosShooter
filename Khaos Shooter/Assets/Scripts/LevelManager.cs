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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        LevelControl(SaveManager.Instance.state.currentLevelIndex);
        
    }

    private void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void LevelControl(int currentLevelIndex)
    {
        
        currentLevelIndex = SaveManager.Instance.state.currentLevelIndex;

        if (SaveManager.Instance.state.score == 100)
            {
                LevelTransition();
                UpdateLevelIndex(currentLevelIndex);
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

    public void LevelTransition()
    {
        
        //transition.alpha = 1;

    }
        
}
