using UnityEngine;
using System.Collections;

public class InfiniteScroll : MonoBehaviour {

	public float scrollRate = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x - 0.02f*scrollRate, transform.position.y);
	}
}
