using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FirePlace : MonoBehaviour, IInteractible
{

    [SerializeField] private GameObject particleSys;
    [SerializeField] private Transform particlePos;
    [SerializeField] private StudioEventEmitter fireSound;
    [SerializeField] private float timeToLightFire;
    [SerializeField] private Item fireStriker;
    private bool hasLitten = false;

    //sorry johan lägger till lite  här
    Inventory inv;



    public void Start()
    {
        inv = Inventory.instance;
    }


    public void Interact()
    {
       
        if(inv && !hasLitten && inv.isItemInInventory(fireStriker))
        {
            ToggleFire();
        }
        else if(!hasLitten && !inv)
            ToggleFire();
    }
    void ToggleFire()
    {
        hasLitten = true;

        fireSound.gameObject.SetActive(true);        
        StartCoroutine(fireStarter(timeToLightFire));
    }

    IEnumerator fireStarter(float time)
    {
        yield return new WaitForSeconds( time);
        
        GameObject tmpPS = Instantiate(particleSys, particlePos);
        gameObject.GetComponent<FirePlace>().enabled = false;
    }
}
