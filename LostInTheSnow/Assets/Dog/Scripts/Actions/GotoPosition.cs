using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoPosition : DogAction{
	Vector3 position;
	float width = 1.5f;
	public GotoPosition(Dog d, Vector3 position, float width = 1.5f) : base(d){
		this.position = position;
		this.width = width;
	}
	public override void StartAction(){
		isDone = false;	
	}
	public override void UpdateAction(){
		if (!isDone){
			Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
			if (Vector2.Distance(dogPos, new Vector2(position.x,position.z)) > width){
				if (dog.terrain != null)
					position.y = dog.terrain.SampleHeight (position);
				navAgent.SetDestination(position);
			}
			else{
				isDone = true;
				EndAction ();
			}
		}
	}
	public override void EndAction(){
		navAgent.ResetPath();
	}
	public void SetPosition(Vector3 pos){
		position = pos;
		StartAction ();
	}
}
