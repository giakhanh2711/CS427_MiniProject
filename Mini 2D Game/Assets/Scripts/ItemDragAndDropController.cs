using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    public ItemSlot itemSlot;
    [SerializeField] GameObject dragIcon;
    RectTransform dragIconTransform;
    Image dragIconImage;

    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new ItemSlot();
        dragIconTransform = dragIcon.GetComponent<RectTransform>();
        dragIconImage = dragIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (dragIcon.activeInHierarchy == true)
        {
            dragIconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0)) 
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    
                    ItemSpawnManager.instance.SpawnItem(worldPosition, itemSlot.item, itemSlot.count);

                    itemSlot.Clear();
                    dragIcon.SetActive(false);
                }
            }
           
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            if (itemSlot.item == this.itemSlot.item)
            {
                itemSlot.count += this.itemSlot.count;
                this.itemSlot.Clear();
            }
            else
            {
                Item iTmp = itemSlot.item;
                int cTmp = itemSlot.count;

                itemSlot.Copy(this.itemSlot);
                this.itemSlot.Set(iTmp, cTmp);
            }
        }
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            dragIcon.SetActive(false);
        }
        else
        {
            dragIcon.SetActive(true);
            dragIconImage.sprite = itemSlot.item.icon;
        }
    }


    public bool CheckEnoughOrMatch(Item item, int count = 1)
    {
        if (itemSlot == null)
            return false;

        if (item.stackable)
        {
            return itemSlot.item == item && itemSlot.count >= count;
        }

        return itemSlot.item == item;
    }

    internal void RemoveItem(int count = 1)
    {
        if (itemSlot == null)
            return;

        if (itemSlot.item.stackable)
        {
            itemSlot.count -= count;

            if (itemSlot.count <= 0)
                itemSlot.Clear();
        }
        else
        {
            itemSlot.Clear();
        }

        UpdateIcon();
    }

    public bool CheckForSell()
    {
        if (itemSlot.item == null)
            return false;

        if (itemSlot.item.canBeSold == false)
            return false;

        return true;
    }
}
