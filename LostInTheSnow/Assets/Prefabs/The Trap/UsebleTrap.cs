using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsebleTrap : MonoBehaviour, IUsable{
    Camera playerCam;
    [SerializeField] private float useRange;
    [SerializeField] private float rotationDegrees;
    [SerializeField] private string locationName;
    [SerializeField] private GameObject trapToPlace;
    Inventory inventory;


    private void Start()
    {
        playerCam = Camera.main;
        inventory = Inventory.instance;
    }

    public void Use(ItemHand ih)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if(Physics.Raycast(ray, out hit, useRange))
        {
            print(hit.transform.gameObject);
            if (hit.transform.name == locationName)
            {
				GameObject placedTrap = Instantiate(trapToPlace, hit.point, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                inventory.removeHoldableItem(ih.getSelectedItem());
                Destroy(gameObject);
                Vector3 rotation = new Vector3(0f, playerCam.transform.rotation.eulerAngles.y + rotationDegrees, 0f);
                placedTrap.transform.Rotate(rotation);
           
            }
            else
            {
                print("Aint allowed to put trap here");
            }
        }
        else
        {
            print("Aint allowed to put trap here");
        }
        
    }
    
}
