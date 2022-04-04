using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTimeline());
    }

    IEnumerator WaitTimeline()
    {
        yield return new WaitForSeconds(4f);
        StorylineManager.Instance.RevertStories();
        StorylineManager.Instance.ReverProgress();
        Loader.Instance.FadeOut("Title");
    }
}
