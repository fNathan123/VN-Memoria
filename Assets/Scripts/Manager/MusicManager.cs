using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance 
    {
        get;
        private set;
    }

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMusic(AudioClip audioClip)
    {
        if(audioSource.clip == null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if(audioSource.clip != audioClip)
        {
            StartCoroutine(MusicTransition(0.5f, audioClip));
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ChangeMusic(float fadeTime, AudioClip audioClip)
    {
        StartCoroutine(MusicTransition(fadeTime, audioClip));
    }

    IEnumerator MusicTransition(float FadeTime, AudioClip audioClip)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();

        audioSource.clip = audioClip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
