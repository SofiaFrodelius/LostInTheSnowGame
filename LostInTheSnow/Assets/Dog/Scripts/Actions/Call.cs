using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : DogAction {
	Transform player;
	FollowTarget followTarget;
	Sit sit;
	public Call(Dog d, Transform player) : base(d){
		this.player = player;
		importance = Importance.HIGH;
		followTarget = new FollowTarget (dog, player, Vector3.zero, true, 2.5f);
		sit = new Sit (dog, 1f);
	}
	public override void StartAction(){
		isDone = false;
		followTarget.StartAction ();
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (!followTarget.IsDone ()) 
				followTarget.UpdateAction ();
			else if (!sit.IsDone ())
				sit.UpdateAction ();
			else
				isDone = true;
		}
	}
	public override void EndAction(){
		followTarget.EndAction ();
		isDone = true;
	}
}
