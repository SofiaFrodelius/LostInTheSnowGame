using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Bark : DogAction{
	GameObject barkEmitter;
    public Bark(Dog d) : base(d){
        
    }
    public override void StartAction(){
        barkEmitter = dog.transform.Find("BarkSoundEmitter").gameObject;
        isDone = false;
        animator.SetTrigger("Bark");
        if (barkEmitter)
        {
            barkEmitter.GetComponent<StudioEventEmitter>().Play();
        }
        else
        {
            Debug.LogWarning("Dog has no GameObject BarkSoundEmitter Child with a fmod_EventStudioEmitter with barkSound");
        }
    }
    public override void UpdateAction(){
		if (!barkEmitter.GetComponent<StudioEventEmitter> ().IsPlaying ()) {
			isDone = true;
		}
    }
    public override void EndAction()
    {
        isDone = true;
    }
}
