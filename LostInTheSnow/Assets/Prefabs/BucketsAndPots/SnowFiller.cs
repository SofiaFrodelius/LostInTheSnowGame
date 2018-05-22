using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFiller : MonoBehaviour , IUsable {

    Camera playerCam;
    Inventory inventory;
    [SerializeField] private float useRange;
    [SerializeField] private Item filledHoldeblePot;
    private void Start()
    {
        playerCam = Camera.main;
        inventory = Inventory.instance;
    }

    public void Use(ItemHand ih)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Physics.Raycast(ray, out hit, useRange))
        {
            if (hit.transform.tag == "Terrain")
            {
                //Fill With Snow;
                //Exchange Object
                print("Fill With Snow");

                //inventory.removeHoldableItem(ih.getSelectedItem());
                inventory.replaceHoldableItem(ih.getSelectedItem(), filledHoldeblePot);
                //inventory.addItem(filledHoldeblePot);
                Destroy(gameObject);
                //ih.IncrementSelectedItem();
                
                
            }
            else
            {
                print("Aint allowed to take snow from this Item");
            }
        }

    }

}
