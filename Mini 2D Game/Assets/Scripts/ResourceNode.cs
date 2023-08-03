using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ResourceNode : HitByToolObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject log; // Log prefab
    [SerializeField] float spreadRadius = 1f;

    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] int logCount = 5;
    [SerializeField] ResourceNodeType nodeType;

    public override void Hit()
    {
        while (logCount > 0)
        {
            --logCount;

            Vector3 position = transform.position;
            position.x -= spreadRadius * UnityEngine.Random.value - spreadRadius / 2;
            position.y += spreadRadius * UnityEngine.Random.value - spreadRadius / 2;

            // Instantiate prefab
           
            ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }

        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> types)
    {
        return types.Contains(nodeType);
    }
}
