using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 *	Represents the basic features of all creatures:
 *	This includes enemies, player, and other NPCs.
 * 
 **/
public class CreatureBehavior : MonoBehaviour {

	public _TestManager manager;
	public int health;
	public int exp;
	public Canvas expCanvas;

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

	public bool takeDamage(int damage) {
		health -= damage;
		if (health <= 0) {

			// generate exp death text
			Canvas expCopy = (Canvas)Instantiate(expCanvas, transform.position, transform.rotation);
			BoxCollider2D expCollider = expCopy.GetComponent<BoxCollider2D> ();
			Text expText = expCopy.GetComponentInChildren<Text> ();
			expText.text = "+" + this.exp.ToString();
			expCollider.size = new Vector2 (expText.preferredWidth, expText.preferredHeight);

			// enemy killed after 1/2 second
			Destroy (this.gameObject,0.3f);
			// add to the killed enemy count
			manager.killEnemy ();
			return true;
		} 
		return false;
	}
}
