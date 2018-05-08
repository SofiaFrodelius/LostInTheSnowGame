using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffForTree : DogAction {
	GameObject tree;
	int actionCount;
	List<DogAction> actions = new List<DogAction>();
	DogAction currentAction;
	public SniffForTree(Dog d): base(d){
		actionDelay = 2;
		moodState.ChangeMood (50f,0f,30f,0f);
		moodEffect.ChangeMood (5f,5f,5f,0f);
	}
	public override void StartAction(){	
		isDone = false;
		actionTimer = actionDelay;
		tree = ScanForObject.Scan (dog.transform.position, 55f, "Tree", dog.dogLayerMask); 
		if (tree != null) {
			actions.Add (new GotoPosition (dog, tree.transform.position + dog.transform.right));
			NextAction ();
		} else 
			isDone = true;
	}
	public override void UpdateAction(){
		if (!isDone) {
			if (currentAction.IsDone ())
				NextAction ();
			currentAction.UpdateAction ();
		}
	}
	public override void EndAction(){
		dog.AddEffectToMood (moodEffect);
		if(currentAction != null)
			currentAction.EndAction ();
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
