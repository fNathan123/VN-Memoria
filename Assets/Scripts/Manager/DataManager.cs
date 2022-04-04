using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public bool autoForwardState;
    public bool skipState;

    private float textSpeed=0.01f;
    private float autoSpeed;

    private float skipTextSpeed = 0.001f;
    private float skipAutoSpeed = 0.1f;

    private string menuState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        GeneratePlayerPrefKeys();
        //Debug.Log(Application.persistentDataPath);
        //C:/Users/feb/AppData/LocalLow/DefaultCompany/NanoReno_VN
    }

    private void Start()
    {
        
    }

    public float GetAutoSpeed()
    {
        if (skipState)
        {
            return skipAutoSpeed;
        }
        else
        {
            return autoSpeed;
        }
    }

    public float GetTextSpeed()
    {
        if (skipState)
        {
            return skipTextSpeed;
        }
        else
        {
            return textSpeed;
        }
    }

    public void SetMenuState(string menu)
    {
        menuState = menu;
    }

    public string GetMenuState()
    {
        return menuState;
    }

    void GeneratePlayerPrefKeys()
    {
        if (!PlayerPrefs.HasKey("Music_Volume"))
        {
            PlayerPrefs.SetFloat("Music_Volume", 1f);
        }

        if (!PlayerPrefs.HasKey("SFX_Volume"))
        {
            PlayerPrefs.SetFloat("SFX_Volume", 1f);
        }

        if (!PlayerPrefs.HasKey("Auto_Time"))
        {
            PlayerPrefs.SetFloat("Auto_Time", 1f);
        }
    }

    public float GetFloatSetting(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public void SetFloatSetting(string key,float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void SetAutoTime(float multiplier)
    {
        float minTime = 1f;
        float maxTime = 3f;
        float value = minTime+ ((maxTime - minTime) * multiplier);
        autoSpeed = value;
    }

    public void SaveProgress(string slotName,Story story)
    {
        SaveData data = new SaveData();
        data.storyID = story.storyID;
        data.chapter = story.storyID.Substring(2);
        data.chapterTitle = story.storyTitle;
        data.saveDate = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss");
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath +"/"+ slotName + ".json", json);
    }

    public SaveData LoadProgress(string slotName)
    {
        string path = Application.persistentDataPath + "/" + slotName +".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        return null;
    }
}

[System.Serializable]
public class SaveData
{
    public string storyID;
    public string chapter;
    public string chapterTitle;
    public string saveDate;
    public Sprite image;
}

