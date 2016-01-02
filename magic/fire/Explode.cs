using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	// attack vars
	public LayerMask attackCollisionLayers;
	public int damage = 10;
	public float knockbackStrength = 2;
	public float stunTime = 0.5f;	// in seconds

	// particle effects vars
	public Rigidbody2D fireParticle;
	public int particleCount = 10;
	public float particleVelocity = 10f;
	public float particleLife = 2f;

	// screenshake var
	public float screenshakeStrength = 1f;

	// Use this for initialization
	void Start () {
		spawnParticles ();
		StartCoroutine (disableExplosion(0.05f));	// stops damage detection, forces, etc.
		Utilities.screenShake(screenshakeStrength);			// causes screen shake
		Destroy(this.gameObject, 1f);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		// check that the object is in an attack collision layer
		if (Utilities.checkLayerMask(attackCollisionLayers, col)) {
			CreatureBehavior target = col.GetComponent<CreatureBehavior> ();
			Debug.Log ("HIT!");
			Debug.Log (col.gameObject.layer);
			target.takeDamage (damage);
			target.stun (stunTime);
		}
	}

	void spawnParticles() {
		Vector2 vel;
		for (int i = 0; i <= particleCount; i++) {

			Rigidbody2D p = (Rigidbody2D) Instantiate(fireParticle, transform.position, Quaternion.identity);

			vel = Random.insideUnitCircle * particleVelocity;
			vel.y += 2; // add upward bias
			// set particle direction/speed
			p.GetComponent<Rigidbody2D>().velocity = vel;

			// set destroy time
			Destroy(p.gameObject, particleLife);
		}
	}

	IEnumerator disableExplosion(float time) {
		PointEffector2D effector = this.GetComponent<PointEffector2D> ();
		CircleCollider2D circleCol = this.GetComponent<CircleCollider2D> ();
		yield return new WaitForSeconds (time);

		effector.enabled = false;
		circleCol.enabled = false;
	}
}
