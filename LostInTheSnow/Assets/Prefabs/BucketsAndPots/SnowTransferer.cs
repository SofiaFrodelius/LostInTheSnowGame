using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTransferer : MonoBehaviour, IUsable {
    Camera playerCam;
    Inventory inventory;
    [SerializeField] private float useRange;
    [SerializeField] private GameObject filledPlacedPot;
    [SerializeField] private Item emptyBucket;

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
            if (hit.transform.tag == "Stove" )
            {

                Transform tmpPos = hit.transform.Find("PotLocation");
                if(tmpPos.childCount > 0)
                {
                    GameObject oldPot = tmpPos.GetChild(0).gameObject;
                    Destroy(oldPot);
                    inventory.replaceHoldableItem(ih.getSelectedItem(), emptyBucket);

                    GameObject tmpPot = Instantiate(filledPlacedPot, tmpPos);
                    tmpPot.name = filledPlacedPot.name;
                }

            }
            else if( hit.transform.name == "potEmtyPickUpeble")
            {
                Transform tmpPos;
                if (hit.transform.parent.name == "PotLocation")
                {
                    tmpPos = hit.transform.parent;
                    Destroy(hit.transform.gameObject);
                    inventory.replaceHoldableItem(ih.getSelectedItem(), emptyBucket);

                    GameObject tmpPot = Instantiate(filledPlacedPot, tmpPos);
                    tmpPot.name = filledPlacedPot.name;
                }
                
            }
        }

    }

}
