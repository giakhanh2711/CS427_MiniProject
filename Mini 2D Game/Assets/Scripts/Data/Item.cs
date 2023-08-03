using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction; // Action làm việc Chẻ item ban đầu ra rồi lượm
    public ToolAction onTilemapAction; // Action làm việc thực hiện thay đổi tilemap
    public ToolAction onItemUsed; // Action làm việc dùng item trong inventory (chỉ là bỏ nó ra khỏi inventory)
    public Crop crop;
    public bool isIconHighlight;
    public GameObject itemPrefab; // To instantiate and place
    public bool isWeapon;
    public int damageAmount = 10;
    public int price = 100;
    public bool canBeSold = true;
}
