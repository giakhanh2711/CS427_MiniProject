using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] Container playerInventory;
    [SerializeField] PanelItem inventoryItemsPanel;
    
    Store store;

    Currency playerMoney;

    ItemStorePanel itemStorePanel;

    private void Awake()
    {
        playerMoney = GetComponent<Currency>();
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }

    public void BeginTrading(Store store)
    {
        this.store = store;

        itemStorePanel.SetInventory(store.storeContainer);

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void StopTrading()
    {
        store = null;

        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void SellItem()
    {
        if (GameManager.instance.itemDragAndDropController.CheckForSell() == true)
        {
            ItemSlot itemToSell = GameManager.instance.itemDragAndDropController.itemSlot;
            int moneyGain = (itemToSell.item.stackable) ? (int)(itemToSell.item.price * itemToSell.count * store.sellToPlayerMulti) : (int)(itemToSell.item.price * store.sellToPlayerMulti);

            playerMoney.AddMoney(moneyGain);

            itemToSell.Clear();

            GameManager.instance.itemDragAndDropController.UpdateIcon();
        }
    }

    internal void BuyItem(int id)
    {
        Item itemBought = store.storeContainer.slots[id].item;
        int price = (int)(itemBought.price * store.sellToPlayerMulti);
        if (playerMoney.CheckEnoughMoney(price) == true)
        {
            playerMoney.DecreaseMoney(price);
            playerInventory.Add(itemBought);
            inventoryItemsPanel.LoadAndShow();
        }
    }
}
