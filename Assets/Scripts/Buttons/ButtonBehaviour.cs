using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonBehaviour : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;

    [SerializeField]
    AudioClip hoverSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        EventTrigger trigger = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((eventData) => { EnableSelector(); });
        trigger.triggers.Add(hoverEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((eventData) => { DisableSelector(); });
        trigger.triggers.Add(exitEntry);

        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerDown;
        clickEntry.callback.AddListener((eventData) => { onClick(); });
        trigger.triggers.Add(clickEntry);
    }

    void EnableSelector()
    {
        //audioSource.PlayOneShot(hoverSound);
        SFXManager.Instance.PlayClip(hoverSound);
        GameObject selector = transform.Find("Selector").gameObject;
        selector.SetActive(true);
        anim.SetBool("Hover",true);
    }

    void DisableSelector()
    {
        anim.SetBool("Hover", false);
        GameObject selector = transform.Find("Selector").gameObject;
        selector.SetActive(false);
    }

    void onClick()
    {
        //audioSource.PlayOneShot(hoverSound);
        SFXManager.Instance.PlayClip(hoverSound);
    }
}
