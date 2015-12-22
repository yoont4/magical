using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public int particle_count;		// # of particles spawned
	public float particle_velocity;	// initial particle speed
	public float particle_life_time;	// in seconds
	public Rigidbody2D particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 vel;

			// get mouse position 
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// spawn particles
			for (int i = 0; i < particle_count; i++) {
				// modulate spawning position to reduce initial collision calucations
				Rigidbody2D pp = (Rigidbody2D)Instantiate (
					particle, 
					pos,
					transform.rotation);

				vel = Random.insideUnitCircle * particle_velocity;
				vel.y += 2;	// add upward bias
				// set particle direction/speed
				pp.velocity = vel;

				// set destroy time
				Destroy (pp.gameObject,particle_life_time);

			}
		}
	}
}
