using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class RetentionManager : MonoBehaviour
{
    public static RetentionManager Instance;
    public int playerScore;
    public TMP_InputField playerName;
    public string savedPlayerName;
    public int score; 
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    [System.Serializable]
    class SaveData
    {
        public string savedPlayerName;
        public int playerScore;
    }

    public void SaveNamenScore()
    {
        SaveData myData = new SaveData();
        savedPlayerName = playerName.text;
        myData.savedPlayerName = savedPlayerName;
        myData.playerScore = playerScore;
        string json = JsonUtility.ToJson(myData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNamenScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData myData = JsonUtility.FromJson<SaveData>(json);
            savedPlayerName = myData.savedPlayerName;
            playerScore = myData.playerScore;
        }
    }

}
