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
		if (Utilities.checkLayerMask(attackCollisionLayers, col)) {
			// check if enemy is in "dying" transition: do nothing if it is
			if (target.health <= 0) {
				return;
			}

			// trigger enemy stun
			target.stun (stunTime);
			// trigger enemy hit sfx
			col.GetComponent<AudioSource> ().PlayDelayed ((Random.value/30f));	//randomize playtime to avoid stacking
			target.takeDamage (attack);

			// reset enemy velocity
			col.attachedRigidbody.velocity = Vector2.zero;
			// apply force in the right direction
			if (attacker.facingRight) {
				target.takeKnockback (new Vector2 (xForce, yForce));
			} else {
				target.takeKnockback (new Vector2 (-xForce, yForce));
			}
		}


	}
}
