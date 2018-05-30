using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : DogAction {
	bool isStarted = false;
	public Pet(Dog d): base(d){
		importance = Importance.MEDIUM;
	}
	public override void StartAction(){	
		isDone = false;
		isStarted = false;
		animator.SetTrigger ("Pet");
	}
	public override void UpdateAction(){
		if (!isStarted) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Pet"))
				isStarted = true;
		} else {
			if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.99f) 
				isDone = true;
		}
	
	}
	public override void EndAction(){
		isDone = true;
	}
}
