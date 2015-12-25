using UnityEngine;
using System.Collections;

public class AttackKnockback : MonoBehaviour {

	private LayerMask attackCollisionLayers;
	private CreatureBehavior attacker;
	public float stunTime;
	public float xForce;
	public float yForce;

	// Use this for initialization
	void Start () {
		attacker = this.GetComponentInParent<CreatureBehavior> ();
		attackCollisionLayers = attacker.attackCollisionLayers;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		// check if the colliding layer matches any of the enemy layers
		int colBit = 1<<col.gameObject.layer;
		int overlap = attackCollisionLayers.value & colBit;
		if (overlap > 0) {
			// trigger enemy stun
			col.GetComponent<CreatureBehavior> ().stun (stunTime);
			// trigger enemy hit sfx
			col.GetComponent<AudioSource> ().PlayDelayed ((Random.value/30f));	//randomize playtime to avoid stacking

			// reset enemy velocity
			col.attachedRigidbody.velocity = Vector2.zero;
			// apply force in the right direction
			if (attacker.facingRight) {
				col.attachedRigidbody.AddForce (new Vector2 (xForce, yForce), ForceMode2D.Impulse);
			} else {
				col.attachedRigidbody.AddForce (new Vector2 (-xForce, yForce), ForceMode2D.Impulse);
			}
		}
	}
}
