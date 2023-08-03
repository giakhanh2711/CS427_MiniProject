using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Placing Object")]
public class PlacingObjects : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TilemapReadController controller, Item item)
    {
        if (controller.objectsManager.Check(gridPosition) == true)
        {
            return false;
        }

        if (item.itemPrefab == null)
        {
            return false;
        }
        controller.objectsManager.Place(item, gridPosition);
        return true;
    }

    public override void OnItemUsed(Item usedItem, Container inventory)
    {
        if (GameManager.instance.poReferenceManger.poManager.canPlaceObject == false)
            return;

        inventory.Remove(usedItem);
    }
}
