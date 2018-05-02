using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stare : DogAction {
	DogTimer timer;
	public Stare(Dog d, float time): base(d){
		timer = new DogTimer (time);
	}
	public override void StartAction(){	
		isDone = false;
		//actions.add(PlayPeeAnimation);
	}
	public override void UpdateAction(){
		if (!isDone) {
			timer.AddTime (Time.deltaTime);
			if (timer.IsDone ())
				isDone = true;
		}
	}
	public override void EndAction(){
	}
}
