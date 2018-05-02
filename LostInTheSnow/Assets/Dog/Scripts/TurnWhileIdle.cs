using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWhileIdle : StateMachineBehaviour {
	DogLocomotion dogLocomotion;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		dogLocomotion = animator.GetComponent<DogLocomotion> ();
		dogLocomotion.StartIdleTurn ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.normalizedTime > 0.9f) {
			dogLocomotion.StopIdleTurn ();
		}
	}
}
