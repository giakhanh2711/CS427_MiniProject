using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;
    public ToolAction onTilemapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public bool isIconHighlight;
    public GameObject itemPrefab; // To instantiate and place
    public bool isWeapon;
    public int damageAmount = 10;
    public int price = 100;
    public bool canBeSold = true;
}
