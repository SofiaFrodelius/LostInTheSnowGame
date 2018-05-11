using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialDisabler : MonoBehaviour
{
    [SerializeField]
    private int petIlaTutorialID, callIlaTutorialID, pickUpIlaTutorialID;


    public float maxInteractLength = 4f;
    public LayerMask interactLayerMask;
    Tutorial tutorial;

    private void Start()
    {
        tutorial = Tutorial.instance;
    }

    private void Update()
    {
        //if(Input.GetButtonDown("Interact"))
        //{
        //    RaycastHit hit = new RaycastHit();
        //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        //    if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
        //    {
        //        Debug.Log("Hit: " + hit.transform.name);
        //        if(hit.transform.tag == "Axe")
        //            tutorial.finishTutorial(interactTutorialID);
        //    }
        //}

        if (Input.GetButtonDown("PetDog"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                Debug.Log("finishedtutorial1");
                if (hit.transform.tag == "Dog")
                {
                    tutorial.finishTutorial(petIlaTutorialID);
                }
            }
        }

        else if (Input.GetButtonDown("CallDog"))
        {
            tutorial.finishTutorial(callIlaTutorialID);
        }

        if (Input.GetButtonDown("PickupDog"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                if (hit.transform.tag == "Dog")
                {
                    tutorial.finishTutorial(pickUpIlaTutorialID);
                }
            }
        }
    }
}
