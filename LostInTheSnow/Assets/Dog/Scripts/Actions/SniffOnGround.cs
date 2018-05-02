using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffOnGround : DogAction {
	DogAction currentAction;
	public SniffOnGround(Dog d): base(d){
		actionDelay = 2;
		moodState.ChangeMood (50f,40f,40f,0f);
		moodEffect.ChangeMood (10f,5f,-5f,0f);
	}
	public override void StartAction(){	
		isDone = false;
		actionTimer = actionDelay;
		currentAction = new GotoPosition (dog, GetRandomTarget (), 0.5f);
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (currentAction.IsDone ()) {
				currentAction = new GotoPosition (dog, GetRandomTarget (), 0.5f);
			}
			currentAction.UpdateAction ();
		}
	}
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
	}
	Vector3 GetRandomTarget(){
		return dog.transform.forward * Random.Range (2f, 5f) + dog.transform.right * Random.Range (-0.2f, 0.2f);	
	}
}
