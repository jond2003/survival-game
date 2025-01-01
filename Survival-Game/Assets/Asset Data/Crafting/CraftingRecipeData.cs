using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipeData : ScriptableObject
{
    public List<InventorySlot> ingredients;
    public InventorySlot craftedItem;
}
