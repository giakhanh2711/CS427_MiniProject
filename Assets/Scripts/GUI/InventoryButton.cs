using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image hightlighImage;
    [SerializeField] TextMeshProUGUI textName;

    int myIndex;
    PanelItem panelItem;

    public void SetItemPanel(PanelItem source)
    {
        panelItem = source;
    }

    public void setIndex(int index)
    {
        myIndex = index;
    }

    public void SetData(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if (slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
            textName.text = slot.item.name;
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void ClearData()
    {
        icon.gameObject.SetActive(false);
        icon.sprite = null;
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        panelItem.OnClick(myIndex);
    }
    
    public void HighlightChosen(bool isChosen)
    {
        hightlighImage.gameObject.SetActive(isChosen);
    }
}
