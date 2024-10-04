using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 10;
    [SerializeField] IconHighlight iconHighlight;
    public Action<int> onChange;

    int toolSelectedIndex;

    public ItemSlot GetItemSlot
    {
        get
        {
            return GameManager.instance.inventory.slots[toolSelectedIndex];
        }
    }
    public Item GetItemSelected
    {
        get
        {
            return GameManager.instance.inventory.slots[toolSelectedIndex].item;
        }
    }

    private void Start()
    {
        onChange += UpdateHighlightIcon;
        UpdateHighlightIcon(toolSelectedIndex);
    }

    public void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        if (delta != 0)
        {
            if (delta > 0)
            {
                toolSelectedIndex += 1;
                toolSelectedIndex = (toolSelectedIndex >= toolbarSize) ? 0 : toolSelectedIndex;
            }
            else
            {
                toolSelectedIndex -= 1;
                toolSelectedIndex = (toolSelectedIndex < 0) ? toolbarSize - 1 : toolSelectedIndex;
            }
            onChange?.Invoke(toolSelectedIndex);
        }
    }

    internal void Set(int id)
    {
        toolSelectedIndex = id;
    }

    public void UpdateHighlightIcon(int id = 0)
    {
        Item item = GetItemSelected;

        if (item == null)
        {
            iconHighlight.Show = false;
            return;
        }

        iconHighlight.Show = item.isIconHighlight;

        if (item.isIconHighlight == true)
        {
            iconHighlight.Set(item.icon);
        }        
    }
}
