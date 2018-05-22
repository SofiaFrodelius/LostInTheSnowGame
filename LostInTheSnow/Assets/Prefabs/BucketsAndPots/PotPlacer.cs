using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotPlacer : MonoBehaviour, IUsable {

    Camera playerCam;
    Inventory inventory;
    [SerializeField] private float useRange;
    [SerializeField] private GameObject filledPlacedPot;
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
            if (hit.transform.tag == "Stove")
            {
                print("Place On Stove");
                Transform tmpPos = hit.transform.Find("PotLocation");
                GameObject tmpPot = Instantiate(filledPlacedPot, tmpPos);
                tmpPot.name = filledPlacedPot.name;
                inventory.removeHoldableItem(ih.getSelectedItem());
                Destroy(gameObject);



            }
            else
            {
                print("Aint allowed to Place Item Here");
            }
        }

    }
}
