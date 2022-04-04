using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ClickDetector : MonoBehaviour
{
    void Start()
    {
        EventTrigger trigger = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerDown;
        clickEntry.callback.AddListener((eventData) => { onClick(); });
        trigger.triggers.Add(clickEntry);
    }
    void onClick()
    {
        //Debug.Log("im clicked");
        DialogueController.Instance.HandleClick();
    }
}
