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

	void Start() {
		z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        
        // get the player position
		Vector3 targetPos = new Vector3 (target.transform.position.x, target.transform.position.y, z);
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
