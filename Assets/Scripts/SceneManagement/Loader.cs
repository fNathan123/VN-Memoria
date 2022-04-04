using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public enum LoaderState {READY,NOTREADY }
public class Loader : MonoBehaviour
{
    public static Loader Instance
    {
        get;
        private set;
    }

    public LoaderState state 
    {
        get;
        private set;
    }

    [SerializeField]
    Animator transition;

    GameObject curTransition;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        state = LoaderState.READY;
        DontDestroyOnLoad(gameObject);
        transition.gameObject.SetActive(false);
        transition.GetComponent<Canvas>().sortingOrder = -1;
    }

    void Start()
    {
        //Debug.Log("Loader Start");
    }

    public void FadeOut()
    {
        state = LoaderState.NOTREADY;
        transition.GetComponent<Canvas>().sortingOrder = 1;
        string id = StorylineManager.Instance.GetNextSceneID();
        transition.gameObject.SetActive(true);
        curTransition = transition.transform.Find("Background").gameObject;
        curTransition.SetActive(true);
        transition.SetBool("FadeOut",true);
        StartCoroutine(WaitTime(FadeIn,id,1f));
    }
    public void FadeOut(string sceneId)
    {
        state = LoaderState.NOTREADY;
        transition.GetComponent<Canvas>().sortingOrder = 1;
        string id = sceneId;
        transition.gameObject.SetActive(true);
        curTransition = transition.transform.Find("Background").gameObject;
        curTransition.SetActive(true);
        transition.SetBool("FadeOut", true);
        StartCoroutine(WaitTime(FadeIn, id, 1f));
    }

    public void SwipeFadeOut()
    {
        state = LoaderState.NOTREADY;
        transition.GetComponent<Canvas>().sortingOrder = 1;
        string id = StorylineManager.Instance.GetNextSceneID();
        transition.gameObject.SetActive(true);
        curTransition = transition.transform.Find("Background2").gameObject;
        curTransition.SetActive(true);
        transition.SetBool("SwipeFadeOut",true);
        StartCoroutine(WaitTime(SwipeIn, id,1f));
    }

    void FadeIn()
    {
        //Debug.Log("FadeIn");
        transition.SetBool("FadeOut", false);
        transition.SetBool("FadeIn", true);

        StartCoroutine(WaitIntro(0.6f));
    }

    void SwipeIn()
    {
        //Debug.Log("FadeIn");
        transition.SetBool("SwipeFadeOut", false);
        transition.SetBool("SwipeFadeIn", true);

        StartCoroutine(WaitIntro(0.5f));
    }

    IEnumerator WaitTime(Action onWaitFinished,string sceneName,float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onWaitFinished();
    }

    IEnumerator WaitIntro(float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        
        transition.GetComponent<Canvas>().sortingOrder = -1;
        transition.SetBool("FadeIn", false);
        transition.SetBool("SwipeFadeIn", false);
        curTransition.SetActive(false);
        transition.gameObject.SetActive(false);
        state = LoaderState.READY;
    }
}
