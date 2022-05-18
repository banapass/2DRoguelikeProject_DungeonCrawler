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
    public static bool CheckEmptySlot()
    {
        bool isEmpty = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemSlot == null)
            {
                isEmpty = true;
            }
        }
        return isEmpty;
    }
    public static void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemSlot == null)
            {
                slots[i].ItemSlot = item;
                break;
            }
        }

    }
}
