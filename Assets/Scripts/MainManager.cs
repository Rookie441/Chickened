using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int currentLevel;
    public float volumeLevel;
    private void Awake()
    {
        // Singleton that persists between scenes
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(currentLevel); // Level 1 start from index 1
    }

    [System.Serializable]
    public class SaveData
    {
        public int currentLevel;
        public bool isLastLevel;
    }

    public void SaveLevel(bool isLastLevel)
    {
        SaveData newData = new SaveData();
        newData.currentLevel = currentLevel;
        newData.isLastLevel = isLastLevel;

        // If previous savefile exists, set level to highest attained because user might be replaying an earlier level
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(File.ReadAllText(path));
            newData.currentLevel = newData.currentLevel > data.currentLevel ? currentLevel : data.currentLevel;
        }

        string json = JsonUtility.ToJson(newData);
        File.WriteAllText(path, json);
    }

    public void SaveCompletionist()
    {
        SaveData data = new SaveData();
        data.isLastLevel = true;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

   
}
