using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class SaveSlotBehaviour : MonoBehaviour
{
    Image selector;

    [SerializeField]
    AudioClip hoverSound;

    private void Awake()
    {
        selector = GetComponent<Image>();

        EventTrigger trigger = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((eventData) => { EnableSelector(); });
        trigger.triggers.Add(hoverEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((eventData) => { DisableSelector(); });
        trigger.triggers.Add(exitEntry);
    }

    void EnableSelector()
    {
        selector.enabled = true;
        SFXManager.Instance.PlayClip(hoverSound);
    }

    void DisableSelector()
    {
        selector.enabled = false;
    }
}
