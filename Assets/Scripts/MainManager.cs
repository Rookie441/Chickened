using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int currentLevel;
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
        SceneManager.LoadScene(currentLevel+1); // Level 1 start from index 2
    }

    [System.Serializable]
    public class SaveData
    {
        public int currentLevel;
    }

    public void SaveLevel()
    {
        SaveData data = new SaveData();
        data.currentLevel = currentLevel;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

   
}
