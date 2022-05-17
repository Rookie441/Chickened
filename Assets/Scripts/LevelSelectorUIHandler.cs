using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUIHandler : MonoBehaviour
{
    public List<Button> levelList;

    private void Start()
    {
        // disable levels based on save files
        LoadLevelSelection();

    }
    public void NewLevelSelected(int level)
    {
        if (MainManager.Instance != null) //may occur during editor mode if menu scene is skipped
        {
            MainManager.Instance.currentLevel = level;
            MainManager.Instance.LoadLevel();
        }
    }

    public void LoadLevelSelection()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);

            for (int i=0; i<data.currentLevel; i++)
                levelList[i].interactable = true;
        }
    }
}
