using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(AudioSource))]
public class GameplayButtonBehaviour : MonoBehaviour
{
    Image buttonBox;
    AudioSource audioSource;

    [SerializeField]
    AudioClip onClickSound;

    [SerializeField]
    bool toggleButton;

    bool toggle;

    void Start()
    {
        buttonBox = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        EventTrigger trigger = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((eventData) => { onHover(); });
        trigger.triggers.Add(hoverEntry);

        EventTrigger.Entry hoverExitEntry = new EventTrigger.Entry();
        hoverExitEntry.eventID = EventTriggerType.PointerExit;
        hoverExitEntry.callback.AddListener((eventData) => { onHoverExit(); });
        trigger.triggers.Add(hoverExitEntry);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((eventData) => { onClicked(); });
        trigger.triggers.Add(clickEntry);
    }

    void onHover()
    {
        buttonBox.color = new Color(0.4f, 0.4f, 0.4f,(160f/255f));
    }

    void onHoverExit()
    {
        if (!toggle)
        {
            buttonBox.color = new Color(0, 0, 0, (160f / 255f));
        }
    }

    void onClicked()
    {
        //audioSource.PlayOneShot(onClickSound);
        SFXManager.Instance.PlayClip(onClickSound);
        if (toggleButton)
        {
            toggle = !toggle;
        }
    }

    public void ToggleOn(bool state)
    {
        toggle = state;
        if (state)
        {
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, (160f / 255f));
        }
        else
        {
            GetComponent<Image>().color = new Color(0, 0, 0, (160f / 255f));
        }
    }
}
