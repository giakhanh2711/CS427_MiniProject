using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Chest : InteractableObject
{
    [SerializeField] GameObject chestClosed;
    [SerializeField] GameObject chestOpened;
    [SerializeField] bool isOpened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] Container containerOfObject;

    public override void BeInteracted(Character character)
    {
        if (isOpened == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
    }

    public void Open(Character character)
    {
        isOpened = true;
        chestOpened.SetActive(true);
        chestClosed.SetActive(false);

        AudioManager.instance.Play(onOpenAudio);
        character.GetComponent<ContainerInteractController>().Open(containerOfObject, transform);
    }

    public void Close(Character character)
    {
        isOpened = false;
        chestOpened.SetActive(false);
        chestClosed.SetActive(true);

        AudioManager.instance.Play(onOpenAudio);
        character.GetComponent<ContainerInteractController>().Close();

    }
}
