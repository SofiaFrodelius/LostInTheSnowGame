using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDog : DogAction {
	public PickupDog(Dog d): base(d){
		
	}
	public override void StartAction(){	
		isDone = false;
		animator.SetTrigger ("Pickup");
	}
	public override void UpdateAction(){
		
	}
	public override void EndAction(){
		isDone = true;
	}
}
