using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Rock
}

[CreateAssetMenu(menuName ="Data/Tool Action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1.0f;
    [SerializeField] List<ResourceNodeType> resourceTypes;
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        foreach (Collider2D collider in colliders)
        {
            HitByToolObject hit = collider.GetComponent<HitByToolObject>();

            if (hit != null)
            {
                if (hit.CanBeHit(resourceTypes) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }

        return false;
    }
}
