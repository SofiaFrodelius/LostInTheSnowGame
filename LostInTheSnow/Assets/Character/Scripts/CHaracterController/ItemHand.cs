﻿using System.Collections;
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
    private float t = 0;
    [SerializeField] private float timeToSwitch;

    public void Start()
    {
        inventory = Inventory.instance;
        if (inventory != null)
            inventory.updateItemInHandCallback += updateItemInHand;
    }



    public void Update()
    {
        //tempkod. i hope.
        if (timeToSwitch <= Time.time - t)
        {
            if (Input.GetAxis("Scroll") > 0.1)
            {
                scroll = 1;
                t = Time.time;
            }
            else if (Input.GetAxis("Scroll") < -0.1)
            {
                scroll = -1;
                t = Time.time;
            }
            else if (Input.GetButtonDown("NextItem"))
            {
                scroll = 1;
                t = Time.time;
            }
            else if (Input.GetButtonDown("PreviousItem"))
            {
                scroll = -1;
                t = Time.time;
            }
            else scroll = 0;


        }

        else scroll = 0;
        //------------------



        if (scroll != 0 && inventory != null && inventory.getNumOfUsedHoldableSlots() > 1)
        {
            selectedItem += scroll;
            if (selectedItem >= inventory.getNumOfUsedHoldableSlots()) selectedItem = 0;
            else if (selectedItem < 0) selectedItem = inventory.getNumOfUsedHoldableSlots() - 1;// -1 to not be out of range in list because list starts at 0 getnumofholdableslots starts at 1
            inventory.updateItemInHandCallback.Invoke();
        }
    }

    public void updateItemInHand()
    {
        if (selectedItem == inventory.getNumOfUsedHoldableSlots() && selectedItem > 0)
        {
            selectedItem--;
        }

        GameObject itemToInstansiate;
        if (inventory.getNumOfUsedHoldableSlots() > 0)
            itemToInstansiate = inventory.getObjectFromHoldableSlot(selectedItem);
        else itemToInstansiate = null;


        if (itemToInstansiate == null) activeItem = null;
        else if (activeItem != itemToInstansiate)
        {
            Collider tempCol;
            if (activeItem != null && activeItem.transform.parent != null) Destroy(activeItem);

            activeItem = Instantiate(itemToInstansiate, transform);
            activeItem.layer = currentItemLayerValue;
            tempCol = activeItem.GetComponent<Collider>();
            if (tempCol) tempCol.enabled = false;
            updateAllChildLayers(activeItem.gameObject);

        }
        if (inventory.holdableItemsChangedCallback != null) //if hud exists
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


    public int getSelectedItem()
    {
        return selectedItem;
    }
	//Emil did this might be wrong
	public Item GetItemInHand(){
		return Inventory.instance.getItemFromHoldableSlot (selectedItem);
	}
    public void IncrementSelectedItem()
    {
        selectedItem++;
    }


}
