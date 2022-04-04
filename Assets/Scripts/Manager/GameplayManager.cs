using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance 
    {
        get;
        private set;
    }

    [SerializeField]
    GameObject rainEffect;

    [SerializeField]
    Image flashbackEffect;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Weather weather = StorylineManager.Instance.GetStoryWeather();
        if(weather == Weather.RAIN)
        {
            rainEffect.SetActive(true);
        }
        if (StorylineManager.Instance.GetStoryFlashbackStat())
        {
            flashbackEffect.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        
    }

    public void SaveButtonClick()
    {
        DataManager.Instance.SetMenuState("Save");
        StartCoroutine(LoadSettings());
    }

    public void LoadButtonClick()
    {
        DataManager.Instance.SetMenuState("Load");
        StartCoroutine(LoadSettings());
    }

    void OpenSetting()
    {
        DataManager.Instance.SetMenuState("Setting");
        StartCoroutine(LoadSettings());
    }

    IEnumerator LoadSettings()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
