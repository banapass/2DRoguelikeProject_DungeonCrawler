using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    [Header("Equipment")]
    int increaceAtk;
    public int IncreaceAtk { get { return increaceAtk; } }
    public override void Use()
    {
        target.Equip(this);
    }
}
