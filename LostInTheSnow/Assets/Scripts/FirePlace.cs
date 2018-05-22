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
    [SerializeField] private Item wood;
    [SerializeField] private int woodNeeded;
    private bool hasLitten = false;
    private Animator anim;

    [SerializeField] private bool suposedToBrake;

    //sorry johan lägger till lite  här
    Inventory inv;



    public void Start()
    {
        anim = GetComponent<Animator>();
        inv = Inventory.instance;
    }


    public void Interact()
    {

        if (inv && !hasLitten && inv.isItemInInventory(fireStriker) && inv.isItemInInventory(wood, woodNeeded))
        {
            ToggleFire();
            if (suposedToBrake)
            {
                anim.SetBool("IsBroken", true);
            }
            else
            {
                anim.SetTrigger("ChangeState");
                StartCoroutine(doorCloser(timeToLightFire + 2f));
            }
            for (int i = 0; i < woodNeeded; i++)
            {
                inv.removeNonHoldableItem(wood);
            }
        }
        else if (!hasLitten && !inv)
        {
            ToggleFire();
        }
        else
        {
            
        }
        
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
    IEnumerator doorCloser(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("ChangeState");
    }
}
