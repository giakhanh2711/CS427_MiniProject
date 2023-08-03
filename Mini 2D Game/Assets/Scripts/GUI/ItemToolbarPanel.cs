using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolbarPanel : PanelItem
{
    [SerializeField] ToolbarController toolbarController;
    int currentSelectedTool;

    private void Start()
    {
        Init();
        toolbarController.onChange += HandleHighlight;
        HandleHighlight(0);
    }

    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        HandleHighlight(id);
        toolbarController.UpdateHighlightIcon(id);
    }

    public void HandleHighlight(int id)
    {
        buttons[currentSelectedTool].HighlightChosen(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].HighlightChosen(true);
    }

    public override void LoadAndShow()
    {
        base.LoadAndShow();
        toolbarController.UpdateHighlightIcon();
    }
}
