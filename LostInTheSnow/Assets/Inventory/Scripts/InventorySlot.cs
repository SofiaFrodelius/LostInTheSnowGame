using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    private int itemsInSlot = 0;

    public Item getItem()
    {
        return item;
    }

    public void setItem(Item i)
    {
        item = i;
    }

    public int getItemsInSlot()
    {
        return itemsInSlot;
    }

    public void incrementItemsInSlot()
    {
        itemsInSlot++;
    }

    public void decrementItemsInSlot()
    {
        itemsInSlot--;
    }






}
