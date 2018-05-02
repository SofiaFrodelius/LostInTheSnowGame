using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHand : MonoBehaviour
{
    private int selectedItem = 0;
    private int numOfItemsInHand = 0;
    private int scroll = 0;
    private GameObject activeItem = null;
    Inventory inventory;
    [SerializeField]
    private int currentItemLayerValue;

    public void Start()
    {
        inventory = Inventory.instance;
        if(inventory != null)
            inventory.updateItemInHandCallback += updateItemInHand;
    }
    
    

    public void Update()
    {
        if (Input.GetAxis("Scroll") > 0.1) scroll = 1;
        else if (Input.GetAxis("Scroll") < -0.1) scroll = -1;
        else scroll = 0;

        if (scroll != 0 && inventory != null && inventory.getNumOfUsedHoldableSlots() > 1)
        {
            selectedItem += scroll;
            if (selectedItem >= inventory.getNumOfUsedHoldableSlots()) selectedItem = 0;
            else if (selectedItem < 0) selectedItem = inventory.getNumOfUsedHoldableSlots() - 1;
            inventory.updateItemInHandCallback.Invoke();
        }
    }

    public void updateItemInHand()
    {
        GameObject itemToInstansiate = inventory.getObjectFromHoldableSlot(selectedItem);
        if (activeItem != itemToInstansiate)
        {
            if (activeItem != null) Destroy(activeItem);

            activeItem = Instantiate(itemToInstansiate, transform);
			activeItem.GetComponent<Rigidbody>().isKinematic = true;
            activeItem.layer = currentItemLayerValue;
            updateAllChildLayers(activeItem.gameObject);

        }
        if(inventory.holdableItemsChangedCallback != null) //if hud exists
            inventory.holdableItemsChangedCallback.Invoke(selectedItem);
    }


    private void updateAllChildLayers(GameObject go)
    {
        go.layer = currentItemLayerValue;

        foreach (Transform child in go.transform)
        {
            updateAllChildLayers(child.gameObject);
        }
        
    }
    public GameObject ActiveItem
    {
        get
        {
            return activeItem;
        }
        set
        {
            activeItem = value;
            
            if (inventory.holdableItemsChangedCallback != null) //if hud exists
                inventory.holdableItemsChangedCallback.Invoke(selectedItem); 
        }
    }

}
