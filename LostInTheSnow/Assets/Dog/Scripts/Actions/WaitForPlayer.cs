using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WaitForPlayer : DogAction{
    Transform player;
	Vector3 target;
    float distance;
	Bark bark;
	public WaitForPlayer(Dog d, Transform player,Vector3 target, float distance): base(d){
        this.player = player;
		this.target = target;
        this.distance = distance;
		bark = new Bark (d);
    }
	public override void StartAction(){
		isDone = false;
		bark.StartAction ();
	}
    public override void UpdateAction(){
		if (!isDone) {
			if (Vector3.Distance (dog.transform.position, player.position) < distance || Vector3.Distance (player.position, target) < Vector3.Distance (dog.transform.position, target)) {
				isDone = true;
			} else {
				if (!bark.IsDone ())
					bark.UpdateAction ();
				else
					bark.StartAction ();
			}
		}
    }
	public override void EndAction(){
		isDone = true;
	}
}
