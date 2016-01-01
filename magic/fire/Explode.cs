using UnityEngine;
using System.Collections;

public class Explode : Magic {
    public GameObject explosion;
    public GameObject fire;
    public GameObject fireParticle;
    public float particleCount;
    public float velocity;
    public bool triggered;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (active) {
			if (triggered) {
				spawnParticles();
				Instantiate(explosion, transform.position, Quaternion.identity);
				Instantiate(fire, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
		if (active) {
			triggered = true;
		}
    }

    void spawnParticles() {
        Vector2 vel;
        for (int i = 0; i <= particleCount; i++) {

            GameObject p = (GameObject) Instantiate(fireParticle, transform.position, Quaternion.identity);

            vel = Random.insideUnitCircle * velocity;
            vel.y += 2; // add upward bias
                        // set particle direction/speed
            p.GetComponent<Rigidbody2D>().velocity = vel;

            // set destroy time
            Destroy(p.gameObject, 2);
        }
    }
}
