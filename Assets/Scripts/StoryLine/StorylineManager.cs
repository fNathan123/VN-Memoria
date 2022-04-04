using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    public static StorylineManager Instance
    {
        get;
        private set;
    }

    [SerializeField]
    List<Story> stories;
    List<Story> initialState;
    int currentStory=0;

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

        DontDestroyOnLoad(gameObject);
        initialState = stories.ConvertAll(
            story => new Story(
                story.GetWeather(),
                story.GetState(),
                story.GetFlashback(),
                story.GetFlashbackTimeline(),
                story.storyID,
                story.storyTitle,
                story.storyDialogue,
                story.opSceneID,
                story.edSceneID      
                ));
    }
    
    public string GetNextSceneID()
    {
        string id = stories[currentStory].GetSceneToLoad();
        if(id == "Finished")
        {
            currentStory++;
            if (currentStory >= stories.Count)
            {
                return "Fin";
            }
            id = stories[currentStory].GetSceneToLoad();
        }
        UpdateStoryState();
        return id;
    }

    void UpdateStoryState()
    {
        stories[currentStory].SetNextState();
    }

    public TextAsset GetStoryDialogue()
    {
        return stories[currentStory].storyDialogue;
    }

    public Weather GetStoryWeather()
    {
        return stories[currentStory].GetWeather();
    }

    public bool GetStoryFlashbackStat()
    {
        return stories[currentStory].GetFlashback();
    }

    public string GetStoryFlashbackTimeline()
    {
        return stories[currentStory].GetFlashbackTimeline();
    }

    public Story GetCurrentStory()
    {
        return stories[currentStory];
    }

    public void SetCurrentStory(string storyID)
    {
        for(int i = 0; i < stories.Count; i++)
        {
            if(stories[i].storyID == storyID)
            {
                //if (i > 0)
                //{
                //    currentStory = i - 1;
                //}
                //else
                //{
                //    currentStory = i;
                //}
                currentStory = i;
            }
        }
    }

    public void RevertStories()
    {
        stories = initialState.ConvertAll(
            story => new Story(
                story.GetWeather(),
                story.GetState(),
                story.GetFlashback(),
                story.GetFlashbackTimeline(),
                story.storyID,
                story.storyTitle,
                story.storyDialogue,
                story.opSceneID,
                story.edSceneID
                ));
    }

    public void ReverProgress()
    {
        currentStory = 0;
    }
}
