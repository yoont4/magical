using UnityEngine;
using System.Collections;

public class AttackKnockback : MonoBehaviour {

	private LayerMask attackCollisionLayers;
	private CreatureBehavior attacker;
	public float stunTime;
	public float xForce;
	public float yForce;

	public int attack;

	// Use this for initialization
	void Start () {
		attacker = this.GetComponentInParent<CreatureBehavior> ();
		attackCollisionLayers = attacker.attackCollisionLayers;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		CreatureBehavior target = col.GetComponent<CreatureBehavior> ();

		// check if enemy is in "dying" transition: do nothing if it is
		if (target.health <= 0) {
			return;
		}

		// check if the colliding layer matches any of the enemy layers
		int colBit = 1<<col.gameObject.layer;
		int overlap = attackCollisionLayers.value & colBit;
		// if the overlap (&) is positive, then the target layer matches some layer in the attack layers
		if (overlap > 0) {
			// trigger enemy stun
			target.stun (stunTime);
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

		col.GetComponent<CreatureBehavior> ().takeDamage (10);
	}
}
