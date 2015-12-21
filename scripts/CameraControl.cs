using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public GameObject target;
	public float dampTime;
	Vector3 zero = Vector3.zero;
	float z;

	void Start() {
		z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = new Vector3 (target.transform.position.x, target.transform.position.y, z);
		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref zero, dampTime);
	}
}
