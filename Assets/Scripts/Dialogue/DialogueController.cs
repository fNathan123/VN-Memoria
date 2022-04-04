using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance 
    {
        get;
        private set;
    }

    [SerializeField]
    Image background;
    [SerializeField]
    Image portrait1;
    [SerializeField]
    Image portrait2;
    [SerializeField]
    Image dialogueBox;
    [SerializeField]
    Image nameBox;
    [SerializeField]
    Image finishIcon;

    [SerializeField]
    Button autoButton;
    [SerializeField]
    Button skipButton;
    [SerializeField]
    Button saveButton;
    [SerializeField]
    Button loadButton;

    [SerializeField]
    Text nameText;
    [SerializeField]
    Text dialogText;

    Queue<Sentence> conversation;
    Dialogue dialogue;
    TextAsset jsonFile;

    Dictionary<string, CharacterAsset> participants;

    float textSpeed;
    IEnumerator dialogueCoroutine;
    bool isCoroutineFinished;
    bool isStoryFinished;
    bool isReady;

    IEnumerator autoForwardCoroutine;
    bool autoForward;
    float autoForwardTime=3f;

    bool skip;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        conversation = new Queue<Sentence>();
        jsonFile = StorylineManager.Instance.GetStoryDialogue();
        dialogue = JsonUtility.FromJson<Dialogue>(jsonFile.text);
        participants = new Dictionary<string, CharacterAsset>();

        Sprite bgImage = AssetManager.Instance.GetBackground(dialogue.background);
        background.sprite = bgImage;

        AudioClip bgMusic = AssetManager.Instance.GetMusic(dialogue.music);
        MusicManager.Instance.SetMusic(bgMusic);

        foreach(Participant p in dialogue.participants)
        {
            participants.Add(p.name,AssetManager.Instance.GetCharacter(p.name));
        }
        disableAllUI();
        autoForward = DataManager.Instance.autoForwardState;
        skip = DataManager.Instance.skipState;

        textSpeed = DataManager.Instance.GetTextSpeed();
        autoForwardTime = DataManager.Instance.GetAutoSpeed();
        
        autoButton.GetComponent<GameplayButtonBehaviour>().ToggleOn(autoForward);
        skipButton.GetComponent<GameplayButtonBehaviour>().ToggleOn(skip);

        StartCoroutine(StartDialogue());
    }

    void Update()
    {

    }

    void disableAllUI()
    {
        portrait1.enabled = false;
        portrait2.enabled = false;
        dialogueBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;
        nameBox.enabled = false;

        autoButton.gameObject.SetActive(false);
        //skipButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
    }

    void enableAllUI()
    {
        portrait1.enabled = true;
        portrait2.enabled = true;
        dialogueBox.enabled = true;
        nameText.enabled = true;
        dialogText.enabled = true;
        nameBox.enabled = true;

        autoButton.gameObject.SetActive(true);
        //skipButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        loadButton.gameObject.SetActive(true);
    }

    void DisablePortrait()
    {
        portrait1.enabled = false;
        portrait2.enabled = false;
    }

    public IEnumerator StartDialogue()
    {
        while (Loader.Instance.state == LoaderState.NOTREADY)
        {
            yield return null;
        }

        foreach (Sentence sentence in dialogue.sentences)
        {
            conversation.Enqueue(sentence);
        }

        enableAllUI();
        DisablePortrait();
        ShowNextDialogue();
        isReady = true;
    }
    public void ShowNextDialogue()
    {
        if (conversation.Count == 0)
        {
            isStoryFinished = true;
            CloseDialogue();
            return;
        }

        Sentence sentence = conversation.Peek();

        if(sentence.music != null && sentence.music != "")
        {
            AudioClip bgMusic = AssetManager.Instance.GetMusic(sentence.music);
            MusicManager.Instance.ChangeMusic(2f, bgMusic);
        }

        if(sentence.sfx != null && sentence.sfx != "")
        {
            AudioClip sfx = AssetManager.Instance.GetSFX(sentence.sfx);
            SFXManager.Instance.PlayClip(sfx);
        }

        if (sentence.name == "Narator")
        {
            portrait2.enabled = false;
            portrait1.enabled = false;
            nameBox.enabled = false;
            nameText.text = "";

            dialogueCoroutine = ShowText(sentence, 0.1f,false);
            StartCoroutine(dialogueCoroutine);
        }
        else if (sentence.name == "Narator-Participant")
        {
            nameBox.enabled = false;
            nameText.text = "";
            dialogueCoroutine = ShowText(sentence, 0.1f,false);
            StartCoroutine(dialogueCoroutine);
        }
        else
        {
            ManageSentence(sentence.pos, sentence);
            ManageConversation(sentence.pos);
        }
    }

    void ManageSentence(string pos,Sentence sentence)
    {
        Image portrait = pos == "R" ? portrait2 : portrait1;
        
        nameBox.enabled = true;
        nameText.text = sentence.name;
        //dialogText.text = sentence.sentence;
        Sprite charImg = GetCharAsset(sentence.name, sentence.expression);
        if(charImg != portrait.sprite)
        {
            portrait.sprite = charImg;
            portrait.gameObject.GetComponent<Animator>().SetTrigger("InTransition");
        }

        if(sentence.effect != null && sentence.effect != "")
        {
            portrait.GetComponent<Shake>().ShakeEffect();
        }
        portrait.enabled = true;
        //if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = ShowText(sentence, 0.1f, true);
        StartCoroutine(dialogueCoroutine);
    }

    IEnumerator ShowText(Sentence sentence,float delay,bool append)
    {
        dialogText.text ="";
        isCoroutineFinished = false;
        finishIcon.gameObject.SetActive(false);
        string sentenceAppend = sentence.sentence;
        if (append)
        {
            sentenceAppend = '"' + sentenceAppend + '"'; 
        }
        foreach (char c in sentenceAppend)
        {
            dialogText.text += c;
            //yield return null;
            yield return new WaitForSeconds(textSpeed);
        }
        conversation.Dequeue();
        isCoroutineFinished = true;
        finishIcon.gameObject.SetActive(true);
        if (autoForward || skip)
        {
            autoForwardCoroutine = AutoForward();
            StartCoroutine(autoForwardCoroutine);
        }
    }

    IEnumerator AutoForward()
    {
        yield return new WaitForSecondsRealtime(autoForwardTime);
        NextDialogue();
    }

    void ManageConversation(string pos)
    {
        if (pos == "R")
        {
            portrait1.color = new Color(0.3f, 0.3f, 0.3f);
            portrait2.color= new Color(1f, 1f, 1f);
        }

        if (pos == "L")
        {
            portrait2.color = new Color(0.3f, 0.3f, 0.3f);
            portrait1.color = new Color(1f, 1f, 1f);
        }
    }

    void StopDialogueCoroutine()
    {
        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        Sentence sentence = conversation.Dequeue();
        string t = sentence.sentence;
        if(sentence.name != "Narator" && sentence.name != "Narator-Participant")
        {
            t = '"' + t + '"';
        }
        dialogText.text = t;
        isCoroutineFinished = true;
        finishIcon.gameObject.SetActive(true);
    }

    void CloseDialogue()
    {
        disableAllUI();
        if (dialogue.flashback || (dialogue.nextSceneStatus != null && dialogue.nextSceneStatus == "Flashback"))
        {
            Loader.Instance.FadeOut();
        }
        else
        {
            Loader.Instance.SwipeFadeOut();
        }
    }

    Sprite GetCharAsset(string charName,string emotion)
    {
        CharacterAsset character=null;
        participants.TryGetValue(charName, out character);
        Sprite asset = character.GetEmotion(emotion);
        return asset;
    }

    public void HandleClick()
    {
        if (autoForward || skip)
        {
            
        }
        else
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        if (isReady)
        {
            if (isCoroutineFinished && !isStoryFinished)
            {
                ShowNextDialogue();
            }
            else if (!isCoroutineFinished)
            {
                StopDialogueCoroutine();
            }
        }
    }

    public void onAutoButtonClicked()
    {
        autoForward = !autoForward;
        DataManager.Instance.autoForwardState = autoForward;
        if (autoForward)
        {
            if (autoForwardCoroutine != null) StopCoroutine(autoForwardCoroutine);
            if (isCoroutineFinished)
            {
                autoForwardCoroutine = AutoForward();
                StartCoroutine(autoForwardCoroutine);
            }
        }
        else
        {
            StopCoroutine(autoForwardCoroutine);
        }
    }

    public void onSkipButtonClicked()
    {
        skip = !skip;
        DataManager.Instance.skipState = skip;
        if (skip)
        {
            textSpeed = DataManager.Instance.GetTextSpeed();
            autoForwardTime = DataManager.Instance.GetAutoSpeed();
            if(autoForwardCoroutine != null) StopCoroutine(autoForwardCoroutine);
            if (isCoroutineFinished)
            {
                autoForwardCoroutine = AutoForward();
                StartCoroutine(autoForwardCoroutine);
            }
        }
        else
        {
            textSpeed = DataManager.Instance.GetTextSpeed();
            autoForwardTime = DataManager.Instance.GetAutoSpeed();
        }
    }
}
