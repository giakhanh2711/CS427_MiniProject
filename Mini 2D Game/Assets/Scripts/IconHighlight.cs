using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;
    [SerializeField] Tilemap targetTilemap;

    Vector3 targetPosition;
    SpriteRenderer spriteRenderer;

    bool canSelect = true;
    bool isShow;

    Tilemap TargetTilemap
    {
        get
        {
            if (targetTilemap == null)
            {
                targetTilemap = GameObject.Find("Background").GetComponent<Tilemap>();
            }
            return targetTilemap;
        }
    }


    public bool CanSelect
    {
        set
        {
            Debug.Log("CanSelect: " + (canSelect && isShow));
            canSelect = value;
            gameObject.SetActive(canSelect && isShow);
        }
    }

    public bool Show
    {
        set
        {
            Debug.Log("Show: " + (canSelect && isShow));
            isShow = value;
            gameObject.SetActive(canSelect && isShow);
        }
    }

    private void Update()
    {
        targetPosition = TargetTilemap.CellToWorld(cellPosition);
        transform.position = targetPosition + TargetTilemap.cellSize / 2;
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = icon;
    }
}
