using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TilemapReadController controller, Item item)
    {
        if (controller.cropManager.Check(gridPosition) == false)
        {
            return false;
        }

        controller.cropManager.Seed(gridPosition, item.crop);
        return true;
    }

    public override void OnItemUsed(Item usedItem, Container inventory)
    {
        inventory.Remove(usedItem);
    }
}
