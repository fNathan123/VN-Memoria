using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    [SerializeField]
    AudioClip titleMusic;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        SetPlayerSettings();
        MusicManager.Instance.SetMusic(titleMusic);
    }
    
    public void StartButtonClick()
    {
        Loader.Instance.FadeOut();
        MusicManager.Instance.StopMusic();
    }

    public void SettingButtonClick()
    {
        DataManager.Instance.SetMenuState("Setting");
        StartCoroutine(LoadSettings());
    }

    public void LoadButtonClick()
    {
        DataManager.Instance.SetMenuState("Load");
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

    public void QuitOnClick()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void CreditsOnClick()
    {
        StartCoroutine(LoadCredits());
    }

    IEnumerator LoadCredits()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void SetPlayerSettings()
    {
        float musicVolume = DataManager.Instance.GetFloatSetting("Music_Volume");
        MusicManager.Instance.SetVolume(musicVolume);

        float sfxVolume = DataManager.Instance.GetFloatSetting("SFX_Volume");
        SFXManager.Instance.SetVolume(sfxVolume);

        float autoTime = DataManager.Instance.GetFloatSetting("Auto_Time");
        DataManager.Instance.SetAutoTime(autoTime);
    }
}
