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

    public float attackTime;    // how long 1 attack takes
    public float attackDelay;   // how long after an attack another one can be executed
    public float attackMovement;    // how far forward the attack moves you
    private float attackStartTime;  // stores a ref to the last time an attack was triggered

    private bool acceptInput = true;


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
    /**
     *  Input listening for the main character
     *  [It's way too cluttered right now, needs to be abstracted out later]
     * 
     * 
     **/
	void Update () {
        if (acceptInput) {
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
                body.velocity = new Vector2(-speed - (-speed - body.velocity.x)/acceleration, body.velocity.y);
		    } else if (Input.GetKey(KeyCode.D)) {	// move right
                animator.SetBool("movementInput", true);
                if (!facingRight) {
                    flip(body);
                }
                body.velocity = new Vector2(speed - (speed - body.velocity.x) / acceleration, body.velocity.y);
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
        } else {
            // if no input is accepted, clear input states and decelerate player
            animator.SetBool("movementInput", false);
            body.velocity = new Vector2(body.velocity.x / stopSpeed, body.velocity.y);
            if (Time.time - attackStartTime >= attackTime) {
                acceptInput = true;
            }
        }

        // melee attack input is not attached to other inputs
        if (Input.GetMouseButtonDown(1) && onGround) {
            groundAttack();
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

        // trigger the pivot animation only if exceeding the pivot speed
        // TODO
        animator.SetTrigger("runPivot");
    }

    void groundAttack() {
        // disable non-attack input
        acceptInput = false;
        animator.SetBool("movementInput", false);
        // initiate attack animation
        animator.SetTrigger("attack");
        // moveforward with attack 
        // TODO
        if (facingRight) {
            body.AddForce(new Vector2(attackMovement,0), ForceMode2D.Impulse);
        } else {
            body.AddForce(new Vector2(-attackMovement,0), ForceMode2D.Impulse);
        }

        // set startTime
        attackStartTime = Time.time;
    }
}
