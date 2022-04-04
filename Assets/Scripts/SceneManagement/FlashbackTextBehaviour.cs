using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashbackTextBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text self = GetComponent<Text>();
        self.text = StorylineManager.Instance.GetStoryFlashbackTimeline();
    }
}
