using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager poManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (poManager == null)
        {
            return;
        }

        poManager.Place(item, pos);
    }

    public bool Check(Vector3Int pos)
    {
        if (poManager == null)
        {
            Debug.Log("poManager = null");
            return false;
        }

        return poManager.Check(pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (poManager == null)
        {
            Debug.Log("poManager = null");
            return;
        }

        poManager.PickUp(gridPosition);
    }
}
