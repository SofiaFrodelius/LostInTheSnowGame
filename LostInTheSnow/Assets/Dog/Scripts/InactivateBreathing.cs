using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateBreathing : StateMachineBehaviour {
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetLayerWeight (4, 0);
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetLayerWeight (4, 0);
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetLayerWeight (4, 1);
	}
}
