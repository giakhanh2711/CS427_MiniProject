using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/RecipeList")]
public class RecipeList : ScriptableObject
{
    public List<CraftingRecipe> craftingRecipes;
    public List<CraftingRecipe> unLaunchedRecipes;

    internal void ResetCraftingRecipes()
    {
        foreach (CraftingRecipe recipe in unLaunchedRecipes)
        {
            craftingRecipes.Remove(recipe);
        }
    }

    internal void UpdateKnowRecipes()
    {
        foreach (CraftingRecipe recipe in unLaunchedRecipes)
        {
            if (craftingRecipes.Find(x => x == recipe) == null)
                craftingRecipes.Add(recipe);
        }
    }
}
