using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public float volumeLevel;
    public int currentLevel;
    public int highestLevel;

    // Encryption Key (EXPOSED)
    private static readonly string keyWord = "Chickened";

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

        string path = Application.persistentDataPath + "/savefile.json";

        string json = JsonUtility.ToJson(newData);
        string encryptedJson = EncryptDecrypt(json);
        File.WriteAllText(path, encryptedJson);
    }

    // Encryption Algorithm (EXPOSED)
    public string EncryptDecrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }
        return result;
    }
}
