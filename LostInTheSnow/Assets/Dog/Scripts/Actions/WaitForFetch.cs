using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFetch : DogAction
{
	public WaitForFetch(Dog d) : base(d){
		importance = Importance.LOW;
	}
	public override void StartAction(){
		isDone = false;
		dog.isWaitingForFetch = true;
		animator.SetBool ("WaitForFetch", true);
	}
	public override void UpdateAction(){
	}
	public override void EndAction(){
		dog.isWaitingForFetch = false;
		animator.SetBool ("WaitForFetch", false);
		isDone = true;
	}
}
