using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Harvest")]
public class TilePickUpAction : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TilemapReadController controller, Item item)
    {
        controller.cropManager.PickUp(gridPosition);

        controller.objectsManager.PickUp(gridPosition);
        return true;
    }
}
