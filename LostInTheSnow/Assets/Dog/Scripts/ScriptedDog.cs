using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedDog : MonoBehaviour {
	public float maxDistance = 10f;
	public float sitTimer = 5f;
	public List<GameObject> wayPoints;

	private Dog dog;
	private DogAI dogAI;
	private List<DogAction> actions = new List<DogAction>();

	private int actionCount = 0;
	void Start () {
		dog = GetComponent<Dog> ();
		dogAI = GetComponent<DogAI> ();
        for (int i = 0; i < wayPoints.Count; i++) {
            if (wayPoints[i] != null) {
                if(wayPoints[(i < wayPoints.Count - 1) ? i + 1 : i] != null)
                    actions.Add(new LeadPlayer(dog, dog.player, wayPoints[i].transform.position, maxDistance, false, wayPoints[(i < wayPoints.Count - 1) ? i + 1 : i].transform.position));
                else
                    actions.Add(new LeadPlayer(dog, dog.player, wayPoints[i].transform.position, maxDistance, false, wayPoints[i].transform.position));
            }
            else
				actions.Add (new Sit (dog, sitTimer)); 
		}
		NextAction ();
	}
	void Update () {
		if (dog.currentAction != null && dog.savedAction == null) {
			if (dog.currentAction.IsDone ()) {
				if (actionCount < wayPoints.Count)
					NextAction ();
				else {
					Destroy (this);
				}
			}
		} else if(dog.savedAction == null){
			if (actionCount < wayPoints.Count)
				NextAction ();
			else {
				Destroy (this);
			}
		}
	}
	public void NextAction(){
		dogAI.StartAction (actions [actionCount]);
		actionCount++;
	}
    public void ForceStopSit() {
        if (dog.currentAction != null && dog.currentAction.ToString() == "Sit"){
            dog.GetComponent<Animator>().SetTrigger("StandUp");
            dogAI.StartAction(actions[actionCount]);
            actionCount++;
        }else {
            actionCount++;
            dogAI.StartAction(actions[actionCount]);
            actionCount++;
        }
      
    }
}
