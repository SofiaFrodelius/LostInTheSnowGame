using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LeadPlayer : DogAction{
    Transform player;
	Vector3 target;
	Vector3 endTarget;
	float width = 4f;
	float maxDistance;
	DogAction currentAction;
	bool isWaiting;
	bool waitForPlayerAtTarget;
	public LeadPlayer(Dog d, Transform player, Vector3 target, float maxDistance, bool waitForPlayerAtTarget, Vector3 endTarget) : base(d){
        this.player = player;
		this.target = target;
		this.endTarget = endTarget;
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
		currentAction = new GoStraightToPosition (dog, target, 1-5f);
		currentAction.StartAction ();
		isWaiting = false;
	}
    public override void UpdateAction(){
        if (!isDone){
            currentAction.UpdateAction();
            if (Vector2.Distance(new Vector2(dog.transform.position.x, dog.transform.position.z), new Vector2(target.x, target.z)) < width){
                if (waitForPlayerAtTarget){
                    if (Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(target.x, target.z)) < 5f)
                        isDone = true;
                }else{
                    isDone = true;
                }
            }
            if (!ShouldWait()){
                if (isWaiting){
                    if (currentAction.IsDone()){
                        currentAction.EndAction();
                        currentAction = new GoStraightToPosition(dog, target, 1.5f);
                        currentAction.StartAction();
                        isWaiting = false;
                    }
                }
            }else{
                if (!isWaiting){
                    currentAction.EndAction();
                    currentAction = new WaitForPlayer(dog, player, target, maxDistance);
                    currentAction.StartAction();
                    isWaiting = true;
                }
            }
        }
    }
	private bool ShouldWait(){
		Vector2 playerPos = new Vector2 (player.position.x, player.position.z);
		Vector2 targetPos = new Vector2 (target.x, target.z);
		Vector2 endTargetPos = Vector2.zero;
        if (endTarget != null)
            endTargetPos = new Vector2(endTarget.x, endTarget.z);
		Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
        bool shouldWait = Vector2.Distance(playerPos, dogPos) > maxDistance && Vector2.Distance(playerPos, targetPos) > Vector2.Distance(dogPos, targetPos);
        if (shouldWait)
            shouldWait = (Vector2.Distance (playerPos, endTargetPos) > Vector2.Distance (dogPos, endTargetPos));
        return shouldWait;
	}
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
		currentAction.EndAction ();
		isDone = true;
	}
}
