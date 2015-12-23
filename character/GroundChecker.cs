using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

    public float landingThreshold;

	private PlayerController player;
    private Rigidbody2D playerBody;

	// Use this for initialization
	void Start () {
		player = this.GetComponentInParent<PlayerController> ();
        playerBody = this.GetComponentInParent<Rigidbody2D>();
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

	// double check on ground (not needed)
//	void OnTriggerStay2D(Collider2D col) {
//		//player.onGround = true;
//	}

	void OnTriggerExit2D(Collider2D col) {
		player.onGround = false;
		player.onFirstJump = true;
	}
}
