using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {
    public GameObject explosion;
    public bool trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (trigger) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        trigger = true;
    }
}
