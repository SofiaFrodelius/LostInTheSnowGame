using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedDog : MonoBehaviour {
	public float maxDistance = 10f;
	public List<GameObject> wayPoints;

	public Dog dog;
	public DogAI dogAI;
	private List<DogAction> actions = new List<DogAction>();

	private int actionCount = 0;
	void Start () {
		dog = GetComponent<Dog> ();
		dogAI = GetComponent<DogAI> ();
		for (int i = 0; i < wayPoints.Count; i++) {
			Vector3 waypoint = new Vector3(wayPoints[i].transform.position.x, 0, wayPoints[i].transform.position.z);
			waypoint.y = Terrain.activeTerrain.SampleHeight (new Vector2(waypoint.x, waypoint.z));
			actions.Add (new LeadPlayer (dog, dog.player, waypoint, maxDistance, false));
		}
		NextAction ();
	}
	void Update () {
		if (dog.currentAction != null) {
			if (dog.currentAction.IsDone ()) {
				if (actionCount < wayPoints.Count)
					NextAction ();
				else {
					Destroy (this);
				}
			}
		} else {
			if (actionCount < wayPoints.Count)
				NextAction ();
			else {
				Destroy (this);
			}
		}
	}
	void NextAction(){
		if(dog.currentAction != null)
			dog.currentAction.EndAction ();
		dog.currentAction = actions [actionCount];
		dog.currentAction.StartAction ();
		actionCount++;
	}
}
