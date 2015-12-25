using UnityEngine;
using System.Collections;

public class AttackKnockback : MonoBehaviour {

	private LayerMask attackCollisionLayers;
	public float stunTime;
	public float xForce;
	public float yForce;

	// Use this for initialization
	void Start () {
		attackCollisionLayers = this.GetComponentInParent<PlayerController> ().attackCollisionLayers;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		// check if the colliding layer matches any of the enemy layers
		int colBit = 1<<col.gameObject.layer;
		int overlap = attackCollisionLayers.value & colBit;
		if (overlap > 0) {
			// trigger enemy stun
			col.GetComponent<enemyBehaviour> ().stun (stunTime);
			// trigger enemy hit sfx
			col.GetComponent<AudioSource> ().Play ();

			// reset enemy velocity
			col.attachedRigidbody.velocity = Vector2.zero;
			// apply force in the right direction
			if (player.facingRight) {
				col.attachedRigidbody.AddForce (new Vector2 (xForce, yForce), ForceMode2D.Impulse);
			} else {
				col.attachedRigidbody.AddForce (new Vector2 (-xForce, yForce), ForceMode2D.Impulse);
			}
		}
	}
}
