using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoPosition : DogAction{
	Vector3 spherepos;


	Vector3 direction;
	private NavMeshPath path;
	Vector3 currentTarget;
	Vector3 targetPosition;
	float width = 1.5f;
	public GotoPosition(Dog d, Vector3 targetPosition, float width = 1.5f) : base(d){
		this.targetPosition = targetPosition;
		this.width = width;
	}
	public override void StartAction(){
		isDone = false;	
		path = new NavMeshPath ();
		currentTarget = dog.transform.position;
	}
	public override void UpdateAction(){
		NavMesh.CalculatePath (dog.transform.position, targetPosition, NavMesh.AllAreas, path);//DONT BE FALSE OR i :cryinglaughter::gun:
		for (int i = 0; i < path.corners.Length - 1; i++) {
			Debug.DrawLine (path.corners [i], path.corners [i + 1], Color.red, 0.1f);	
		}if (path.corners.Length > 1) {
			direction = (path.corners [1] - dog.transform.position).normalized;
			//direction.y = dog.transform.position.y;
		}
		Vector3 offset = new Vector3 (0, 1, 0);
		Debug.DrawLine (dog.transform.position+offset, dog.transform.position+direction*4+offset, Color.blue);

		if (!isDone){
			Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
			if (Vector2.Distance(dogPos, new Vector2(currentTarget.x, currentTarget.z)) > width){
				navAgent.SetDestination(currentTarget);
			}
			else if(Vector2.Distance(dogPos, new Vector2(targetPosition.x, targetPosition.z))> width){
				GetNewTarget ();
			}else{
				isDone = true;
				EndAction ();
			}
		}
	}
	private void GetNewTarget(){
		float maxForward = 5f;
		if (Vector2.Distance (new Vector2 (dog.transform.position.x, dog.transform.position.z), new Vector2 (targetPosition.x, targetPosition.z))< maxForward) {
			currentTarget = targetPosition;
			return;
		}
		//Temporary values.
		currentTarget = GetPos (2,5,-3,3,4,"Tree");
	}
	Vector3 GetPos(float minForward, float maxForward, float minRight, float maxRight, float radius, string tag){
		Vector3 pos = Vector3.zero;
		Vector3 scanPos = new Vector3(direction.x, 0, direction.z)*Random.Range(minForward, maxForward);
		scanPos += dog.transform.position;
		scanPos += GetPerpendicular2DVector (direction) * Random.Range(minRight, maxRight);
		if(dog.terrain != null)
			scanPos.y = dog.terrain.SampleHeight (scanPos);
		GameObject obj = ScanForObject.Scan (scanPos, radius, tag, dog.dogLayerMask);
		if (obj != null) {
			pos = obj.transform.position;
			//Temporary I guess.
			obj.tag = "Untagged";
		}else {
			pos = scanPos;
		}
		NavMeshHit hit;
		if (NavMesh.SamplePosition (pos, out hit, 2.0f, 1)){
			pos = hit.position;
		}else {
			pos = path.corners [1];
		}
		dog.TestWaypoint.position = (pos);
		return pos;
	}
	private Vector3 GetPerpendicular2DVector(Vector3 v){
		return new Vector3 (v.z, 0, -v.x);
	}
	public override void EndAction(){
		navAgent.ResetPath();
	}
}
