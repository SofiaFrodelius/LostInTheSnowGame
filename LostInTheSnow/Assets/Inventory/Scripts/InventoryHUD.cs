using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryHUD : MonoBehaviour
{
    public static InventoryHUD instance;
    Inventory inventory;
    public InventorySlotHUD[] inventorySlots;
    private HoldableInventorySlotHUD[] holdableInventorySlots;


    public InventoryHUD()
    {
        if(instance != null)
        {
            //Debug.LogWarning("More than one instance of class InventoryHUD in scene.");
            return;
        }
        instance = this;
    }



    public void Start()
    {
        inventorySlots = GetComponentsInChildren<InventorySlotHUD>();
        holdableInventorySlots = GetComponentsInChildren<HoldableInventorySlotHUD>();
        inventory = Inventory.instance;
        if (inventory != null)
        {
            inventory.inventoryChangedCallback += updateInventoryHUD;
            inventory.holdableItemsChangedCallback += updateHoldableItemsHUD;
        }
        updateHoldableItemsHUD(0); //0 is selected item at the start
        updateInventoryHUD();
    }


    public void Update()
    {
    }


    public void updateInventoryHUD()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Item current = inventory.getItemFromSlot(i);
            if (current != null)
            {
                inventorySlots[i].updateSlot(current, inventory.getNumOfItemsInSlot(i));
            }
            else if(current == null)
            {
                inventorySlots[i].resetSlot();
            }
        }
    }

    public void updateHoldableItemsHUD(int sItem)
    {
        int temp = sItem;
        temp -= 1;
        if (temp < 0) temp = inventory.getNumOfUsedHoldableSlots()-1;
        for(int i = 0; i < holdableInventorySlots.Length; i++)
        {
            Item current;
            if(inventory.getNumOfUsedHoldableSlots() > 0)
            {
                current = inventory.getItemFromHoldableSlot(temp);
                holdableInventorySlots[i].updateSlot(current, temp == sItem);
            }
            else
            {
                holdableInventorySlots[i].updateSlot(null, temp == sItem);
            }

            temp++;
            if (temp >= inventory.getNumOfUsedHoldableSlots()) temp = 0;
        }
    }

    //denna funktionen har en magisk bugg.. rör inte skiten
    //public void showInventory()
    //{
    //    for (int i = 0; i < inventorySlots.Length; i++)
    //    {
    //        if (inventorySlots[i].getCurrentItem() != null)
    //        {
    //            inventorySlots[i].showInventorySlot();
    //        }
    //        else Debug.Log(inventorySlots[i].getCurrentItem());
    //    }
    //}
}
