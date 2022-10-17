using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;    

public class RetentionManager : MonoBehaviour
{
    public static RetentionManager Instance;
    [System.Serializable]
    class SaveName
    {
        public InputField inputName; 
    }

    public void SaveInput()
    {
        SaveName data = new SaveName();
        data.inputName = inputName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveName data = JsonUtility.FromJson<SaveName>(json);

            inputName = data.inputName;
        }
    }

    private InputField inputName;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        
    }

}
