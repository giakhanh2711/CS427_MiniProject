using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] Container inventory;
    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.FindFreeSpace(recipe.output.item) == false)
        {
            Debug.Log("Inventory is full");
            return;
        }

        for (int i = 0; i < recipe.elements.Count; ++i)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.Log("Not enough ingredients for this craft");
                return;
            }
        }

        for (int i = 0; i < recipe.elements.Count; ++i)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }

        inventory.Add(recipe.output.item, recipe.output.count);
    }
}
