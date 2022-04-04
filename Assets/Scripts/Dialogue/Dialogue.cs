using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string background;
    public string music;
    public bool flashback;
    public string nextSceneStatus;
    public Participant[] participants;
    public Sentence[] sentences;
}
