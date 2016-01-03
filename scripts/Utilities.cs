using UnityEngine;
using System.Collections;

/**
 * just has a bunch of utility functions that are used throughout
 * follows the singleton pattern: only 1 should exist and it should be attached to main cam
 */
public class Utilities : MonoBehaviour {

	// Static singleton property
	public static Utilities Instance { get; private set; }
	private static Camera mainCam;

	// screen shake vars
	private static bool shaking = false;
	private static float shakeFactor;

	void Awake() {
		// First we check if there are any other instances conflicting
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}

		// Here we save our singleton instance
		Instance = this;
		// assign camera ref
		mainCam = this.GetComponentInChildren<Camera>();

		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		if (shaking) {
			mainCam.transform.localPosition = Random.insideUnitCircle*shakeFactor;
			shakeFactor -= 0.2f;
			if (shakeFactor <= 0.2f) {
				shaking = false;
				shakeFactor = 0;
				mainCam.transform.localPosition = Vector2.zero;
			}
		}
	}

	/**
	 * Returns true if the col matches any of the layers in the mask
	 */
	public static bool checkLayerMask(LayerMask mask, Collider2D col) {
		// check if the colliding layer matches any of the enemy layers
		int colBit = 1 << col.gameObject.layer;
		int overlap = mask.value & colBit;
		// if the overlap (&) is positive, then the target layer matches some layer in the attack layers
		if (overlap > 0) {
			return true;
		} else {
			return false;
		}
	}

	public static void screenShake(float shakeAmount) {
		shaking = true;
		shakeFactor = shakeAmount;
	}

}