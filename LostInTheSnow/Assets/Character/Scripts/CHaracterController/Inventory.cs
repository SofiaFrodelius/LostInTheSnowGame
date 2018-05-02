using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance; //singleton


    public int numOfSlots = 5;
    public int numOfHoldableSlots = 1;
    private InventorySlot[] inventorySlots;
    private InventorySlot[] holdableSlots;
    private int numOfUsedSlots = 0;
    private int numOfUsedHoldableSlots;
    public Item testItem; //temp


    public delegate void InventoryChanged();
    public InventoryChanged inventoryChangedCallback;
    public delegate void HoldableItemsChanged(int sItem);
    public HoldableItemsChanged holdableItemsChangedCallback;
    public delegate void UpdateItemInHand();
    public UpdateItemInHand updateItemInHandCallback;


    InventoryHUD inventoryHUD;


    private void Awake()
    {
        inventoryHUD = InventoryHUD.instance;

        if(instance != null)
        {
            Debug.LogWarning("More than one instance of class inventory in scene.");
            return;
        }
        instance = this;
    }


    public void Start()
    {
        inventorySlots = new InventorySlot[numOfSlots];
        holdableSlots = new InventorySlot[numOfHoldableSlots];


        for(int i = 0; i < numOfSlots; i++)
        {
            inventorySlots[i] = new InventorySlot();
        }
        for(int i = 0; i < numOfHoldableSlots; i++)
        {
            holdableSlots[i] = new InventorySlot();
        }

    }


    //temporärkod
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) inventoryHUD.showInventory();
    }





    public Item getItemFromSlot(int slot)
    {
        return inventorySlots[slot].getItem();
    }

    public Item getItemFromHoldableSlot(int slot)
    {
        return holdableSlots[slot].getItem();
    }

    public int getNumOfItemsInSlot(int slot)
    {
        return inventorySlots[slot].getItemsInSlot();
    }

    public void addItem(Item item)
    {

        if (!item.getHoldable())
        {
            for (int i = 0; i < numOfUsedSlots; i++)
            {
                if (inventorySlots[i].getItem() != null && inventorySlots[i].getItem().getId() == item.getId())
                {
                    if (inventorySlots[i].getItemsInSlot() < item.getMaxStack())
                    {
                        inventorySlots[i].incrementItemsInSlot();
                        inventoryChangedCallback.Invoke();
                        return;
                    }
                }
            }

            if (numOfUsedSlots == numOfSlots)
            {
                Debug.Log("Inventory is full. Could not add item to inventory.");
                return;
            }

            else
            {
                inventorySlots[numOfUsedSlots].setItem(item);
                inventorySlots[numOfUsedSlots].incrementItemsInSlot();
                inventoryChangedCallback.Invoke();
                numOfUsedSlots++;
            }
        }

        else if(numOfUsedHoldableSlots < numOfHoldableSlots)
        {
            holdableSlots[numOfUsedHoldableSlots].setItem(item);
            holdableSlots[numOfUsedHoldableSlots].incrementItemsInSlot();
            //holdaBleItemsChangedCallback.Invoke();
            numOfUsedHoldableSlots++;
            if(updateItemInHandCallback != null)
                updateItemInHandCallback.Invoke();
        }


    }


    public int getNumOfHoldableSlots()
    {
        return numOfHoldableSlots;
    }
    public int getNumOfUsedHoldableSlots()
    {
        return numOfUsedHoldableSlots;
    }

    public GameObject getObjectFromHoldableSlot(int i)
    {
        if (holdableSlots[i].getItem() != null)
            return holdableSlots[i].getItem().getAssociatedGameobject();
        else return null;
    }

}
