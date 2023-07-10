using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    [SerializeField] GameObject chestClosed;
    [SerializeField] GameObject chestOpened;
    [SerializeField] bool isOpened;

    public override void BeInteracted(Character character)
    {
        if (isOpened == false)
        {
            isOpened = true;
            chestOpened.SetActive(true);
            chestClosed.SetActive(false);
        }
    }
}
