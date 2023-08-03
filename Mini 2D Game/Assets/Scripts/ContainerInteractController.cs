using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control interaction with any item in a container in the scene
public class ContainerInteractController : MonoBehaviour
{
    Container targetContainer;
    InventoryController inventoryController;
    [SerializeField] ItemInContainerOfObjectPanel containerOfObjectPanel;
    Transform curOpenedChest;
    [SerializeField] float maxDistance = 0.8f;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        if (curOpenedChest != null)
        {
            float distance = Vector2.Distance(curOpenedChest.position, transform.position);
            if (distance > maxDistance)
            {
                curOpenedChest.GetComponent<Chest>().Close(GetComponent<Character>());
            }
        }
    }

    public void Open(Container containerOfObject, Transform _openedChest)
    {
        targetContainer = containerOfObject;
        containerOfObjectPanel.inventory = targetContainer;
        inventoryController.Open();
        containerOfObjectPanel.gameObject.SetActive(true);
        curOpenedChest = _openedChest;
    }

    public void Close()
    {
        inventoryController.Close();
        containerOfObjectPanel.gameObject.SetActive(false);
        curOpenedChest = null;
    }

}
