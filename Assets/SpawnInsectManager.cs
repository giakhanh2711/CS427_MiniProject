using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SpawnInsectManager : MonoBehaviour
{
    [SerializeField] GameObject objectSpawner;
    [SerializeField] CropManager cropManager;
    [SerializeField] float distHorizontal = 1f;
    [SerializeField] float distVertical = 1f;
    [SerializeField] GameObject cropPosition;
    [SerializeField] List<Crop> grownCrop;
    [SerializeField] float timeToKill = 10f;

    DayNightController dayNightController; // To get cur time
    [SerializeField] GameObject player; // To get position
    float countDown;
    Vector3 spawnerPos = Vector3.zero;

    public Vector3 SpawnerPos
    {
        get
        {
            return spawnerPos;
        }
    }
    
     
    private void Awake()
    {
        dayNightController = gameObject.GetComponent<DayNightController>();
        cropManager = gameObject.GetComponent<CropManager>();
        //player = GameManager.instance.player;
    }

    private void Update()
    {
        SpawnInsect();

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
    }

    private void SpawnInsect()
    {
        float distance = Vector3.Distance(player.transform.position, cropPosition.transform.position);
        Debug.Log("Crop count" + cropManager.Crops.Count.ToString());
        if ((distance <= distHorizontal || distance <= distVertical) && cropManager.Crops.Count > 0)
        {
            Debug.Log("Yesssssssssssssssssssss");
            if (countDown <= 0)
            {   
                int numSpawner = 2 * player.GetComponent<Character>().GetLevel();
                
                if (numSpawner > cropManager.Crops.Count)
                    numSpawner = cropManager.Crops.Count;

                countDown = (timeToKill) * numSpawner;

                Dictionary<Vector2Int, CropTile> dict = cropManager.Crops;
                
                List<Vector2Int> keyList = new List<Vector2Int>(dict.Keys);
           
                while (numSpawner > 0)
                {
                    --numSpawner;
                    Vector2Int pos = keyList[UnityEngine.Random.Range(0, dict.Count)];
                    spawnerPos = new Vector3(pos.x + UnityEngine.Random.Range(-0.2f, 0.2f), pos.y + UnityEngine.Random.Range(-0.2f, 0.2f), cropPosition.transform.position.z);
                    Instantiate(objectSpawner, spawnerPos, Quaternion.identity);
                    objectSpawner.GetComponent<ObjectSpawner>().Spawn();
                    
                    countDown += Time.deltaTime;
                }
            }
        }
    }
}
