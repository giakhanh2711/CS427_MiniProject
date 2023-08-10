using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class ItemConvertorData
{
    public ItemSlot itemSlot;
    public float timer;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
    }
}

public class Convertor : InteractableObject, IPersistant
{
    [SerializeField] Item itemToProcess;
    [SerializeField] Item itemResult;
    [SerializeField] int timeToProcess = 5;
    [SerializeField] int itemResultCount = 1;

    [SerializeField] GameObject panelConvertorPrefab;
    //[SerializeField] RecipePanel convertorPanel;
    //[SerializeField] string panelName;
    

    ItemConvertorData data;
    Animator animator;

    private void Start()
    {
        if (data == null)
        {
            data = new ItemConvertorData();
        }
        
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ConvertProcess();
    }
    
    // Continuously called with time agent
    private void ConvertProcess()
    {
        if (data.itemSlot == null)
            return;

        if (data.timer > 0f)
        {
            data.timer -= Time.deltaTime;

            if (data.timer <= 0f)
            {
                CompleteConversion();
                Debug.Log("Conversion complete");
            }
        }
    }

    public override void BeInteracted(Character character)
    {
        if (panelConvertorPrefab != null)
        {
            panelConvertorPrefab.SetActive(true);
            panelConvertorPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (data.itemSlot.item == null)
        {
            // When item from inventory is chosen
            if (GameManager.instance.itemDragAndDropController.CheckEnoughOrMatch(itemToProcess))
            {
                StartProcessing();
                GameManager.instance.itemDragAndDropController.itemSlot.Clear();
                //return;
            }
        }

        // When finish processing
        //if (data.itemSlot.item != null && data.timer < 0f)
        //{
        //    GameManager.instance.inventory.Add(data.itemSlot.item, data.itemSlot.count);
        //    data.itemSlot.Clear();
        //    //CompleteConversion();
        //}
    }

    private void StartProcessing()
    {
        animator.SetBool("isWorking", true);
        data.itemSlot.Copy(GameManager.instance.itemDragAndDropController.itemSlot);
        data.itemSlot.count = 1;

        //GameManager.instance.itemDragAndDropController.RemoveItem();

        GameManager.instance.inventory.Remove(data.itemSlot.item);
        
        data.timer = timeToProcess;
    }

    private void CompleteConversion()
    {
        animator.SetBool("isWorking", false);
        data.itemSlot.Clear();
        data.itemSlot.Set(itemResult, itemResultCount);
        //data.itemSlot.Set(itemResult, itemResultCount);

        //// Update newly created output immediately 
        GameManager.instance.inventory.Add(data.itemSlot.item, data.itemSlot.count);
        data.itemSlot.Clear();
    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    }
}
