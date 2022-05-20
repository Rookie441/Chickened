using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
To add a new level:
- Editor -> Copy paste previous level
- Build settings -> Add open Scenes
- Menu scene -> Copy paste level text, set button position, change onClick() parameter
- Menu scene -> Add and drag level button in LevelSelector Canvas's script
*/
public class LevelSelectorUIHandler : MonoBehaviour
{
    public List<Button> levelList;
    public RawImage starImage;

    private void Start()
    {
        // disable levels based on save files
        LoadLevelSelection();
        // show star image if game is fully completed
        starImage.enabled = LoadCompletionist();

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
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(MainManager.Instance.EncryptDecrypt(json));

            for (int i=0; i<data.currentLevel; i++)
                levelList[i].interactable = true;
        }
    }

    public bool LoadCompletionist()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(MainManager.Instance.EncryptDecrypt(json));

            return data.isLastLevel;
        }
        return false;
    }
}
