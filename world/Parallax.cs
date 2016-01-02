using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform target;
	public float xRate = 10f;
	public float yRate = 10f;
	private Vector2 targetRef;
	private Vector2 startRef;
	private float x;
	private float y;

	// Use this for initialization
	void Start () {
		targetRef = target.transform.position;
		startRef = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		x = -(target.transform.position.x - targetRef.x) / 100f * xRate;
		y = -(target.transform.position.y - targetRef.y) / 100f * yRate;
		this.transform.position = new Vector2 (startRef.x + x, startRef.y + y);
	}
}
