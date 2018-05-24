using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class InteractSoundTrigger : MonoBehaviour, IInteractible
{
    private StudioEventEmitter see;
    [SerializeField] private int voiceLine;

    private void Start()
    {
        see = GetComponent<StudioEventEmitter>();
    }


    public void AlternateInteract()
    {

    }

    public void Interact()
    {
        if (!see.IsPlaying())
        {
            see.Play();
            see.SetParameter("Voice Line", voiceLine);
        }
    }
}
