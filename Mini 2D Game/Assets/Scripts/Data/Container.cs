using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Container")]
public class Container : ScriptableObject
{
    public List<ItemSlot> slots;
    public bool isChanged;
    
    public void Add(Item item, int count = 1)
    {
        isChanged = true;

        if (item.stackable == true)
        {
            ItemSlot slot = slots.Find(x => x.item == item);

            if (slot != null)
            {
                slot.count += count;
            }
            else
            {
                slot = slots.Find(x => x.item == null);
                if (slot != null)
                {
                    slot.item = item;
                    slot.count = count;
                }
            }
        }
        else
        {
            ItemSlot slot = slots.Find(x => x.item == null);
            if (slot != null)
            {
                slot.item = item;
            }
        }
    }

    public void Remove(Item itemToRemove, int count = 1)
    {
        isChanged = true;

        if (itemToRemove.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            
            if (itemSlot == null)
            {
                return;
            }

            itemSlot.count -= count;
            
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while(count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);

                if (itemSlot == null)
                {
                    break;
                }

                itemSlot.Clear();
            }
        }
    }

    internal bool FindFreeSpace(Item item)
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].item == item)
                return true;
        }

        for (int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].item == null)
                return true;
        }

        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);

        if (itemSlot == null)
            return false;

        if (checkingItem.item.stackable == true)
            return itemSlot.count >= checkingItem.count;

        return false;
    }

}
