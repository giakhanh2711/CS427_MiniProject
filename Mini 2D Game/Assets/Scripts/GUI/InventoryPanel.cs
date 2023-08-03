using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : PanelItem
{
    public override void OnClick(int id)
    {
        GameManager.instance.itemDragAndDropController.OnClick(inventory.slots[id]);
        LoadAndShow();
    }
}
