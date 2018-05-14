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
    private bool hasLitten = false;

    public void Interact()
    {
       
        
        if(!hasLitten)
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
