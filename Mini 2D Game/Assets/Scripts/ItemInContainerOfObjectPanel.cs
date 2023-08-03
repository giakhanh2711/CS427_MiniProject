using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for Item in container of an object
public class ItemInContainerOfObjectPanel : PanelItem
{
    public override void OnClick(int id)
    {
        GameManager.instance.itemDragAndDropController.OnClick(inventory.slots[id]);
        LoadAndShow();
    }
}
