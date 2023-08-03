using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControl : MonoBehaviour
{
    CharacterController2D characterController;
    CharacterUseToolsController useToolsController;
    InventoryController inventoryController;
    ToolbarController toolbarController;
    ContainerInteractController containerInteractController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        useToolsController = GetComponent<CharacterUseToolsController>();
        inventoryController = GetComponent<InventoryController>();
        toolbarController = GetComponent<ToolbarController>();
        containerInteractController = GetComponent<ContainerInteractController>();
    }

    public void DisablePlayerControl()
    {
        characterController.enabled = false;
        toolbarController.enabled = false;
        useToolsController.enabled = false;
        inventoryController.enabled = false;
        containerInteractController.enabled = false;
    }

    public void EnablePlayerControl()
    {
        characterController.enabled = true;
        toolbarController.enabled = true;
        useToolsController.enabled = true;
        inventoryController.enabled = true;
        containerInteractController.enabled = true;
    }
}
