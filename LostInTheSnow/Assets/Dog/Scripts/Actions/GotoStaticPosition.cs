using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoStaticPosition : DogAction{
	Vector3 targetPosition;
	Vector3 position;
	Vector3 offset;
	float width = 1.5f;
	public GotoStaticPosition(Dog d, Vector3 position, Vector3 offset, float width = 0.5f) : base(d){
		this.position = position;
		this.width = width;
		this.offset = offset;
	}
	public override void StartAction(){
		isDone = false;	
		targetPosition = position + offset;
		if (dog.terrain != null)
			targetPosition.y = dog.terrain.SampleHeight (targetPosition);
	}
	public override void UpdateAction(){
		if (!isDone){
			Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
			if (Vector2.Distance(dogPos, new Vector2(targetPosition.x,targetPosition.z)) > width){
				navAgent.SetDestination(targetPosition);
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
