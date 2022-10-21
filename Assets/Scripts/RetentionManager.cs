using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro; //for capturing name in inputfield

public class RetentionManager : MonoBehaviour
{
    public static RetentionManager Instance; //creating your retention manager instance 
    public int playerScore; //here's where you will be putting the m_points in the MainManager script later
    public TMP_InputField playerName; //this is to get the name in the inputfield
    public string savedPlayerName; //variable to store the playername.text from the inputfield in order to save it
    
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
        savedPlayerName = playerName.text; //grab the playername written in inputfield and store it into a variable 
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
