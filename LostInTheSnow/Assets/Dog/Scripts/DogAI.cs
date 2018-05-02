using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.Events;

public class DogAI : MonoBehaviour {
	private List<List<DogAction>> actions = new List<List<DogAction>> ();
	private List<DogAction> idleActions = new List<DogAction>();
	private List<DogAction> activeActions = new List<DogAction>();
	private List<DogAction> interactActions = new List<DogAction>();
	private enum ActionType{ IDLE, ACTIVE};
	private Mood bestMood;

	private Dog dog;
	void Start(){
		dog = GetComponent<Dog> ();
		actions.Add (idleActions);
		actions.Add (activeActions);
		actions.Add (interactActions);
		activeActions.Add(new FollowPlayer(dog, dog.player));
		activeActions.Add(new Fetch (dog, dog.player));
		//activeActions.Add(new LeadPlayer(dog, dog.player, dog.TestWaypoint.position, 10f));
		activeActions.Add(new SniffForTree (dog));
		activeActions.Add(new Walk (dog, dog.player, 15f));

		idleActions.Add(new Sit (dog, 1f));
		idleActions.Add(new Stare (dog, 1f));

		interactActions.Add (new Call (dog, dog.player));

		bestMood.ChangeMood (100f, 50f, 75f, 0f);

		//dog.currentAction = new RingARound (dog, dog.player, 5f);
		//dog.currentAction = new Sit(dog, 5f);
		//dog.currentAction.StartAction ();
		//StartAction (ActionType.ACTIVE);

	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.M))
			StartAction (new Sit (dog, -1));
		if (dog.currentAction != null) {
			if (dog.currentAction.IsDone ()) {
				EndAction ();
			} /*else if (!dog.player.GetComponent<CharacterMovement> ().getSprint ()) {//Player.getspeed > walking speed <-- me ny player controller.
				if (dog.currentAction.GetImportance () != DogAction.Importance.HIGH) {
					EndAction ();
					dog.currentAction = activeActions [0];
					dog.currentAction.StartAction ();
				}
			}*/
		}
	}
	public void StartAction(DogAction action){
		EndAction ();
		dog.currentAction = action;
		dog.currentAction.StartAction ();
	}
	private void StartAction(ActionType actionType){
		switch (actionType) {
		case ActionType.IDLE:
			dog.currentAction = idleActions [GetBestActionIndex (bestMood, idleActions)];	
			break;
		case ActionType.ACTIVE:
			dog.currentAction = activeActions [GetBestActionIndex (bestMood, activeActions)];	
			break;
		}
		dog.currentAction.StartAction ();
		foreach (List<DogAction> list in actions) {
			foreach (DogAction action in list) {
				if(action != dog.currentAction)
					action.DecrementActionTimer ();
			}
		}
	}
	private void EndAction(){
		if (dog.currentAction != null) {
			dog.currentAction.EndAction ();
			dog.currentAction = null;
		}
	}
	private int GetBestActionIndex(Mood desiredMood, List<DogAction> actions){
		int bestId = -1;
		float bestScore = 0;
		for (int i = 0; i < actions.Count; i++) {
			if (actions [i].IsReady ()) {
				float score = GetScore (dog.currentMood, desiredMood, actions [i].GetMoodState (), actions [i].GetMoodEffect ());
				if (score > bestScore) {
					bestScore = score;
					bestId = i;
				}
			}
		}
		return bestId;
	}
	private float GetScore(Mood currentMood, Mood desiredMood, Mood actionMood, Mood actionEffect){
		float score = 0;
		score += GetMoodMatchScale (currentMood, actionMood);
		score += GetMoodMatchScale (currentMood + actionEffect, desiredMood);
		return score / 2f;
	}
	private float GetMoodMatchScale(Mood mood1, Mood mood2){
		float moodMutliplier = 10f / 4;
		float scale = 10f;
		scale -= (Mathf.Abs (mood1.happy - mood2.happy) / 100f ) * moodMutliplier;
		scale -= (Mathf.Abs (mood1.inspired - mood2.inspired) / 100f ) * moodMutliplier;
		scale -= (Mathf.Abs (mood1.playful - mood2.playful) / 100f ) * moodMutliplier;
		scale -= (Mathf.Abs (mood1.scared - mood2.scared) / 100f ) * moodMutliplier;
		return scale;
	}
}

[System.Serializable]
public struct Mood{
	[Range(0,100)]
	public float happy;
	[Range(0,100)]
	public float inspired;
	[Range(0,100)]
	public float playful;
	[Range(0,100)]
	public float scared;
	public static Mood operator +(Mood m1, Mood m2){
		Mood m3 = new Mood ();
		m3.happy = Mathf.Clamp (m1.happy + m2.happy, 0f, 100f);
		m3.inspired = Mathf.Clamp (m1.inspired + m2.inspired, 0f, 100f);
		m3.playful = Mathf.Clamp (m1.playful + m2.playful, 0f, 100f);
		m3.scared = Mathf.Clamp (m1.scared + m2.scared, 0f, 100f);
		return m3;
	}
	public static Mood operator -(Mood m1, Mood m2){
		Mood m3 = new Mood ();
		m3.happy = Mathf.Clamp (m1.happy - m2.happy, 0f, 100f);
		m3.inspired = Mathf.Clamp (m1.inspired - m2.inspired, 0f, 100f);
		m3.playful = Mathf.Clamp (m1.playful - m2.playful, 0f, 100f);
		m3.scared = Mathf.Clamp (m1.scared - m2.scared, 0f, 100f);
		return m3;
	}
	public void ChangeMood(float happy, float inspired, float playful, float scared){
		this.happy = happy;
		this.inspired = inspired;
		this.playful = playful;
		this.scared = scared;
	}
}
