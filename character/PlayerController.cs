using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
    public float acceleration;
	public float stopSpeed;
	public float jumpHeight;
	public float doubleJumpHeight;
	public float jumpSpring;
    public float maxFallSpeed;
	public int health;
	public int exp;


	public LayerMask groundLayer;
	public bool onGround;
	public bool onFirstJump;

	private Rigidbody2D body;
    [HideInInspector] public Animator animator;
    [HideInInspector] public float fallSpeed;
    private bool facingRight = false;

	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
        animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
        //Debug.Log(body.velocity);
        fallSpeed = this.GetComponent<Rigidbody2D>().velocity.y;
        
        // control falling state
        if (body.velocity.y < -0.1) {
            // only trigger the start falling animation if the previous frame the player was not falling
            if (!animator.GetBool("falling")) {
                animator.SetTrigger("startFalling");
            }
            animator.SetBool("falling", true);
        } else {
            animator.SetBool("falling", false);
        }

        // control movement state
		if (Input.GetKey(KeyCode.A)) {	        // move left
            animator.SetBool("movementInput", true);
            if (facingRight) {
                flip(body);
            }
            body.velocity = new Vector2(-speed, body.velocity.y);
		} else if (Input.GetKey(KeyCode.D)) {	// move right
            animator.SetBool("movementInput", true);
            if (!facingRight) {
                flip(body);
            }
            body.velocity = new Vector2(speed, body.velocity.y);
        } else {                                // stop moving
            animator.SetBool("movementInput", false);
            body.velocity = new Vector2 (body.velocity.x / stopSpeed, body.velocity.y);
        }

		// control jump state
		if (Input.GetKeyDown (KeyCode.Space)) {
            
			if (onGround) {
                animator.SetTrigger("startJump");
				jump (jumpHeight);
			} else if (onFirstJump) {
                animator.SetTrigger("startJump");
				jump (doubleJumpHeight);
				onFirstJump = false;
			}
		}
	}

	void jump(float jumpHeight) {
		// reset vertical velocity and then jumping force
		body.transform.Translate(new Vector2(0, jumpSpring));
		body.velocity = new Vector2 (body.velocity.x, 1 );
		body.AddForce (new Vector2 (0, jumpHeight), ForceMode2D.Impulse);
	}

    void flip(Rigidbody2D body) {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
