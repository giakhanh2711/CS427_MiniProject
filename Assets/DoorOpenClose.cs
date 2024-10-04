using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class DoorOpenClose : InteractableObject
{
    Animator animator;
    [SerializeField] GameObject sleepPanel;
    [SerializeField] AudioClip onOpenAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void BeInteracted(Character character)
    {
        animator.SetBool("isOpened", true);
        sleepPanel.SetActive(true);
        AudioManager.instance.Play(onOpenAudio);
    }

    internal void CloseDoor()
    {
        animator.SetBool("isOpened", false);
        AudioManager.instance.Play(onOpenAudio);
    }
}
