using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Slot[] slots;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        
    }
    public static void CheckSlot(Item item)
    {
        for(int i = 0; i < slots.Length;i++)
        {
            if(slots[i].ItemSlot == null)
            {
                slots[i].ItemSlot = item;
                break;
            }
        }
        
    }
}
