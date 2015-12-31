using UnityEngine;
using System.Collections;

/**
 * Applies damage to a creature when an object that this script is attached
 * to hits it.
 * 
 * 
 * */
public class ImpactDamage : MonoBehaviour {

	public LayerMask effectedLayers;
	public int damageScale;
	public float knockbackScale;
	private Vector2 velocity;
	private Rigidbody2D body;

	void Start() {
		body = this.GetComponent<Rigidbody2D> ();

	}

	void Update() {
		
		velocity = body.velocity;
	}

	void OnCollisionEnter2D(Collision2D col) {
		int colBit = 1 << col.gameObject.layer;
		int overlap = effectedLayers.value & colBit;
		if (overlap > 0) {
			//Vector2 velocity = this.GetComponent<Rigidbody2D> ().velocity;


			// apply damage, stun, and knockback
			CreatureBehavior target = col.gameObject.GetComponent<CreatureBehavior>();
			target.takeDamage (calculateDamage(velocity));
			target.stun (0.2f);
			target.takeKnockback (velocity*knockbackScale);

			// remove script from object once damage has been applied
			Destroy(this);
		}
	}

	int calculateDamage(Vector2 velocity) {
		float speed = velocity.magnitude;
		return (int)Mathf.Sqrt (speed) * damageScale;
	}
}
