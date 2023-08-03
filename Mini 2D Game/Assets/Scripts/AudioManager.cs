using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] GameObject audioSourcePrefab;
    [SerializeField] int audioSourceCount;

    List<AudioSource> audioSources;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        audioSources = new List<AudioSource>();

        for (int i = 0; i < audioSourceCount; ++i)
        {
            GameObject gameObject = Instantiate(audioSourcePrefab, transform);
            gameObject.transform.localPosition = Vector3.zero;
            audioSources.Add(gameObject.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip audioClip)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource = GetFreeAudioSource();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        for (int i = 0; i < audioSources.Count; ++i)
        {
            if (audioSources[i].isPlaying == false)
                return audioSources[i];
        }

        return audioSources[0];
    }
}
