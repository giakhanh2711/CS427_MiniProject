using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 5f;
    [SerializeField] float spawnArea_width = 5f;

    [SerializeField] GameObject[] spawnObjects;
    int length;
    int curSpawnNumber = 0;
    
    [SerializeField] float probToSpawn = 0.5f;
    [SerializeField] int spawnCount = 1;

    [SerializeField] bool isSpawnOneTime = false;
    [SerializeField] int spawnLimit = 10;

    private void Start()
    {
        length = spawnObjects.Length;

        if (isSpawnOneTime == false)
        {
            // SPAWN OBJECTS LIÊN TỤC TỪNG TICK
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            timeAgent.onTimeTick += Spawn;
        }
        else
        {
            Spawn();
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        if (Random.value > probToSpawn)
            return;

        for (int i = 0; i < spawnCount; ++i)
        {
            /// Spawn objects
            /// Khi instantiate thì phải dùng prefab
            GameObject go = Instantiate(spawnObjects[Random.Range(0, length)]);
            Transform transformGO = go.transform;

            Vector3 position = transform.position;
            position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
            position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);
            transformGO.position = position;
            ///

            //Vector3 position = transform.position;
            //position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
            //position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);
            ////transform.position = position;
            //go.transform.position = position;

            //GameObject go = Instantiate(spawnObjects[Random.Range(0, length)]);
            ////Transform transform = go.transform;

            ++curSpawnNumber;

            if (curSpawnNumber >= spawnLimit)
                return;
        }
    }

    // Vẽ 1 hình chữ nhật
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));

    }
}
