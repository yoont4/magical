using UnityEngine;
using System.Collections;

/**
 *	Represents the basic features of all creatures:
 *	This includes enemies, player, and other NPCs.
 * 
 **/
public class CreatureBehavior : MonoBehaviour {

	// directional vars
	[HideInInspector] public bool facingRight = false;	// sprites are default facing left

	// which layers this creature attacks
	public LayerMask attackCollisionLayers;

	// hit-stun vars
	[HideInInspector] public bool stunned;
	[HideInInspector] public float stunStart;
	[HideInInspector] public float stunTime;
	private Material originalMaterial;
	public Material flashMaterial;

	void Awake() {
		Debug.Log ("creature spawned");
		originalMaterial = this.GetComponent<SpriteRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void stun(float time) {
		stunned = true;
		stunStart = Time.time;
		stunTime = time;
		StartCoroutine(flash (time));
	}

	IEnumerator flash(float time) {
		Renderer render = this.GetComponent<Renderer> ();
		for (int i = 0; i < 2; i++) {
			render.material = flashMaterial;
			yield return new WaitForSeconds(0.1f);
			render.material = originalMaterial;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
