using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

[Serializable]
//public class ItemConvertorData
//{
//    public ItemSlot itemSlot;
//    public float timer;

//    public ItemConvertorData()
//    {
//        itemSlot = new ItemSlot();
//    }
//}

public class Convertor : InteractableObject
{
    //[SerializeField] Item itemToProcess;
    //[SerializeField] Item itemResult;
    //[SerializeField] int timeToProcess = 5;
    //[SerializeField] int itemResultCount = 1;

    [SerializeField] GameObject panelConvertorPrefab;
    //[SerializeField] RecipePanel convertorPanel;
    //[SerializeField] string panelName;


    //ItemConvertorData data;
    [SerializeField] Container inventory;
    public ItemSpawnManager spawner;

    [SerializeField] float offsetDistance = 1.0f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    ItemSlot itemResult;
    Animator animator;
    GameObject player;
    float timer;

    private void Start()
    {
        //if (data == null)
        //{
        //    data = new ItemConvertorData();
        //}
        
        animator = GetComponent<Animator>();
        player = GameManager.instance.player;
        spawner = GameManager.instance.GetComponent<ItemSpawnManager>();
    }

    private void Update()
    {
        HidePanel();

        ConvertProcess();
    }
    
    private void HidePanel()
    {
        Vector2 position = player.GetComponent<Rigidbody2D>().position + player.GetComponent<CharacterController2D>().lastMotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            InteractableObject obj = collider.GetComponent<InteractableObject>();
            if (obj != null)
            {
                //marker.MarkerAppear(obj.gameObject);
                return;
            }
        }

        panelConvertorPrefab.SetActive(false);
    }

    // Continuously called with time agent
    private void ConvertProcess()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                CompleteConversion();
                Debug.Log("Conversion complete");
            }
        }
        //if (data.itemSlot == null)
        //    return;

        //if (data.timer > 0f)
        //{
        //    data.timer -= Time.deltaTime;

        //    if (data.timer <= 0f)
        //    {
        //        CompleteConversion();
        //        Debug.Log("Conversion complete");
        //    }
        //}
    }

    public override void BeInteracted(Character character)
    {
        if (panelConvertorPrefab != null)
        {
            panelConvertorPrefab.SetActive(true);
        }

        //if (data.itemSlot.item == null)
        //{
        //    // When item from inventory is chosen
        //    if (GameManager.instance.itemDragAndDropController.CheckEnoughOrMatch(itemToProcess))
        //    {
        //        StartProcessing();
        //        GameManager.instance.itemDragAndDropController.itemSlot.Clear();
        //        //return;
        //    }
        //}

        // When finish processing
        //if (data.itemSlot.item != null && data.timer < 0f)
        //{
        //    GameManager.instance.inventory.Add(data.itemSlot.item, data.itemSlot.count);
        //    data.itemSlot.Clear();
        //    //CompleteConversion();
        //}
    }

    //private void StartProcessing()
    //{
    //    animator.SetBool("isWorking", true);
    //    data.itemSlot.Copy(GameManager.instance.itemDragAndDropController.itemSlot);
    //    data.itemSlot.count = 1;

    //    //GameManager.instance.itemDragAndDropController.RemoveItem();

    //    GameManager.instance.inventory.Remove(data.itemSlot.item);
        
    //    data.timer = timeToProcess;
    //}

    private void CompleteConversion()
    {
        animator.SetBool("isWorking", false);
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        spawner.SpawnItem(
            new Vector3(transform.position.x + width / 2f + UnityEngine.Random.Range(0.4f, 0.8f), transform.position.y + UnityEngine.Random.Range(-0.2f, 0.2f), transform.position.z),
            itemResult.item,
            itemResult.count
            );
        //data.itemSlot.Clear();
        //data.itemSlot.Set(itemResult, itemResultCount);
        //data.itemSlot.Set(itemResult, itemResultCount);

        //// Update newly created output immediately 
        //GameManager.instance.inventory.Add(data.itemSlot.item, data.itemSlot.count);
        //data.itemSlot.Clear();
    }

    internal void Convert(ConvertRecipe convertRecipe)
    {
        if (animator.GetBool("isWorking") == true)
            return;

        for (int i = 0; i < convertRecipe.elements.Count; ++i)
        {
            if (inventory.CheckItem(convertRecipe.elements[i]) == false)
            {
                Debug.Log("Not enough ingredients for this craft");
                return;
            }
        }

        HidePanel();
        for (int i = 0; i < convertRecipe.elements.Count; ++i)
        {
            inventory.Remove(convertRecipe.elements[i].item, convertRecipe.elements[i].count);
        }

        itemResult = convertRecipe.output;
        animator.SetBool("isWorking", true);
        timer = convertRecipe.timeToProcess;
    }

    //public string Read()
    //{
    //    return JsonUtility.ToJson(data);
    //}

    //public void Load(string jsonString)
    //{
    //    data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    //}


}
