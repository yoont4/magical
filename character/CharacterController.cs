using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float speed;
	public float stopSpeed;
	public float jumpHeight;
	public float doubleJumpHeight;
	public float jumpSpring;
	public int health;
	public int exp;


	public LayerMask groundLayer;
	public bool onGround;
	public bool onFirstJump;


	private Rigidbody2D body;
	public Transform bottomLeft;


	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		// horizontal movement 
		if (Input.GetKey(KeyCode.A)) {	// move left
			body.velocity = new Vector2 (-speed, body.velocity.y);
		} else if (Input.GetKey(KeyCode.D)) {	// move right
			body.velocity = new Vector2 (speed, body.velocity.y);
		} else {	// stop moving
			body.velocity = new Vector2 (body.velocity.x / stopSpeed, body.velocity.y);
		}

		// jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (onGround) {
				jump (jumpHeight);
			} else if (onFirstJump) {
				jump (doubleJumpHeight);
				onFirstJump = false;
			}
		}
	}

	void jump(float jump) {
		// reset vertical velocity and then jumping force
		body.transform.Translate(new Vector2(0, jumpSpring));
		body.velocity = new Vector2 (body.velocity.x, 1 );
		body.AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
	}
}
