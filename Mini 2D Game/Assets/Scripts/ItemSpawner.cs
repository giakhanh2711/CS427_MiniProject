using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
//using static UnityEditor.Progress;

[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item itemToSpawn;
    [SerializeField] int numToSpawn;
    [SerializeField] float spreadRadius = 2f;
    [SerializeField] float probability = 0.5f;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += Spawn;
    }

    void Spawn()
    {
        if (UnityEngine.Random.value < probability)
        {
            Vector3 position = transform.position;
            position.x -= spreadRadius * UnityEngine.Random.value - spreadRadius / 2;
            position.y += spreadRadius * UnityEngine.Random.value - spreadRadius / 2;

            // Instantiate prefab

            ItemSpawnManager.instance.SpawnItem(position, itemToSpawn, numToSpawn);
        }
    }
}
