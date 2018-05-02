using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogLocomotion : MonoBehaviour {
	[Header("Locomotion Settings")]
	[Range(0,1f)]
	public float directionResponseTime = 0.2f;
	[Range(0,1f)]
	public float speedDampTime = 0.1f;
	[Range(0,1f)]
	public float angularSpeedDampTime = 0.25f;

	private int speedId;
	private int angularSpeedId;
	private int directionId;

	private bool update = true;

	private NavMeshAgent navAgent;
	private Animator animator;
	void Start () {
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		navAgent.updateRotation = false;

		//Get Ids of animator parameters.
		speedId = Animator.StringToHash("Speed");
		angularSpeedId = Animator.StringToHash("AngularSpeed");
		directionId = Animator.StringToHash("Direction");
	}
	void Update(){
		SetupAgentLocomotion();
	}
	void SetupAgentLocomotion(){
		if (NavAgentDone ()) {
			SetParameters (0, 0);
		} else {
			//Hämtar hastigheten med navagentens desirededVelocitys längd.
			float speed = navAgent.desiredVelocity.magnitude;
			//Hämtar inversen av rotationen mutliplicerat av desiredVelocity
			Vector3 velocity = Quaternion.Inverse (transform.rotation) * navAgent.desiredVelocity;
			//Beräknar vinkeln mot velocityn i grader.
			float angle = Mathf.Atan2 (velocity.x, velocity.z) * 180.0f / 3.14159f;
			//Sets Mecanim Animator Parameters
			if (update) 
				SetParameters (speed, angle);
		}
	}
	public void StartIdleTurn(){
		update = false;
	}
	public void StopIdleTurn(){
		update = true;
	}
	void OnAnimatorMove(){
		navAgent.velocity = animator.deltaPosition / Time.deltaTime;
		transform.rotation = animator.rootRotation;
	}
	void SetParameters(float speed, float direction){
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		bool inTransition = animator.IsInTransition(0);
		bool inIdle = state.IsName("Locomotion.Idle");
		bool inBasicWalk = state.IsName("Locomotion.WalkRun");

		//float speedDampTime = inIdle ? 0 : speedDampTime;
		//float angularSpeedDampTime = inBasicWalk || inTransition ? angularSpeedDampTime : 0;
		float directionDampTime = inTransition ? 1000000 : 0;

		float angularSpeed = direction / directionResponseTime;

		animator.SetFloat(speedId, speed, inIdle ? speedDampTime : speedDampTime, Time.deltaTime);
		animator.SetFloat(angularSpeedId, angularSpeed, inBasicWalk || inTransition ? angularSpeedDampTime : 0, Time.deltaTime);
		animator.SetFloat(directionId, direction, directionDampTime, Time.deltaTime);
	}
	bool NavAgentDone(){
		return !navAgent.pathPending && (navAgent.remainingDistance <= navAgent.stoppingDistance);
	}
}
