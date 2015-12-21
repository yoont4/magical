using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

	private CharacterController player;

	// Use this for initialization
	void Start () {
		player = this.GetComponentInParent<CharacterController> ();
	}


	void OnTriggerEnter2D(Collider2D col) {
		player.onGround = true;
		player.onFirstJump = false;
	}

	// double check on ground (not needed)
//	void OnTriggerStay2D(Collider2D col) {
//		//player.onGround = true;
//	}

	void OnTriggerExit2D(Collider2D col) {
		player.onGround = false;
		player.onFirstJump = true;
	}
}
