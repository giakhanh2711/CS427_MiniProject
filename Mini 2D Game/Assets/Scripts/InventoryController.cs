using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cho Đóng mở inventory, toolbar, và status panel
public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject belowPanel;
    [SerializeField] GameObject storePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeInHierarchy == false)
                Open();

            else
                Close();
        }
    }

    public void Open()
    {
        inventoryPanel.SetActive(true);
        statusPanel.SetActive(true);
        toolbarPanel.SetActive(false);
        storePanel.SetActive(true);
    }

    public void Close()
    {
        inventoryPanel.SetActive(false);
        statusPanel.SetActive(false);
        toolbarPanel.SetActive(true);
        belowPanel.SetActive(false);
        storePanel.SetActive(false);
    }
}
