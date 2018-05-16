using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDog : DogAction {
	DogTimer timer;
	public PickupDog(Dog d): base(d){
		importance = Importance.HIGH;
	}
	public override void StartAction(){	
		isDone = false;
		animator.SetTrigger ("Pickup");
		timer = new DogTimer (8f);
	}
	public override void UpdateAction(){
		if (!timer.IsDone ()) {
			timer.AddTime (Time.deltaTime);
		} else {
			animator.SetTrigger ("BreakLoose");
			isDone = true;
		}

	}
	public override void EndAction(){
		isDone = true;
	}
}
