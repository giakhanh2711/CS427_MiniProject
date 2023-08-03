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

    public GameObject player;
    public Container inventory;
    public ItemDragAndDropController itemDragAndDropController;
    public DayNightController dayNightController;
    public PlaceableObjectsReferenceManager poReferenceManger;
    public OnScreenMessageSystem messageSystem;
}
