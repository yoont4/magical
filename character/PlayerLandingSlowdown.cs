using UnityEngine;
using System.Collections;

public class PlayerLandingSlowdown : StateMachineBehaviour {
    PlayerController player;
    float originalSpeed;
    float increment = 1f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // slow down the player when the animation starts
        player = animator.GetComponentInParent<PlayerController>();
        originalSpeed = player.speed;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float ramp = (originalSpeed / 50) * increment;
        player.speed = originalSpeed / 2 + ramp;
        increment++;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    // restore the player's original speed when the animation is over
        player.speed = originalSpeed;
        increment = 1f;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
