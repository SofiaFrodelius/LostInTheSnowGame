using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pee : DogAction {
	Transform tree;
	public Pee(Dog d, Transform tree): base(d){
		this.tree = tree;
	}
	public override void StartAction(){	
		isDone = false;
		//actions.add(PlayPeeAnimation);
		isDone = true;
	}
	public override void UpdateAction(){
	}
	public override void EndAction(){
		
	}
}
