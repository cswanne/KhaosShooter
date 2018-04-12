using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        ResetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

    }

    //Load the previous save state from the player prefs
    //Currently there is no ability or need to load previous save which is why the function ResetSave is always called
    //But there could be the potential to save and continue in the future so this is here in preparation

    public void Load()
    {
        // Check if a save already exists
        if (PlayerPrefs.HasKey("save"))
        {
            state = Assistant.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }

        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one ");
        }
    }

    //Save the saveState to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Assistant.Serialize<SaveState>(state));

    }

    //Complete level
    public void CompleteLevel(int index)
    {
        
            state.currentLevelIndex ++;
            Save();
        
    }

    //Function that resets the whole save file
    public void ResetSave()
    {
        
        PlayerPrefs.DeleteKey("save");
    }
}
