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
        LoadData();
    }

    // Canvas: Level Buttons On Click() function
    public void NewLevelSelected(int level)
    {
        if (MainManager.Instance != null) //may occur during editor mode if menu scene is skipped
        {
            MainManager.Instance.currentLevel = level;
            MainManager.Instance.LoadLevel();
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string encryptedJson = File.ReadAllText(path);
            try
            {
                // Decrypt Data
                MainManager.SaveData decryptedJson = JsonUtility.FromJson<MainManager.SaveData>(MainManager.Instance.EncryptDecrypt(encryptedJson));

                // Load Level Selection : Disable levels based on savefile
                for (int i = 0; i < decryptedJson.currentLevel; i++)
                    levelList[i].interactable = true;

                // Load Completionist: Show star image if game is fully completed
                starImage.enabled = decryptedJson.isLastLevel;
            }
            catch
            {
                // Decryption failed, savefile has been corrupted, delete it
                File.Delete(path);
            }
        }
    }

}
