using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance
    {
        get;
        private set;
    }
    public string mode;

    [SerializeField]
    GameObject slot1;
    [SerializeField]
    GameObject slot2;
    [SerializeField]
    GameObject slot3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSlotData("Slot1", slot1);
        LoadSlotData("Slot2", slot2);
        LoadSlotData("Slot3", slot3);
    }

    void LoadSlotData(string slot,GameObject slotField)
    {
        Image slotImage = slotField.transform.Find("SlotImage").GetComponent<Image>();
        Text slotDesc = slotField.transform.Find("SlotDesc").GetComponent<Text>();
        SaveData data = DataManager.Instance.LoadProgress(slot);
        if (data != null)
        {
            //Debug.Log("GG " +data.chapter + " " + data.chapterTitle + " " + data.saveDate);
            slotImage.sprite = data.image;
            string chapter = "Chapter " + data.chapter + " - " + data.chapterTitle + "\n" + data.saveDate;
            slotDesc.text = chapter;
        }
        else
        {
            slotDesc.text = "Empty";
        }
    }
    
    public void ChangeMode(string value)
    {
        mode = value;
        slot1.GetComponent<Button>().onClick.RemoveAllListeners();
        slot2.GetComponent<Button>().onClick.RemoveAllListeners();
        slot3.GetComponent<Button>().onClick.RemoveAllListeners();

        if (mode == "Save")
        {
            slot1.GetComponent<Button>().onClick.AddListener(delegate { SaveGame("Slot1"); });
            slot2.GetComponent<Button>().onClick.AddListener(delegate { SaveGame("Slot2"); });
            slot3.GetComponent<Button>().onClick.AddListener(delegate { SaveGame("Slot3"); });
        }
        else
        {
            slot1.GetComponent<Button>().onClick.AddListener(delegate { LoadGame("Slot1"); });
            slot2.GetComponent<Button>().onClick.AddListener(delegate { LoadGame("Slot2"); });
            slot3.GetComponent<Button>().onClick.AddListener(delegate { LoadGame("Slot3"); });
        }
    }

    public void SaveGame(string slot)
    {
        //Debug.Log("Im saving " + slot);
        Story curStory = StorylineManager.Instance.GetCurrentStory();
        DataManager.Instance.SaveProgress(slot, curStory);
        GameObject slotObj = GetSlotObjByName(slot);
        if(slotObj != null)
        {
            LoadSlotData(slot, slotObj);
        }
        
    }

    public void LoadGame(string slot)
    {
        SaveData data = DataManager.Instance.LoadProgress(slot);
        StorylineManager.Instance.RevertStories();
        StorylineManager.Instance.SetCurrentStory(data.storyID);
        Loader.Instance.FadeOut();
    }

    GameObject GetSlotObjByName(string slot)
    {
        if(slot == "Slot1")
        {
            return slot1;
        }
        else if(slot == "Slot2")
        {
            return slot2;
        }
        else if(slot == "Slot3")
        {
            return slot3;
        }

        return null;
    } 
}
