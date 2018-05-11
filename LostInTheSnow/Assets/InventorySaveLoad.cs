using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaveLoad : MonoBehaviour
{
    private List<InventorySlot> holdableSlots;
    private List<InventorySlot> nonHoldableSlots;
    private int numberOfUsedHoldableSlots;
    private int numberOfUsedNonHoldableSlots;
    private int numberOfHoldableSlots;
    private int numberOfNonHoldableSlots;
    private bool hasSaved = false;

    private Inventory inventory;
    public static InventorySaveLoad instance;


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))saveInventory();
        if (Input.GetKeyDown(KeyCode.P)) loadInventory();
    }



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        inventory = Inventory.instance;
    }



    public void saveInventory()
    {
        holdableSlots = inventory.getHoldableSlotsList();
        nonHoldableSlots = inventory.getNonHoldableSlotsList();
        numberOfUsedHoldableSlots = inventory.getNumOfUsedHoldableSlots();
        numberOfHoldableSlots = inventory.getNumOfHoldableSlots();

        numberOfUsedNonHoldableSlots = inventory.getNumOfUsedRegularSlots();
        numberOfNonHoldableSlots = inventory.getNumOfRegularSlots();
        Debug.Log("Inventory Saved");
        hasSaved = true;
    }

    public void loadInventory()
    {
        Debug.Log("Inventory Loaded");
        inventory = Inventory.instance;
        inventory.loadInventory(holdableSlots, nonHoldableSlots, numberOfUsedHoldableSlots, numberOfHoldableSlots, numberOfUsedNonHoldableSlots, numberOfNonHoldableSlots);
    }



    private void OnLevelWasLoaded(int level)
    {
        inventory = Inventory.instance;
        //loadInventory();
    }

    public bool getHasSaved()
    {
        return hasSaved;
    }

}
