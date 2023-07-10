using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] GameObject marker;
    GameObject currentHighlightedObject;
    public void MarkerAppear(GameObject target)
    {
        if (currentHighlightedObject == target)
        {
            return;
        }

        currentHighlightedObject = target;

        Vector3 posTarget = target.transform.position;
        Debug.Log(posTarget);
        marker.transform.position = posTarget;
        marker.SetActive(true);
    }

    public void MarkerHide()
    {
        currentHighlightedObject = null;
        marker.SetActive(false);
    }
}
