using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

    public float landingThreshold;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = this.GetComponentInParent<PlayerController> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
        // check if the colliding layer matches any of the ground layers
        int colBit = 1<<col.gameObject.layer;
        int overlap = player.groundLayer.value & colBit;
        if (overlap > 0) {
            player.onGround = true;
		    player.onFirstJump = false;
            
            // only trigger landing animation if player is falling fast enough
            // calculate landing threshold
            if (player.fallSpeed <= -landingThreshold) {
                player.animator.SetTrigger("startLand");
            } 
        }
	}

	// double check on ground (needed in case physics-engine misses enter)
	void OnTriggerStay2D(Collider2D col) {
		// check if the colliding layer matches any of the ground layers
        int colBit = 1<<col.gameObject.layer;
        int overlap = player.groundLayer.value & colBit;
        if (overlap > 0) {
            player.onGround = true;
            player.onFirstJump = false;
        }
	}

	void OnTriggerExit2D(Collider2D col) {
         // check if the colliding layer matches any of the ground layers
        int colBit = 1<<col.gameObject.layer;
        int overlap = player.groundLayer.value & colBit;
        if (overlap > 0) {
            player.onGround = false;
            player.onFirstJump = true;
        }
	}
}
