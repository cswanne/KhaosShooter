using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour //not used
{

    public static LevelManager Instance { set; get; }

    public float transitionWait = 5.0f;
    public bool transition = false;
    public int killsPerLevel = 50;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        LevelControl(SaveManager.Instance.state.currentLevelIndex);
        
    }

    public void LevelControl(int currentLevelIndex)
    {
        
        currentLevelIndex = SaveManager.Instance.state.currentLevelIndex;

        if (SaveManager.Instance.state.score == killsPerLevel)
            {
            
            StartCoroutine (LevelTransition(currentLevelIndex));
                
            }
        if (SaveManager.Instance.state.score == killsPerLevel*2)
            {
                StartCoroutine(LevelTransition(currentLevelIndex));
                
            }
        
    }

    public void Update()
    {
        if(transition == true)
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
            transition = false; 
        }
    }

    private CanvasGroup fader;
    
    IEnumerator LevelTransition(int currentLevelIndex)
    {
        //stop the asteroids spawning
        FindObjectOfType<GameController>().spawn = false;


        //fade the transition proocess in

        fader = FindObjectOfType<CanvasGroup>();
        FindObjectOfType<Text>().text = ("Awesome! You've just finished level: " + (SaveManager.Instance.state.currentLevelIndex - 1) + " and your current score is: " + SaveManager.Instance.state.score).ToString();
        transition = true;
             
        //end of transition process

        yield return new WaitForSeconds(transitionWait);
        Debug.Log("IENUMERATOR next level started");
        UpdateLevelIndex(currentLevelIndex);
        yield return null;
        
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = fader.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            fader.alpha = Mathf.Lerp(alpha, aValue, t);
            yield return null;
        }


            
    }

    public void UpdateLevelIndex(int currentLevelIndex)
    {
        currentLevelIndex++;
        SaveManager.Instance.CompleteLevel(currentLevelIndex);
        
        SceneManager.LoadScene(SaveManager.Instance.state.currentLevelIndex);
    }

}
