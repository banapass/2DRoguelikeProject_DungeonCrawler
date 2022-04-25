using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    [Header("Potion")]
    [SerializeField] int increaceHp;
    private void Start()
    {
        itemToolTip = $"체력을 {increaceHp}만큼 회복합니다";
    }
    public override void Use()
    {
        target.Healing(increaceHp); ;
    }
}
