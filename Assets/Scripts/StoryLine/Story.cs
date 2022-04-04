using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather { NORMAL, RAIN }
public enum State { OPSCENE, MAINSTORY, EDSCENE, FINISHED }
[System.Serializable]
public class Story
{
    
    
    [SerializeField]
    private Weather weather;
    [SerializeField]
    private State state;
    [SerializeField]
    private bool flashback;
    [SerializeField]
    private string flashbackTimeline;

    public string storyID;
    public string storyTitle;
    public TextAsset storyDialogue;
    public string opSceneID;
    public string edSceneID;

    public Story(Weather weather, State state, bool flashback, string flashbackTimeline, string storyID, string storyTitle, TextAsset storyDialogue, string opSceneID, string edSceneID)
    {
        this.weather = weather;
        this.state = state;
        this.flashback = flashback;
        this.flashbackTimeline = flashbackTimeline;
        this.storyID = storyID;
        this.storyTitle = storyTitle;
        this.storyDialogue = storyDialogue;
        this.opSceneID = opSceneID;
        this.edSceneID = edSceneID;
    }

    public void SetNextState()
    {
        if(state == State.OPSCENE)
        {
            state = State.MAINSTORY;
        }
        else if(state == State.MAINSTORY && edSceneID != null && edSceneID != "")
        {
            state = State.EDSCENE;
        }
        else
        {
            state = State.FINISHED;
        }
    }

    public string GetSceneToLoad()
    {
        if (state == State.OPSCENE)
        {
            return opSceneID;
        }
        else if (state == State.MAINSTORY)
        {
            return "MainStory";
        }
        else if (state == State.EDSCENE)
        {
            return edSceneID;
        }
        else
        {
            return "Finished";
        }
    }

    public Weather GetWeather()
    {
        return weather;
    }

    public bool GetFlashback()
    {
        return flashback;
    }

    public string GetFlashbackTimeline()
    {
        return flashbackTimeline;
    }

    public State GetState()
    {
        return state;
    }
}
