using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldableInventorySlotHUD : MonoBehaviour
{
    private Item currentItem;
    [SerializeField]
    private Image itemImage;

    public void Start()
    {

    }


    public void updateSlot(Item item, bool selected)
    {
        if (item != null)
        {
            currentItem = item;
            if (selected)
            {
                itemImage.sprite = item.getSelectedImage();
            }
            else itemImage.sprite = item.getNeutralImage();
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1);
        }
        else
        {
            currentItem = null;
            itemImage.sprite = null;
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0);
        }
    }
}
