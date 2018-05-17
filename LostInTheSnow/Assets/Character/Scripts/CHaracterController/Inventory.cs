using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance; //singleton


    [SerializeField]
    private int numOfSlots = 5;
    [SerializeField]
    private int numOfHoldableSlots = 1;
    private List<InventorySlot> inventorySlots;
    private List<InventorySlot> holdableSlots;
    private int numOfUsedSlots = 0;
    private int numOfUsedHoldableSlots;
    public Item hands;


    public delegate void InventoryChanged();
    public InventoryChanged inventoryChangedCallback;
    public delegate void HoldableItemsChanged(int sItem);
    public HoldableItemsChanged holdableItemsChangedCallback;
    public delegate void UpdateItemInHand();
    public UpdateItemInHand updateItemInHandCallback;


    InventoryHUD inventoryHUD;
    private InventorySaveLoad invSaveLoad;

    private void Awake()
    {
        inventoryHUD = InventoryHUD.instance;

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of class inventory in scene.");
            return;
        }
        instance = this;

    }


    public void Start()
    {
        inventorySlots = new List<InventorySlot>();
        holdableSlots = new List<InventorySlot>();
        numOfHoldableSlots++;
        addItem(hands);

        invSaveLoad = InventorySaveLoad.instance;
        if (invSaveLoad && invSaveLoad.getHasSaved())
        {
            invSaveLoad.loadInventory();
        }
    }


    //temporärkod
    public void Update()
    {
    }

    public Item getItemFromSlot(int slot)
    {
        if (inventorySlots.Count > slot)
            return inventorySlots[slot].getItem();
        else return null;
    }

    public Item getItemFromHoldableSlot(int slot)
    {
        if (holdableSlots.Count > slot)
            return holdableSlots[slot].getItem();
        else return null;
    }

    public int getNumOfItemsInSlot(int slot)
    {
        if (inventorySlots.Count > slot)
            return inventorySlots[slot].getItemsInSlot();
        else return 0;
    }

    public bool addItem(Item item)
    {
        //skriv om så det inte är samma kod 2 gånger om tid finns
        if (!item.getHoldable())
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].getItem() != null && inventorySlots[i].getItem().getId() == item.getId())
                {
                    if (inventorySlots[i].getItemsInSlot() < item.getMaxStack())
                    {
                        inventorySlots[i].incrementItemsInSlot();
                        inventoryChangedCallback.Invoke();
                        return true;
                    }
                    else return false;
                }
            }

            if (inventorySlots.Count == numOfSlots)
            {
                Debug.Log("Inventory is full. Could not add item to inventory.");
                return false;
            }

            if (inventorySlots.Count < numOfSlots)
            {
                inventorySlots.Add(new InventorySlot());
                inventorySlots[inventorySlots.Count - 1].setItem(item);
                inventorySlots[inventorySlots.Count - 1].incrementItemsInSlot();
                if (inventoryChangedCallback != null)
                    inventoryChangedCallback.Invoke();
                return true;
            }
        }

        else
        {
            for (int i = 0; i < holdableSlots.Count; i++)
            {
                if (holdableSlots[i].getItem() != null && holdableSlots[i].getItem().getId() == item.getId())
                {
                    if (holdableSlots[i].getItemsInSlot() < item.getMaxStack())
                    {
                        holdableSlots[i].incrementItemsInSlot();
                        inventoryChangedCallback.Invoke();
                        return true;
                    }
                    else return false;
                }
            }

            if (holdableSlots.Count == numOfSlots)
            {
                Debug.Log("Inventory is full. Could not add item to inventory.");
                return false;
            }

            if (holdableSlots.Count < numOfHoldableSlots)
            {
                holdableSlots.Add(new InventorySlot());
                holdableSlots[holdableSlots.Count - 1].setItem(item);
                holdableSlots[holdableSlots.Count - 1].incrementItemsInSlot();
                if (updateItemInHandCallback != null)
                    updateItemInHandCallback.Invoke();
                return true;
            }
        }
        return false;
    }

    public void removeHoldableItem(int slotId)
    {
        if (getNumOfUsedHoldableSlots() > 0)
        {
            if (holdableSlots[slotId].getItemsInSlot() > 1)
            {
                holdableSlots[slotId].decrementItemsInSlot();
            }
            else
            {
                holdableSlots.RemoveAt(slotId);
            }
            updateItemInHandCallback.Invoke();
        }
    }

    public void replaceHoldableItem(int slotId, Item item)
    {
        if(holdableSlots[slotId].getItemsInSlot() == 1)
        {
            holdableSlots[slotId].setItem(item);
        }
        else
        {
            holdableSlots[slotId].decrementItemsInSlot();
            addItem(item);
        }

        if (updateItemInHandCallback != null)
            updateItemInHandCallback.Invoke();
    }


    public void removeNonHoldableItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].getItem() == item)
            {
                if (inventorySlots[i].getItemsInSlot() > 1)
                {
                    inventorySlots[i].decrementItemsInSlot();
                }
                else
                {
                    inventorySlots.Remove(inventorySlots[i]);
                }
                inventoryChangedCallback.Invoke();
                return;
            }
        }
    }


    public int getNumOfHoldableSlots()
    {
        return numOfHoldableSlots;
    }
    public int getNumOfUsedHoldableSlots()
    {
        return holdableSlots.Count;
    }

    public int getNumOfRegularSlots()
    {
        return numOfSlots;
    }

    public int getNumOfUsedRegularSlots()
    {
        return numOfUsedSlots;
    }


    public GameObject getObjectFromHoldableSlot(int i)
    {
        if (holdableSlots[i].getItem() != null)
            return holdableSlots[i].getItem().getAssociatedGameobject();
        else return null;
    }

    public bool isItemInInventory(Item item, int num = 1)
    {
        if (num < 1) return true;

        if(item.getHoldable())
        {
            for(int i = 0; i < holdableSlots.Count; i++)
            {
                if (holdableSlots[i].getItem() == item && holdableSlots[i].getItemsInSlot() >= num) return true;
                else continue;
            }
        }

        else if (!item.getHoldable())
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].getItem() == item && inventorySlots[i].getItemsInSlot() >= num) return true;
                else continue;
            }
        }

        return false;
    }



    public void loadInventory(List<InventorySlot> HS, List<InventorySlot> NHS, int usedHoldableSlots, int numHoldableSlots, int usedRegularSlots, int numRegularSlots) //HS holdableSlots, NHS nonHoldableSlots
    {
        inventorySlots = NHS;
        holdableSlots = HS;
        numOfUsedHoldableSlots = usedHoldableSlots;
        numOfHoldableSlots = numHoldableSlots;
        numOfUsedSlots = numRegularSlots;
        numOfSlots = numRegularSlots;
    }

    public List<InventorySlot> getHoldableSlotsList()
    {
        return holdableSlots;
    }

    public List<InventorySlot> getNonHoldableSlotsList()
    {
        return inventorySlots;
    }

    public void OnDestroy()
    {
        if(invSaveLoad != null) invSaveLoad.saveInventory();
    }

}
