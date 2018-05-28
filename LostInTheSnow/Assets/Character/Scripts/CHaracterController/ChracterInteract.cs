using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChracterInteract : MonoBehaviour
{
    [SerializeField] private float maxInteractLength;
    [SerializeField] private LayerMask interactLayerMask;
    private Camera playerCam;
	private Dog dog;
    private bool cutsceneLock = false;
    private bool interactAllowed = true;
    private bool callDogAllowed = true;
    private bool interactDogAllowed = true;
    private bool lastSceneExeptions = false;
    private bool pickUpDogAllowed = true;
    private IlaCallVoiceLinePicker picker;
    // Use this for initialization
    void Start()
    {
        playerCam = Camera.main;
		dog = GameObject.FindWithTag ("Dog").GetComponent<Dog>();
        picker = GetComponent<IlaCallVoiceLinePicker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutsceneLock)
        {
            if (interactAllowed) Interact();
            if (callDogAllowed) CallDog();
            if (interactDogAllowed) InteractWithDog();
            if (pickUpDogAllowed) PickUpDog();
            if (lastSceneExeptions) CallDog();
        }
    }


    void CallDog()
    {
        if (Input.GetButtonDown("CallDog"))
        {
            if (picker)
            {
                picker.PlayVoiceLine(Vector3.Distance(dog.transform.position, transform.position));
            }
            if (!lastSceneExeptions)
                dog.Call ();
        }
    }
    void PickUpDog()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Input.GetButtonDown("PickupDog"))
        {
            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                if (hit.transform.tag == "Dog")
                {
                    Debug.Log(hit.transform.gameObject.name);
                    dog.PickupDog();
                    //HUNDJÄVELN SKA UPP I FAMNHELVETET
                }
            }
        }
    }
    void InteractWithDog()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Input.GetButtonDown("PetDog"))
        {
            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                if (hit.transform.tag == "Dog")
                {
                    Debug.Log(hit.transform.gameObject.name);
                    dog.Pet();
                }
            }
        }
    }
    void Interact()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Input.GetButtonDown("Interact"))
        {
            
            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                ExecuteEvents.ExecuteHierarchy<IInteractible>(hit.transform.gameObject, null, (handler, eventData) => handler.Interact());
                ExecuteEvents.ExecuteHierarchy<IGrabable>(hit.transform.gameObject, null, pickup);
            }
        }
    }

	private void pickup(IGrabable handler, BaseEventData eventData)
    {
        bool temp;
        Inventory inventory;
        inventory = Inventory.instance;
        Item item;
        item = handler.getItemOnPickup();
        if (inventory != null)
        {
            temp = inventory.addItem(item);
            if(temp) handler.destroyItem();
        }
        else handler.destroyItem();
    }

    public bool CutsceneLock
    {
        get { return cutsceneLock; }
        set { cutsceneLock = value; }
    }

    public void PermitAction(int actionIndex, bool value)
    {
        if (actionIndex == 0)
            interactAllowed = value;
        if (actionIndex == 1)
            callDogAllowed = value;
        if (actionIndex == 2)
            interactDogAllowed = value;
        if (actionIndex == 3)
            pickUpDogAllowed = value;
        if (actionIndex == 4)
            lastSceneExeptions = value;

        Debug.Log(interactAllowed);
    }
}