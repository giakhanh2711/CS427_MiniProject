using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using static UnityEditor.Progress;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjectsContainer;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] Container inventory;

    public bool canPlaceObject;
    private void Start()
    {
        GameManager.instance.GetComponent<PlaceableObjectsReferenceManager>().poManager = this;
        VisualizeMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; ++i)
        {
            if (placeableObjectsContainer.placeableObjects[i].targetObject == null)
            {
                continue;
            }

            IPersistant persistant = placeableObjectsContainer.placeableObjects[i].targetObject.GetComponent<IPersistant>();

            if (persistant != null)
            {
                string jsonString = persistant.Read();
                placeableObjectsContainer.placeableObjects[i].objectState = jsonString;
            }

            placeableObjectsContainer.placeableObjects[i].targetObject = null;
        }
    }


    private void VisualizeMap()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; ++i)
        {
            VisualizeItem(placeableObjectsContainer.placeableObjects[i]);
        }
    }

    // Đặt object lên background
    private void VisualizeItem(PlaceableObject placeableObject)
    {
        GameObject gameObject = Instantiate(placeableObject.placedItem.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(placeableObject.positionOnGrid) + targetTilemap.cellSize / 2;
        position -= Vector3.forward * 0.1f;
        gameObject.transform.position = position;

        IPersistant persistant = gameObject.GetComponent<IPersistant>();
        if (persistant != null)
        {
            persistant.Load(placeableObject.objectState);
        }

        placeableObject.targetObject = gameObject.transform;
    }

    // Đặt object lên background
    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid) == true) // There is object in this position
        {
            canPlaceObject = false;
            return;
        }

        canPlaceObject = true;
        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        VisualizeItem(placeableObject);
        placeableObjectsContainer.placeableObjects.Add(placeableObject);
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjectsContainer.Get(position) != null;
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Debug.Log("PickUP calleddddddddddd");
        PlaceableObject placeableObject = placeableObjectsContainer.Get(gridPosition);

        if (placeableObject == null)
            return;

        ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placeableObject.placedItem, 1);

        inventory.Add(placeableObject.placedItem);
        Destroy(placeableObject.targetObject.gameObject);

        placeableObjectsContainer.Remove(placeableObject);
    }
}