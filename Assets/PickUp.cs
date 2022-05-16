using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Item Item
    {
        set
        {
            item = value;
            if (item != null)
            {
                spriteRenderer.sprite = item.itemSprite;
            }
        }
    }
}
