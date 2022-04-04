using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance 
    {
        get;
        private set;
    }
    [SerializeField]
    GameObject saveLoadScreen;
    [SerializeField]
    GameObject settingsScreen;
    [SerializeField]
    Button saveButton;
    [SerializeField]
    Button mainMenuButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DisableAllScreen();
        string previousScene = CheckPreviousScene();
        if(previousScene == "Title")
        {
            saveButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
        }
        ChangeMenu(DataManager.Instance.GetMenuState());
    }

    private void Update()
    {
        
    }

    void ChangeMenu(string menu)
    {
        switch (menu)
        {
            case "Save":
                DisableAllScreen();
                saveLoadScreen.SetActive(true);
                saveLoadScreen.transform.Find("ModeText").GetComponent<Text>().text = "Save";
                SaveLoadManager.Instance.ChangeMode("Save");
                break;
            case "Load":
                DisableAllScreen();
                saveLoadScreen.SetActive(true);
                saveLoadScreen.transform.Find("ModeText").GetComponent<Text>().text = "Load";
                SaveLoadManager.Instance.ChangeMode("Load");
                break;
            case "Setting":
                DisableAllScreen();
                settingsScreen.SetActive(true);
                break;
        }
    }

    void DisableAllScreen()
    {
        saveLoadScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void ReturnOnClick()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync("Settings");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SaveOnClick()
    {
        ChangeMenu("Save");
    }

    public void LoadOnClick()
    {
        ChangeMenu("Load");
    }

    public void QuitOnClick()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void MainMenuOnClick()
    {
        StorylineManager.Instance.ReverProgress();
        StorylineManager.Instance.RevertStories();
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SettingOnClick()
    {
        ChangeMenu("Setting");
    }

    string CheckPreviousScene()
    {
        return SceneManager.GetSceneAt(0).name;
    }
}
