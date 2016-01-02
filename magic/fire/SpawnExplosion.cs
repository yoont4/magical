using UnityEngine;
using System.Collections;

public class SpawnExplosion : Magic {
    public Explode explosion;

    public float particleCount;
    public float velocity;
    public bool triggered;

	// Update is called once per frame
	void FixedUpdate () {
	    if (active) {
			if (triggered) {
				Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
		if (active) {
			triggered = true;
		}
    }
}
