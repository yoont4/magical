using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	// pursuit vars
	public GameObject target;
	public float dampTime;
	// alignment vars
    public bool pixelAlign = true;

	// positioning vars
	private Vector3 zero = Vector3.zero;
    private float x, y, z;
    private float scalar = 0.03125f;    // 1 pixel

	// boundary vars
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	void Start() {
		z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        
        // get the player position
		Vector3 targetPos = new Vector3 (target.transform.position.x, target.transform.position.y, z);

		// apply bounds
		if (targetPos.x < xMin) {
			targetPos.x = xMin;
		} else if(targetPos.x > xMax) {
			targetPos.x = xMax;
		}
		if(targetPos.y < yMin) {
			targetPos.y = yMin;
		} else if(targetPos.y > yMax) {
			targetPos.y = yMax;
		}

        // calculate the new position
        Vector3 unRoundedPos = Vector3.SmoothDamp(transform.position, targetPos, ref zero, dampTime);

        if (pixelAlign) {
            // round the new position to allign on each pixel
            x = Mathf.Round(unRoundedPos.x / scalar) * scalar;
            y = Mathf.Round(unRoundedPos.y / scalar) * scalar;
            transform.position = new Vector3(x, y, z);
        } else {
            transform.position = unRoundedPos;
        }
	}
}
