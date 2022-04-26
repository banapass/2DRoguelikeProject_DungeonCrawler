using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    [Header("Slot")]
    [SerializeField] Item item;
    [SerializeField] ToolTip toolTip;
    [SerializeField] Image slotImage;
   
    [SerializeField] Player target;
    public Item ItemSlot
    {
        get { return item; }
        set
        {
            item = value;
            if(item != null)
            {
                slotImage.sprite = item.itemSprite;
                SetAlpha(slotImage, 1);
            }
            else
            {
                slotImage.sprite = null;
                SetAlpha(slotImage,0);
            }

        }
    }

    // 캐싱
    RectTransform myRect;
    

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
        slotImage = GetComponent<Image>();
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Use(ItemSlot);
        }    
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemSlot != null)
        {
            float targetSizeX = transform.position.x + myRect.rect.width * 0.5f;
            float targetSizeY = transform.position.y + myRect.rect.height* 0.3f;

            ToolTip.instance.transform.position = new Vector3(targetSizeX, targetSizeY, myRect.position.z);
            SetAlpha(1);

            ToolTip.instance.itemName.text = ItemSlot.itemName;
            ToolTip.instance.toolTip.text= ItemSlot.itemToolTip;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("ExitPointer");
        SetAlpha(0);
    }

    void SetAlpha(Image target, float alpha)
    {
        Color color = target.color;
        color.a = alpha;
        target.color = color;
    }
    void SetAlpha(float alpha)
    {
        ToolTip.instance.GetComponent<CanvasGroup>().alpha = alpha;
    }
    void Use(Item item)
    {
        if (item.itemType == Item.ItemType.Weapon)
        {
            target.Equip(item);
            Debug.Log($"{item.itemName}을(를) 장착했습니다");
        }
        else if(item.itemType == Item.ItemType.Potion)
        {
            Debug.Log($"{item.itemName}을 사용했습니다");
            target.Healing(item);
            ItemSlot = null;
        }
    }

    
}
