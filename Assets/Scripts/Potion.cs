using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/New Potion", fileName = "New Potion")]
public class Potion : Item
{
    [Header("Potion")]
    [SerializeField] int increaceHp;
    public int IncreaceHp { get { return increaceHp; } }
    
}
