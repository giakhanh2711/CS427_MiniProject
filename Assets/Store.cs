using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : InteractableObject
{
    public Container storeContainer;
    public float buyFromPlayerMulti = 1f;
    [SerializeField] GameObject shopBoss;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] bool isOpened;
    [SerializeField] GameObject storeDoorPos;

    public override void BeInteracted(Character character)
    {
        Trading trading = character.GetComponent<Trading>();
        
        if (trading == null)
            return;

        if (isOpened == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }

        trading.BeginTrading(this);
    }

    public void Open(Character character)
    {
        isOpened = true;
        shopBoss.SetActive(true);
        //chestClosed.SetActive(false);

        AudioManager.instance.Play(onOpenAudio);
        character.GetComponent<ContainerInteractController>().Open(storeContainer, storeDoorPos.transform);
    }

    public void Close(Character character)
    {
        isOpened = false;
        shopBoss.SetActive(false);
        //chestClosed.SetActive(true);

        AudioManager.instance.Play(onOpenAudio);
        character.GetComponent<ContainerInteractController>().Close();

    }
}
