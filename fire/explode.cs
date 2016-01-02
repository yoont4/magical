﻿using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {
    public GameObject explosion;
    public GameObject fire;
    public GameObject fireParticle;
    public float particleCount;
    public float velocity;
    public bool trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (trigger) {
            blowUp();
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        trigger = true;
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

    void blowUp() {
        spawnParticles();
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(fire, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
