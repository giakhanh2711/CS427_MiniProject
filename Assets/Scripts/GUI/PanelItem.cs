using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelItem : MonoBehaviour
{
    public Container inventory;
    public List<InventoryButton> buttons;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetSourcePanel();
        SetItemIndex();
        LoadAndShow();
    }

    private void SetSourcePanel()
    {
        for (int i = 0; i < buttons.Count; ++i)
        {
            buttons[i].SetItemPanel(this);
        }
    }

    private void OnEnable()
    {
        Clear();
        LoadAndShow();
    }

    // To update tool bar immediately
    private void LateUpdate()
    {
        if (inventory == null)
            return;

        if (inventory.isChanged == true)
        {
            LoadAndShow();
            inventory.isChanged = false;
        }
    }

    private void SetItemIndex()
    {
        for (int i = 0; i < buttons.Count; ++i)
        {
            buttons[i].setIndex(i);
        }
    }

    public virtual void LoadAndShow()
    {
        if (inventory == null)
            return;

        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; ++i)
        {
            if (inventory.slots[i].item != null)
            {
                buttons[i].SetData(inventory.slots[i]);
            }
            else
            {
                buttons[i].ClearData();
            }
        }
    }

    public void SetInventory(Container newInventory)
    {
        inventory = newInventory;
    }

    public void Clear()
    {
        for (int i = 0; i < buttons.Count; ++i)
        {
            buttons[i].ClearData();
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
