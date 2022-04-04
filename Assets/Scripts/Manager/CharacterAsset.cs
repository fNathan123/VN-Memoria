using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAsset
{
    public string charName;
    public Dictionary<string, Sprite> emotions;

    public CharacterAsset(string charNameVal, Dictionary<string, Sprite> emotionsVal)
    {
        charName = charNameVal;
        emotions = emotionsVal;
    }

    public Sprite GetEmotion(string emotion)
    {
        Sprite res = null;
        emotions.TryGetValue(emotion, out res);
        return res;
    }
}
