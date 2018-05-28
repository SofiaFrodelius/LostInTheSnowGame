using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Feed : MonoBehaviour, IInteractible
{
    Inventory inv;
    [SerializeField] private Item rabbit;
    private InteractPrompt ip;
    [SerializeField] private StudioEventEmitter dogEatSound;
    [SerializeField] private StudioEventEmitter playerEatSound;


    private void Start()
    {
        inv = Inventory.instance;
        ip = GetComponent<InteractPrompt>();
    }


    public void AlternateInteract()
    {
        if (inv.isItemInInventory(rabbit, 1))
        {
            //feed dog
            inv.removeNonHoldableItem(rabbit);
            ip.updatePrompts();
            Debug.Log("Feeds Dog");
            dogEatSound.Play();
        }
    }

    public void Interact()
    {
        if (inv.isItemInInventory(rabbit, 1))
        {
            //feed player
            inv.removeNonHoldableItem(rabbit);
            ip.updatePrompts();
            Debug.Log("Feeds Player");
            playerEatSound.Play();
        }
    }
}
