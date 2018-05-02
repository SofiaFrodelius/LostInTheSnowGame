using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class FollowPlayer : DogAction{
    Transform player;
    FollowTarget followTarget;
	DogTimer timer;
    public FollowPlayer(Dog d, Transform player) : base(d){
    	this.player = player;
		timer = new DogTimer (5F);
		moodState.ChangeMood (80f,50f,50f,50f);
		moodEffect.ChangeMood (10f,0f,0f,-20f);
    }
	public override void StartAction(){
		isDone = false;
		followTarget = new FollowTarget(dog, player, Vector3.zero, false, 3f);
		followTarget.StartAction ();
	}
    public override void UpdateAction(){
		if(!isDone){
	        followTarget.UpdateAction();
			if (!timer.IsDone ())
				timer.AddTime (Time.deltaTime);
			else {
				followTarget.EndAction ();
				isDone = true;
			}
		}
			
    }
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
		followTarget.EndAction ();
		timer.ResetTimer ();
		isDone = true;
	}
}
