using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance 
    {
        get;
        private set;
    }

    private Dictionary<string, CharacterAsset> characters;
    private Dictionary<string, AudioClip> musics;
    private Dictionary<string, AudioClip> sfx;
    private Dictionary<string, Sprite> backgrounds;

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

        characters = new Dictionary<string, CharacterAsset>();
        musics = new Dictionary<string, AudioClip>();
        backgrounds = new Dictionary<string, Sprite>();
        sfx = new Dictionary<string, AudioClip>();
        LoadAssets();
        DontDestroyOnLoad(gameObject);
    }
    
    public CharacterAsset GetCharacter(string charName)
    {
        CharacterAsset res = null;
        characters.TryGetValue(charName, out res);
        return res;
    }

    public AudioClip GetMusic(string musicName)
    {
        AudioClip res = null;
        musics.TryGetValue(musicName, out res);
        return res;
    }

    public Sprite GetBackground(string backgroundName)
    {
        Sprite res = null;
        backgrounds.TryGetValue(backgroundName, out res);
        return res;
    }
    public AudioClip GetSFX(string sfxName)
    {
        AudioClip res = null;
        sfx.TryGetValue(sfxName, out res);
        return res;
    }

    void LoadAssets()
    {
        LoadCharacterAssets();
        LoadMusics();
        LoadBackGrounds();
        LoadSFX();
    }

    #region Asset Load
    void LoadCharacterAssets()
    {
        //Neighbor410H
        Dictionary<string, Sprite> neighborAEmo = new Dictionary<string, Sprite>();
        neighborAEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Neighbor410H/Neighbor410H-Idle"));
        characters.Add("NeighborHusband",new CharacterAsset("NeighborHusband", neighborAEmo));

        //Neighbor410W
        Dictionary<string, Sprite> neighborBEmo = new Dictionary<string, Sprite>();
        neighborBEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Neighbor410W/Neighbor410W-Idle"));
        characters.Add("NeighborWife", new CharacterAsset("NeighborWife", neighborBEmo));

        //Neighbor411
        Dictionary<string, Sprite> neighbor411Emo = new Dictionary<string, Sprite>();
        neighbor411Emo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Neighbor411/Neighbor411-Idle"));
        characters.Add("Neighbor411", new CharacterAsset("Neighbor411", neighbor411Emo));

        //Will
        Dictionary<string, Sprite> willEmo = new Dictionary<string, Sprite>();
        willEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Will/Will-Idle-Front"));
        willEmo.Add("Sad", Resources.Load<Sprite>("Sprites/Characters/Will/Will-Sad"));
        willEmo.Add("Sad2", Resources.Load<Sprite>("Sprites/Characters/Will/Will-Sad2"));
        willEmo.Add("Smile", Resources.Load<Sprite>("Sprites/Characters/Will/Will-Smile"));
        willEmo.Add("WeakSmile", Resources.Load<Sprite>("Sprites/Characters/Will/Will-WeakSmile"));
        willEmo.Add("Grin", Resources.Load<Sprite>("Sprites/Characters/Will/Will-Grin"));
        characters.Add("Will", new CharacterAsset("NeighborB", willEmo));

        //Anna
        Dictionary<string, Sprite> annaEmo = new Dictionary<string, Sprite>();
        annaEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Anna/Anna-Idle"));
        annaEmo.Add("Smile-Side", Resources.Load<Sprite>("Sprites/Characters/Anna/Anna-Smile-Side"));
        annaEmo.Add("Smile-Side-Flip", Resources.Load<Sprite>("Sprites/Characters/Anna/Anna-Smile-Side-Flip"));
        annaEmo.Add("Sad-Side", Resources.Load<Sprite>("Sprites/Characters/Anna/Anna-Sad-Side"));
        annaEmo.Add("Sad-Side-Flip", Resources.Load<Sprite>("Sprites/Characters/Anna/Anna-Sad-Side-Flip"));
        characters.Add("Anna", new CharacterAsset("Anna", annaEmo));

        //Auntie
        Dictionary<string, Sprite> auntieEmo = new Dictionary<string, Sprite>();
        auntieEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Auntie/Auntie-Idle"));
        characters.Add("Auntie", new CharacterAsset("Auntie", auntieEmo));

        //Painter
        Dictionary<string, Sprite> painterEmo = new Dictionary<string, Sprite>();
        painterEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Painter/Painter-Idle"));
        characters.Add("Painter", new CharacterAsset("Painter", painterEmo));

        //Tims
        Dictionary<string, Sprite> timsEmo = new Dictionary<string, Sprite>();
        timsEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Tim/Tim-Idle"));
        timsEmo.Add("Smile-Side", Resources.Load<Sprite>("Sprites/Characters/Tim/Tim-Smile-Side"));
        timsEmo.Add("Smile-Side-Flip", Resources.Load<Sprite>("Sprites/Characters/Tim/Tim-Smile-Side-Flip"));
        characters.Add("Tims", new CharacterAsset("Tims", timsEmo));

        //Chloe
        Dictionary<string, Sprite> chloeEmo = new Dictionary<string, Sprite>();
        chloeEmo.Add("Idle", Resources.Load<Sprite>("Sprites/Characters/Chloe/Chloe-Idle-Side"));
        chloeEmo.Add("Idle-Side", Resources.Load<Sprite>("Sprites/Characters/Chloe/Chloe-Idle-Side"));
        chloeEmo.Add("Idle-Side-Flip", Resources.Load<Sprite>("Sprites/Characters/Chloe/Chloe-Idle-Side-Flip"));
        characters.Add("Chloe", new CharacterAsset("Chloe", chloeEmo));
    }

    void LoadMusics()
    {
        musics.Add("bensound-november", Resources.Load<AudioClip>("Music/bensound-november"));
        musics.Add("bensound-sadday", Resources.Load<AudioClip>("Music/bensound-sadday"));
        musics.Add("bensound-onceagain", Resources.Load<AudioClip>("Music/bensound-onceagain"));
        musics.Add("fulminis-death", Resources.Load<AudioClip>("Music/fulminis-death"));
        musics.Add("calm-and-peaceful", Resources.Load<AudioClip>("Music/Calm-and-Peaceful"));
        musics.Add("lesfm-sorrow", Resources.Load<AudioClip>("Music/lesfm-sorrow"));
        musics.Add("lesfm-emotional", Resources.Load<AudioClip>("Music/lesfm-emotional"));
        musics.Add("lesfm-tearful", Resources.Load<AudioClip>("Music/lesfm-tearful"));
        musics.Add("lesfm-calm-peaceful", Resources.Load<AudioClip>("Music/lesfm-calm-peaceful"));
        musics.Add("lesfm-drama", Resources.Load<AudioClip>("Music/lesfm-drama"));
        musics.Add("juliush-rain-tears", Resources.Load<AudioClip>("Music/juliush-rain-tears"));
        musics.Add("stock-dramatic-sad-music", Resources.Load<AudioClip>("Music/stock-dramatic-sad-music"));
        musics.Add("spheria-dont-forget-me", Resources.Load<AudioClip>("Music/Spheria-Dont-Forget-Me"));
        musics.Add("spheria-dont-forget-me-alternative-version", Resources.Load<AudioClip>("Music/Spheria-Dont-Forget-Me-Alternative-Version"));
    }

    void LoadBackGrounds()
    {
        backgrounds.Add("BG1", Resources.Load<Sprite>("Sprites/Background/BG1"));
        backgrounds.Add("BG-Apartment_Hallway", Resources.Load<Sprite>("Sprites/Background/BG-Apartment_Hallway"));
        backgrounds.Add("BG-Park", Resources.Load<Sprite>("Sprites/Background/BG-Park"));
        backgrounds.Add("BG-Bakery", Resources.Load<Sprite>("Sprites/Background/BG-Bakery"));
        backgrounds.Add("BG-Flower", Resources.Load<Sprite>("Sprites/Background/BG-Flower"));
        backgrounds.Add("BG-Tree", Resources.Load<Sprite>("Sprites/Background/BG-Tree"));
        backgrounds.Add("BG-Apartment", Resources.Load<Sprite>("Sprites/Background/BG-Apartment"));
        backgrounds.Add("BG-Library", Resources.Load<Sprite>("Sprites/Background/BG-Library"));
        backgrounds.Add("BG-Orphanage", Resources.Load<Sprite>("Sprites/Background/BG-Orphanage"));
        backgrounds.Add("BG-Hospital", Resources.Load<Sprite>("Sprites/Background/BG-Hospital"));
        backgrounds.Add("BG-Inside-Orphanage", Resources.Load<Sprite>("Sprites/Background/BG-Inside-Orphanage"));
        backgrounds.Add("BG-Marketplace", Resources.Load<Sprite>("Sprites/Background/BG-Marketplace"));
    }

    void LoadSFX()
    {
        sfx.Add("knocking", Resources.Load<AudioClip>("SFX/Knock-WoodDoor"));
        sfx.Add("door-open", Resources.Load<AudioClip>("SFX/mixkit-door-open"));
        sfx.Add("door-closed", Resources.Load<AudioClip>("SFX/mixkit-closing-door"));
    }
    #endregion


}
