using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    internal void Respawn(Vector3 respawnPointPosition)
    {
        MoveCharacter(respawnPointPosition);
    }

    private void MoveCharacter(Vector3 respawnPointPosition)
    {
        Transform playerTransform = instance.player.transform;
        playerTransform.position = new Vector3(
            respawnPointPosition.x,
            respawnPointPosition.y,
            respawnPointPosition.z);

        playerTransform.GetComponent<Character>().FullHeal();
        playerTransform.GetComponent<Character>().GetFullRest();
        playerTransform.GetComponent<DisableControl>().EnablePlayerControl();
    }

    public GameObject player;
    public Container inventory;
    public ItemDragAndDropController itemDragAndDropController;
    public DayNightController dayNightController;
    public PlaceableObjectsReferenceManager poReferenceManger;
    public OnScreenMessageSystem messageSystem;
}
