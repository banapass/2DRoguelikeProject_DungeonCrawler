using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public Player target;
    int increaceHp;
    public override void Use()
    {
        target.Healing(increaceHp); ;
    }
}
