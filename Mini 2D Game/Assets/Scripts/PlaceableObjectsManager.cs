using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjectsContainer;
    [SerializeField] Tilemap targetTilemap;

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
        PlaceableObject placeableObject = placeableObjectsContainer.Get(gridPosition);

        if (placeableObject == null)
            return;

        ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placeableObject.placedItem, 1);

        Destroy(placeableObject.targetObject.gameObject);

        placeableObjectsContainer.Remove(placeableObject);
    }
}