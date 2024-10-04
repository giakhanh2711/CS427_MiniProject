using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMetal : MonoBehaviour
{
    [SerializeField] float timeToSpawn;
    [SerializeField] ItemSpawnManager spawnManager;
    [SerializeField] List<Item> itemProduced;

    float countdown;
    Vector3 pos;

    private void Awake()
    {
        countdown = timeToSpawn;
        pos = transform.position;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Vector3 posToSpawn = pos;
            posToSpawn.x = pos.x + Random.Range(-1f, 1f);
            posToSpawn.y = pos.y - gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.2f;
            posToSpawn.z = 0.5f;

            spawnManager.SpawnItem(posToSpawn, itemProduced[Random.Range(0, itemProduced.Count)], 1);

            countdown = timeToSpawn;
        }
    }
}
