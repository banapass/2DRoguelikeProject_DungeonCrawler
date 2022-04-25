using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName ="Item/New Item",fileName ="New Item")]
abstract public class Item : MonoBehaviour, IUsable
{
    [Header("Item")]
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemToolTip;
    [SerializeField] protected Player target;
    public ItemType itemType;
    public enum ItemType
    {
        Equipment,
        Potion
    }

    abstract public void Use();
}
