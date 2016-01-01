using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 *	Represents the basic features of all creatures:
 *	This includes enemies, player, and other NPCs.
 * 
 **/
public class CreatureBehavior : MonoBehaviour {

	// store a reference to it's container
	// **all creatures are stored within a container
	private Transform container;

	public _TestManager manager;
	public int health;
	public int def;
	public int exp;
	public Canvas expCanvas;
	public Canvas damageCanvas;

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
		container = this.transform.parent;

	}

	// Update is called once per frame
	void Update () {
	
	}

	//------------------------------------------------------------------------------------------------//
	//----------------------------------------DAMAGE FUNCTIONS----------------------------------------//
	//------------------------------------------------------------------------------------------------//

	/**
	 * Stuns/incapacitates the current creature for a set amount of time
	 * @see this.flash()
	 */
	public void stun(float time) {
		stunned = true;
		stunStart = Time.time;
		stunTime = time;
		StartCoroutine(flash (time));
	}

	/**
	 *	causes the sprite to flash for a set amount of time
	 */
	IEnumerator flash(float time) {
		Renderer render = this.GetComponent<Renderer> ();
		for (int i = 0; i < 2; i++) {
			render.material = flashMaterial;
			yield return new WaitForSeconds(0.1f);
			render.material = originalMaterial;
			yield return new WaitForSeconds(0.1f);
		}
	}

	/**
	 * Applies damage to the creature and kills it if it's health runs out
	 */
	public bool takeDamage(int damage) {
		// apply damage
		health -= damage;

		// spawn damage text
		Canvas damageCopy = (Canvas)Instantiate(damageCanvas, transform.position, transform.rotation);
		Text[] damageText = damageCopy.GetComponentsInChildren<Text> ();
		damageText[0].text = damage.ToString();
		damageText[1].text = damage.ToString();


		// if the enemy dies, spawn the exp text
		if (health <= 0) {

			// generate exp death text
			Canvas expCopy = (Canvas)Instantiate(expCanvas, transform.position, transform.rotation);
			BoxCollider2D expCollider = expCopy.GetComponent<BoxCollider2D> ();
			Text[] expText = expCopy.GetComponentsInChildren<Text> ();
			expText[0].text = "+" + this.exp.ToString();
			expText[1].text = "+" + this.exp.ToString();
			expCollider.size = new Vector2 (expText[0].preferredWidth, expText[0].preferredHeight);

			// enemy killed after 1/2 second
			Destroy (container.gameObject,0.3f);
			// add to the killed enemy count
			manager.killEnemy ();
			return true;
		} 
		return false;
	}

	/**
	 * Applies knockback and stun to the creature 
	 */
	public void takeKnockback(Vector2 knockback) {
		// reset the enemy velocity before applying force
		Rigidbody2D body = this.GetComponent<Rigidbody2D>();
		body.velocity = Vector2.zero;
		// apply knockback
		body.AddForce(knockback, ForceMode2D.Impulse);
	}
}
