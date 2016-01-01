using UnityEngine;
using System.Collections;

public class disappear : MonoBehaviour {

    public float activeTime;
    private float timer;

	// Use this for initialization
	void Start () {
        activeTime = activeTime * 50;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer++;
        if (timer >= activeTime) {
            Destroy(gameObject);
        }
	}
}
