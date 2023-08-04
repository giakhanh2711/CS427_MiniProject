using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

    public bool isComplete
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Tilemap TargetTilemap
    {
        get
        {
            if (targetTilemap == null)
            {
                targetTilemap = GameObject.Find("CropTilemap").GetComponent<Tilemap>();
            }
            return targetTilemap;
        }
    }

    [SerializeField] GameObject cropSpritePrefab;

    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        if (TargetTilemap == null)
            return;

        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop == null)
            {
                continue;
            }

            cropTile.damage += 0.02f;

            if (cropTile.damage > 1f)
            {
                cropTile.Harvested();
                TargetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }

            if (cropTile.isComplete)
            {
                Debug.Log("Fully growing");
                continue;
            }

            cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                cropTile.growStage += 1;
            }
        }
    }

    public bool Check(Vector3Int position)
    {
        if (TargetTilemap == null)
            return false;

        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (TargetTilemap == null)
            return;

        Debug.Log("Crop manager " + crops.ContainsKey((Vector2Int)position));

        if (crops.ContainsKey((Vector2Int)position) == true)
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (TargetTilemap == null)
            return;

        seeded = toSeed.seededTile;
        
        TargetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        if (TargetTilemap == null)
            return;

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject gameObject = Instantiate(cropSpritePrefab);
        gameObject.transform.position = TargetTilemap.CellToWorld(position);
        gameObject.transform.position -= Vector3.forward * 0.01f;
        gameObject.SetActive(false);
        crop.renderer = gameObject.GetComponent<SpriteRenderer>();

        crop.position = position;

        TargetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (TargetTilemap == null)
            return;

        Vector2Int position = (Vector2Int)gridPosition;
        if (crops.ContainsKey(position) == false)
        {
            return;
        }

        CropTile cropTile = crops[position];

        if (cropTile.isComplete)
        {
            ItemSpawnManager.instance.SpawnItem(
                TargetTilemap.CellToWorld(gridPosition),
                cropTile.crop.yield,
                cropTile.crop.count
                );

            TargetTilemap.SetTile(gridPosition, plowed);

            cropTile.Harvested();
        }
    }
}
