using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConvertRecipePanel : PanelItem
{
    [SerializeField] List<ConvertRecipe> convertRecipeList;
    //[SerializeField] Crafting crafting;
    [SerializeField] Convertor convertor;

    public override void LoadAndShow()
    {
        for (int i = 0; i < buttons.Count && i < convertRecipeList.Count; ++i)
        {
            buttons[i].SetData(convertRecipeList[i].output);
        }
    }

    public override void OnClick(int id)
    {
        if (id >= convertRecipeList.Count)
            return;

        //if (crafting == null)
        //    crafting = GameObject.Find("Player").GetComponent<Crafting>();

        //crafting.Craft(recipeList.craftingRecipes[id]);
        convertor.Convert(convertRecipeList[id]);
    }

    internal void UpdatePanel(RecipeList smelterRecipe)
    {
        foreach(ConvertRecipe recipe in smelterRecipe.unLaunchedRecipes)
        {
            convertRecipeList.Add(recipe);
        }

        LoadAndShow();
    }
}
