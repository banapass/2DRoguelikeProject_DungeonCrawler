using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/New Weapon", fileName = "New Weapon")]
public class Weapon : Item
{
    [Header("Weapon")]
    [SerializeField] int increaceAtk;
    public int IncreaceAtk { get { return increaceAtk; } }
    
}
