using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineBehaviour : MonoBehaviour
{
    PlayableDirector playableDirector;
    double duration;
    //double curTimer=0f;

    private void Start()
    {
        MusicManager.Instance.StopMusic();
        playableDirector = GetComponent<PlayableDirector>();
        duration = playableDirector.duration;
        StartCoroutine(WaitTimeline());
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(curTimer + " " + duration);
        //if (playableDirector.state != PlayState.Playing && curTimer >=duration)
        //{
        //    Loader.Instance.FadeOut();
        //    Debug.Log("Finished");
        //}
        //else if(playableDirector.state == PlayState.Playing)
        //{
        //    curTimer += Time.deltaTime;
        //}
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    Loader.Instance.FadeOut();
        //}
    }

    IEnumerator WaitTimeline()
    {
        yield return new WaitForSeconds((float)duration);
        Loader.Instance.FadeOut();
    }
}
