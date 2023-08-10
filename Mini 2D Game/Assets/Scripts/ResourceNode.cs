using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ResourceNode : HitByToolObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject log; // Log prefab
    [SerializeField] float spreadRadius = 0.05f;

    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] int logCount = 5;
    [SerializeField] ResourceNodeType nodeType;
    //Character player;

    public override void Hit()
    {
        //if (player == null)
        //{
        //    player = GameManager.instance.player.GetComponent<Character>();
        //    player.ReceiveStar(starGain);
        //    Debug.Log("ADD STAR GAIN = " + starGain.ToString());
        //}
        
        while (logCount > 0)
        {
            --logCount;

            Vector3 position = transform.position;
            position.x += spreadRadius * UnityEngine.Random.Range(-1f, 1f) - spreadRadius / 2;
            position.y -= spreadRadius * UnityEngine.Random.Range(-1f, 1f) - spreadRadius / 2;

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
