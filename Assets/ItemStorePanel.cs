using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorePanel : PanelItem
{
    [SerializeField] Trading trading;


    public override void OnClick(int id)
    {
        if (GameManager.instance.itemDragAndDropController.itemSlot.item == null)
        {
            PlayerBuyItem(id);
        }
        else
        {
            // Trading process
            PlayerSellItem();
        }
        LoadAndShow();
    }

    private void PlayerBuyItem(int id)
    {
        trading.BuyItem(id);
    }

    private void PlayerSellItem()
    {
        trading.SellItem();
    }
}
