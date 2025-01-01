using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Dropper", menuName = "Item Dropper")]
public class ItemDropperData : ScriptableObject
{
    public List<ItemDrop> drops;
}
