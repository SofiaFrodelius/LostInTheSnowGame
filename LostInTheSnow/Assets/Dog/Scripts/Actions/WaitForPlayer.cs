using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WaitForPlayer : DogAction{
    Transform player;
	Vector3 target;
    float distance;
	public WaitForPlayer(Dog d, Transform player,Vector3 target, float distance): base(d){
        this.player = player;
		this.target = target;
        this.distance = distance;
    }
	public override void StartAction(){
		isDone = false;
	}
    public override void UpdateAction(){
		dog.Print (Vector3.Distance (dog.transform.position, player.position).ToString());
		if (!isDone) {
			if (Vector3.Distance (dog.transform.position, player.position) < distance || Vector3.Distance (player.position, target) < Vector3.Distance (dog.transform.position, target)) {
				//Wait for animation
				isDone = true;
			} else {
				//Look at player
			}
		}
    }
	public override void EndAction(){
		isDone = true;
	}
}
