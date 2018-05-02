using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : DogAction {
	public Drop(Dog d) : base(d){
	}
    public override void StartAction(){
		isDone = false;
        dog.DropGrabbedItem();
        isDone = true;
    }
}
