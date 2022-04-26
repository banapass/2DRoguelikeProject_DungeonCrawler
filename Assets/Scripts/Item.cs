using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/New Item",fileName ="New Item")]
abstract public class Item : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public string itemToolTip;
    public Sprite itemSprite;
    public ItemType itemType;
    public enum ItemType
    {
        Weapon,
        Potion
    }
}
