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
	public float headLookDampTime = 0.1f;

	private int speedId;
	private int angularSpeedId;
	private int directionId;
	private int lookDirectionId;
	private int lookUpId;

	private bool update = true;
	private Dog dog;
	private NavMeshAgent navAgent;
	private Animator animator;
	void Start () {
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		dog = GetComponent<Dog> ();
		navAgent.updateRotation = false;

		//Get Ids of animator parameters.
		speedId = Animator.StringToHash("Speed");
		directionId = Animator.StringToHash("Direction");
		lookDirectionId = Animator.StringToHash ("LookDirection");
		lookUpId = Animator.StringToHash ("LookUp");
	}
	void Update(){
		SetupSlopeAngleRotation ();
		SetupLookDirection ();
		SetupAgentLocomotion();
		if (dog.isSniffing)
			animator.SetLayerWeight (3, Mathf.Clamp(animator.GetLayerWeight(3)+ Time.deltaTime,0,1));
		else
			animator.SetLayerWeight (3, Mathf.Clamp(animator.GetLayerWeight(3)- Time.deltaTime,0,1));
	}
	void SetupSlopeAngleRotation(){
		RaycastHit hit;
		float angle = 0;
		Ray forwardRay = new Ray (transform.position, transform.forward);
		Ray downwardRay = new Ray (transform.position, -Vector3.up);
		if (Physics.Raycast (forwardRay, out hit,0.5f, dog.dogLayerMask)) {
			angle = 90 - Vector3.Angle (transform.forward, hit.normal);
		}else if(Physics.Raycast(downwardRay, out hit, 1f, dog.dogLayerMask)){
			angle = 90 - Vector3.Angle (transform.forward, hit.normal);
		}
		Vector3 angleVector3 = new Vector3 (angle, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.eulerAngles = angleVector3;//Vector3.Lerp (transform.eulerAngles, angleVector3, Time.deltaTime*10);
	}
	void SetupLookDirection(){
		float distanceToPlayer = Vector2.Distance (new Vector2 (dog.transform.position.x, dog.transform.position.z), new Vector2 (dog.player.position.x, dog.player.position.z));
		float HeightDifferenceToPlayer = dog.player.transform.position.y - dog.transform.position.y;
		float upAngle = Mathf.Atan2 (HeightDifferenceToPlayer, distanceToPlayer) * Mathf.Rad2Deg;

		Vector3 direction = (dog.player.position - dog.transform.position).normalized;
		float forwardAngle = Mathf.Atan2 (dog.transform.forward.x , dog.transform.forward.z) * Mathf.Rad2Deg;
		float playerAngle = Mathf.Atan2 (direction.x , direction.z) * Mathf.Rad2Deg;
		float angle = (playerAngle - forwardAngle);
		if (angle > 90 || angle < -90) {
			animator.SetFloat (lookDirectionId, 0, headLookDampTime, Time.deltaTime);
			animator.SetFloat (lookUpId, 0, headLookDampTime, Time.deltaTime);
		} else {
			animator.SetFloat (lookDirectionId, angle, headLookDampTime, Time.deltaTime);
			animator.SetFloat (lookUpId, upAngle, headLookDampTime, Time.deltaTime);
		}
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
			float angle = Mathf.Atan2 (velocity.x, velocity.z) * Mathf.Rad2Deg;
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
		if (dog.usingRootMotion) {
			navAgent.velocity = animator.deltaPosition / Time.deltaTime;
			transform.rotation = animator.rootRotation;
		}
	}
	void SetParameters(float speed, float direction){
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		bool inTransition = animator.IsInTransition(0);
		bool inIdle = state.IsName("Locomotion.Idle");
		bool inBasicWalk = state.IsName("Locomotion.WalkRun");

		//float speedDampTime = inIdle ? 0 : speedDampTime;
		//float angularSpeedDampTime = inBasicWalk || inTransition ? angularSpeedDampTime : 0;
		float directionDampTime = inTransition ? 1000000 : 0;
		animator.SetFloat(speedId, speed, inIdle ? speedDampTime : speedDampTime, Time.deltaTime);
		animator.SetFloat(directionId, direction, directionDampTime, Time.deltaTime);
	}
	bool NavAgentDone(){
		return !navAgent.pathPending && (navAgent.remainingDistance <= navAgent.stoppingDistance);
	}
}
