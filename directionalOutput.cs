using UnityEngine;
using System.Collections;

public class directionalOutput : MonoBehaviour {
	public Vector2 lastPos;
	private Vector2 change;
	public Vector2 velocity;
	private float timeHeld;
	private float changeInTime;
	public Vector2 avgVel;

	public Rigidbody2D castingItem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			timeHeld = Time.time;
			castingItem = (Rigidbody2D)Instantiate (castingItem, lastPos, transform.rotation);
		} else if (Input.GetMouseButton (0)) {
			castingItem.transform.position = lastPos;
			change = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition) - lastPos;
			changeInTime = Time.time - timeHeld;
			velocity = change / (Time.time - timeHeld);

			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		} else if (Input.GetMouseButtonUp (0)) {
			Cast (castingItem);
		}
	}

	void Cast (Rigidbody2D item) {
		//Rigidbody2D item = (Rigidbody2D)Instantiate (castingItem, lastPos, transform.rotation);
		item.velocity = velocity;
	}
}
