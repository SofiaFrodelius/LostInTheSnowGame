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
    float startWidth;
	float width = 1.5f;
    //DogTimer timer;
	public GotoPosition(Dog d, Vector3 targetPosition, float width = 1.5f) : base(d){
		this.targetPosition = targetPosition;
        startWidth = width;
		this.width = width;
	}
	public override void StartAction(){
		isDone = false;	
		path = new NavMeshPath ();
		currentTarget = dog.transform.position;
        //timer = new DogTimer(Vector3.Distance(dog.transform.position, targetPosition) * dog.waypointMultiplierPerMeter);
        //timer.ResetTimer();
    }
	public override void UpdateAction(){
        Debug.Log(navAgent.path.status);
		if(NavMesh.CalculatePath (dog.transform.position, targetPosition, NavMesh.AllAreas, path)){//DONT BE FALSE OR i :cryinglaughter::gun:
		}
		for (int i = 0; i < path.corners.Length - 1; i++) {
			Debug.DrawLine (path.corners [i], path.corners [i + 1], Color.red, 0.1f);	
		}if (path.corners.Length > 1) {
			direction = (path.corners [1] - dog.transform.position).normalized;
		}
		Vector3 offset = new Vector3 (0, 1, 0);
		Debug.DrawLine (dog.transform.position+offset, dog.transform.position+direction*4+offset, Color.blue);

		if (!isDone){
			Vector2 dogPos = new Vector2 (dog.transform.position.x, dog.transform.position.z);
			if (Vector2.Distance(dogPos, new Vector2(currentTarget.x, currentTarget.z)) > width && navAgent.path.status == NavMeshPathStatus.PathComplete){
		        navAgent.SetDestination(currentTarget);
			}else if(Vector2.Distance(dogPos, new Vector2(targetPosition.x, targetPosition.z))> width){
				dog.isSniffing = false;
				GetNewTarget ();
			}else{
				isDone = true;
				EndAction ();
			}
            //timer.AddTime(Time.deltaTime);
		}
	}
	private void GetNewTarget(){
		float maxForward = 20f;
		if (Vector2.Distance (new Vector2 (dog.transform.position.x, dog.transform.position.z), new Vector2 (targetPosition.x, targetPosition.z))< maxForward) {
            width = 1.5f;
			currentTarget = targetPosition;
			return;
		}

		NavMeshHit hit;
        if (Vector3.Distance(dog.transform.position, dog.player.transform.position) < 40){
            NavMesh.SamplePosition(GetPos(20, 30, -10, 10, 5, "Tree"), out hit, 2, NavMesh.AllAreas);
            currentTarget = hit.position;
        }else {
            currentTarget = dog.player.transform.position;
        }
	}
	Vector3 GetPos(float minForward, float maxForward, float minRight, float maxRight, float radius, string tag){
        width = startWidth;
		Vector3 pos = Vector3.zero;
		Vector3 scanPos = new Vector3(direction.x, 0, direction.z)*Random.Range(minForward, maxForward);
		scanPos += dog.transform.position;
		scanPos += GetPerpendicular2DVector (direction) * Random.Range(minRight, maxRight);
		if(dog.terrain != null)
			scanPos.y = dog.terrain.SampleHeight (scanPos);
		GameObject obj = ScanForObject.Scan (scanPos, radius, tag, dog.dogLayerMask);
		if (obj != null) {
			pos = obj.transform.position;
			if (Random.Range (0, 100/dog.sniffPercent) == 0)
				dog.isSniffing = true;
            //Temporary I guess.
            width = 1.5f;
			obj.tag = "Untagged";
		}else {
			pos = scanPos;
		}
		NavMeshHit hit;
		if (NavMesh.SamplePosition (pos, out hit, 3.0f, 1)){
			pos = hit.position;
		}else {
			pos = path.corners [1];
		}
		//dog.TestWaypoint.position = (pos);
		return pos;
	}
	private Vector3 GetPerpendicular2DVector(Vector3 v){
		return new Vector3 (v.z, 0, -v.x);
	}
	public override void EndAction(){
		navAgent.ResetPath();
	}
}
