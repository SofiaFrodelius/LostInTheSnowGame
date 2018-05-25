using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFetch : DogAction{
    Transform player;
	public WaitForFetch(Dog d, Transform player) : base(d){
        this.player = player;
        importance = Importance.LOW;
	}
	public override void StartAction(){
		isDone = false;
		dog.isWaitingForFetch = true;
		animator.SetBool ("WaitForFetch", true);
	}
	public override void UpdateAction(){
        if (Vector3.Distance(player.position, dog.transform.position) > 5)
            isDone = true;
	}
	public override void EndAction(){
		dog.isWaitingForFetch = false;
		animator.SetBool ("WaitForFetch", false);
		isDone = true;
	}
}
