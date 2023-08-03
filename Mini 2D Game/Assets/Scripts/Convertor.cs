using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class Convertor : InteractableObject
{
    [SerializeField] Item itemToProcess;
    [SerializeField] Item itemResult;
    [SerializeField] int timeToProcess = 5;
    [SerializeField] int itemResultCount = 1;

    ItemSlot itemSlot;
    float timer;
    Animator animator;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += ConvertProcess;
        itemSlot = new ItemSlot();
        animator = GetComponent<Animator>();
    }

    // Continuously called with time agent
    private void ConvertProcess()
    {
        if (itemSlot == null)
            return;

        if (timer > 0f)
        {
            timer -= 1;

            if (timer <= 0f)
            {
                CompleteConversion();
                Debug.Log("Conversion complete");
            }
        }
    }

    public override void BeInteracted(Character character)
    {
        if (itemSlot.item == null)
        {
            // When item from inventory is chosen
            if (GameManager.instance.itemDragAndDropController.CheckEnoughOrMatch(itemToProcess))
            {
                StartProcessing(GameManager.instance.itemDragAndDropController.itemSlot);
                return;
            }

            // If item from tool bar is chosen
            ToolbarController toolbarController = character.GetComponent<ToolbarController>();
            if (toolbarController == null)
                return;

            ItemSlot iSlot = toolbarController.GetItemSlot;

            if (iSlot.item == itemToProcess)
            {
                StartProcessing(iSlot);
                return;
            }
        }
        
        //// When finish processing
        //if (itemSlot.item != null && timer < 0f)
        //{
        //    GameManager.instance.inventory.Add(itemSlot.item, itemSlot.count);
        //    itemSlot.Clear();
        //}
    }

    private void StartProcessing(ItemSlot itemToProcess)
    {
        animator.SetBool("isWorking", true);
        itemSlot.Copy(GameManager.instance.itemDragAndDropController.itemSlot);
        
        if (itemToProcess.item.stackable)
        {
            itemToProcess.count -= 1;
            if (itemToProcess.count <= 0)
            {
                itemToProcess.Clear();
            }
        }
        else
        {
            itemToProcess.Clear();
        }

        timer = timeToProcess;
    }

    private void CompleteConversion()
    {
        animator.SetBool("isWorking", false);
        itemSlot.Clear();
        itemSlot.Set(itemResult, itemResultCount);

        // Update newly created output immediately 
        GameManager.instance.inventory.Add(itemSlot.item, itemSlot.count);
        itemSlot.Clear();
    }
}
