using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LeadPlayer : DogAction{
    Transform player;
	Vector3 target;
	float width = 1.5f;
	float maxDistance;
	DogAction currentAction;
	bool isWaiting;
	bool waitForPlayerAtTarget;
	public LeadPlayer(Dog d, Transform player, Vector3 target, float maxDistance, bool waitForPlayerAtTarget) : base(d){
        this.player = player;
		this.target = target;
		this.maxDistance = maxDistance;
		this.waitForPlayerAtTarget = waitForPlayerAtTarget;
		importance = Importance.HIGH;
		actionDelay = 1;
		moodState.ChangeMood (50f, 100f, 0f, 50f);
		moodEffect.ChangeMood (5f, 10f, 10f, -5f);
    }
	public override void StartAction(){
		actionTimer = actionDelay;
		isDone = false;
		currentAction = new GotoPosition (dog, target);
		currentAction.StartAction ();
		isWaiting = false;
	}
    public override void UpdateAction(){
		currentAction.UpdateAction ();
		Vector2 playerPos = new Vector2 (player.position.x, player.position.z);
		Vector2 targetPos = new Vector2 (target.x, target.z);
		Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
		if (Vector2.Distance (dogPos, targetPos) < width) {
			if (waitForPlayerAtTarget) {
				if (Vector2.Distance (playerPos, targetPos) < 5f)
					isDone = true;
			} else {
				isDone = true;
			}
		}
		if (Vector2.Distance (playerPos, dogPos) < maxDistance || Vector2.Distance (playerPos, targetPos) < Vector2.Distance (dogPos, targetPos)) {
			if (isWaiting) {
				if (currentAction.IsDone ()) {
					currentAction.EndAction ();
					currentAction = new GotoPosition (dog, target, width);
					currentAction.StartAction ();
					isWaiting = false;
				}
			}
		} else {
			if (Vector2.Distance (playerPos, targetPos) > Vector2.Distance (dogPos, targetPos)) {
				if (!isWaiting) {
					currentAction.EndAction ();
					currentAction = new WaitForPlayer (dog, player,target, maxDistance);
					currentAction.StartAction ();
					isWaiting = true;
				}
			}
		}
    }
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
		currentAction.EndAction ();
		isDone = true;
	}
}
