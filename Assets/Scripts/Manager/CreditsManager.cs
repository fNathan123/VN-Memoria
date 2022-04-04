using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField]
    GameObject page1;
    [SerializeField]
    GameObject page2;

    public void NextPageOnClick()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void PreviousPageOnClick()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void ReturnPageOnClick()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync("Credits");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
