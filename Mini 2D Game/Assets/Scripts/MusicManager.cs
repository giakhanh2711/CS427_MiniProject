using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch;

    [SerializeField] AudioClip playOnStart;
    
    AudioClip audioToSwitchTo;
    float volume;

    private void Start()
    {
        Play(playOnStart, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null)
            return;

        if (interrupt == true)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }

        else
        {
            audioToSwitchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    IEnumerator SmoothSwitchMusic()
    {
        volume = 1f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;

            if (volume < 0f)
                volume = 0f;

            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(audioToSwitchTo, true);
    }
}
