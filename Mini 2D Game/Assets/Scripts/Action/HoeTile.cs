using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class HoeTile : ToolAction
{

    [SerializeField] List<TileBase> plowableTiles;
    [SerializeField] AudioClip onHoeUsed;

    public override bool OnApplyToTilemap(Vector3Int gridPosition, TilemapReadController controller, Item item)
    {
        TileBase tileToPlow = controller.GetTileBase(gridPosition);
        
        if (plowableTiles.Contains(tileToPlow) == false)
        {
            return false;
        }

        controller.cropManager.Plow(gridPosition);

        AudioManager.instance.Play(onHoeUsed);

        return true;
    }
}
