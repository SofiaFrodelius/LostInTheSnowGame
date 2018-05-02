using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Walk : DogAction{
	Transform player;
	GotoPosition gotoPosition;
	Vector3 currentPos;
	float radius;
	public Walk(Dog d, Transform player, float radius) : base(d){
		this.player = player;
		this.radius = radius;
		currentPos = Vector3.zero;
		moodState.ChangeMood (80f,50f,50f,50f);
		moodEffect.ChangeMood (10f,0f,0f,-20f);
	}
	public override void StartAction(){
		isDone = false;
		gotoPosition = new GotoPosition(dog, GetRandomPosition(),0.5f);
	}
	public override void UpdateAction(){
		if(!isDone){
			if (gotoPosition.IsDone ())
				gotoPosition.SetPosition (GetRandomPosition ());
			gotoPosition.UpdateAction();
		}

	}
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
		gotoPosition.EndAction ();
		navAgent.speed = dog.GetDefaultSpeed ();
		isDone = true;
	}
	Vector3 GetRandomPosition(){
		Vector3 newPos;
		Vector3 playerPos = player.transform.position;
		do {
			Vector2 rand = Random.insideUnitCircle;
			newPos = new Vector3 (playerPos.x + rand.x * radius,0, playerPos.z + rand.y * radius);
			if(dog.terrain != null)
				newPos.y = dog.terrain.SampleHeight(newPos);
		} while(Vector3.Distance (newPos, currentPos) < 2f);
		currentPos = newPos;
		return newPos;
	}
}
