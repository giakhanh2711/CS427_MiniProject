using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    public CropManager cropManager;
    public PlaceableObjectsReferenceManager objectsManager;
   
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public Vector3Int GetGridPostition(Vector2 position, bool mousePosition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Background").GetComponent<Tilemap>();
        }

        if (tilemap == null)
            return Vector3Int.zero;

        Vector3 worldPos;

        if (mousePosition)
        {
            //Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
            //worldPos = currentCamera.GetComponent<CinemachineVirtualCamera>().
            worldPos = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPos = position;
        }

        Vector3Int gridPos = tilemap.WorldToCell(worldPos);

        return gridPos;
    }
        
    public TileBase GetTileBase(Vector3Int gridPos)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Background").GetComponent<Tilemap>();
        }

        if (tilemap == null)
            return null;

        TileBase tile = tilemap.GetTile(gridPos);

        return tile;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        //Debug.Log("Count " + dataFromTiles.Count);
        if (tileBase == null)
        {
            //Debug.Log(1);
            return null;
        }
        if (dataFromTiles.ContainsKey(tileBase) == false)
        {
            //Debug.Log(2);
            return null;
        }

        return dataFromTiles[tileBase];
    }
}
