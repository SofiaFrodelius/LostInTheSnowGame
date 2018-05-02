using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fetch : DogAction{
    Transform item;
    Transform player;
    DogAction currentAction;
    List<DogAction> actions = new List<DogAction>();
    int actionCount;
    public Fetch(Dog d, Transform player) : base(d){
        this.player = player;
		importance = Importance.MEDIUM;
		actionDelay = 3;
		moodState.ChangeMood (80f, 0f, 100f, 0f);
		moodEffect.ChangeMood (5f, -5f, 5f, -25f);
    }
	public override void StartAction(){
		isDone = false;
		actionTimer = actionDelay;
		actionCount = 0;
		actions.Clear ();
		ScanForObject scan = new ScanForObject (dog, 35f,"Stick", true, dog.dogLayerMask);
		scan.StartAction ();
		if (scan.GetObject () != null) {
			item = scan.GetObject ().transform;
			actions.Add (new FollowTarget (dog, item, Vector3.zero, true, 0.2f));
			actions.Add (new Grab (dog, item.gameObject));
			actions.Add (new FollowTarget (dog, player,player.forward, true, 0.5f));
			actions.Add (new Drop (dog));
			NextAction();
		} else {
			isDone = true;
		}
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (currentAction.IsDone ())
				NextAction ();
			currentAction.UpdateAction ();
		}
    }
	public override void EndAction(){
		actionCount = 0;
		dog.AddEffectToMood (moodEffect);
		isDone = true;
	}
    void NextAction(){
        if (actionCount < actions.Count){
            currentAction = actions[actionCount];
            currentAction.StartAction();
            actionCount++;
        }else
            isDone = true;
    }
}
