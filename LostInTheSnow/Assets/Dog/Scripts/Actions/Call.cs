using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : DogAction {
	Transform player;
	DogAction currentAction;
	public Call(Dog d, Transform player, bool staticPlayer = false) : base(d){
		this.player = player;
		importance = Importance.LOW;
		if(!staticPlayer)
			currentAction = new FollowTarget (dog, player, Vector3.zero, true, 3f);
		else
			currentAction = new GotoStaticPosition(dog, player.position, player.forward);
	}
	public override void StartAction(){
		isDone = false;
		currentAction.StartAction ();
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (!currentAction.IsDone ()) 
				currentAction.UpdateAction ();
			else
				isDone = true;
		}
	}
	public override void EndAction(){
		currentAction.EndAction ();
		isDone = true;
	}
}
