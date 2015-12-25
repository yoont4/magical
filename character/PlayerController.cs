using UnityEngine;
using System.Collections;

public class PlayerController : CreatureBehavior {

	public float speed;
    public float acceleration;
	public float stopSpeed;
	public float jumpHeight;
	public float doubleJumpHeight;
	public float jumpSpring;
	public float landingThreshold;
	public int health;
	public int exp;

	private AudioSource[] audios;
	public float attackTime;    // how long 1 attack takes
	public float finalAttackTime;	// how long the last attack in the chain takes
	public float attackDelay;   // how long after an attack another one can be executed
	public float finalAttackDelay;	// how long after the last attack can another one be executed
	public float attackMovement;    // how far forward the attack moves you
	private float attackStartTime;  // stores a ref to the last time an attack was triggered
	private int attackNumber;		// stores which attack player is on: -1 means no attack

	// vfx vars
	public Rigidbody2D iceParticle;
	public float iceParticleLife;

    private bool acceptInput = true;

	public LayerMask groundLayer;
	public bool onGround;
	public bool onFirstJump;

	private Rigidbody2D body;
    [HideInInspector] public Animator animator;
    [HideInInspector] public float fallSpeed;

	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
        animator = this.GetComponent<Animator>();
		audios = this.GetComponentsInChildren<AudioSource> ();
		attackNumber = -1;
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
			attackNumber = -1;

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

		    // control jump state and grond attack state
			// only allow one or the other per frame, so ground_attack trigger cannot be set
			// on jump time 
		    if (Input.GetKeyDown (KeyCode.Space)) {
            
			    if (onGround) {
                    animator.SetTrigger("startJump");
				    jump (jumpHeight);
			    } else if (onFirstJump) {
                    animator.SetTrigger("startJump");
				    jump (doubleJumpHeight);
				    onFirstJump = false;
			    }
			} else if (Input.GetMouseButtonDown(1)) {	// ground_attack check
				groundAttack(attackDelay);
			}
        } else {
            // if no input is accepted, clear input states and decelerate player
            animator.SetBool("movementInput", false);
            body.velocity = new Vector2(body.velocity.x / stopSpeed, body.velocity.y);

			if (attackNumber == 2) {	// final attack check
				// detect ground attack input
				if (Input.GetMouseButtonDown(1)) {
					groundAttack(finalAttackDelay);
				}
				if (Time.time - attackStartTime >= finalAttackTime) {
					acceptInput = true;
				}
			} else {				// every other attack check
				// detect ground attack input
				if (Input.GetMouseButtonDown(1)) {
					groundAttack(attackDelay);
				}
				if (Time.time - attackStartTime >= attackTime) {
					acceptInput = true;
				}
	        }


        }
	}

	void jump(float jumpHeight) {
		// reset vertical velocity and then jumping force
		//body.transform.Translate(new Vector2(0, jumpSpring));
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

	void groundAttack(float delayCheck) {
		// check that input is within attack delay limit and player is on ground
		if (onGround && Time.time - attackStartTime >= delayCheck) {
			attackNumber = (attackNumber + 1) % 3;
			audios [attackNumber].Play();
			Debug.Log (attackNumber + " : " + audios.Length + "->" + audios [attackNumber].name);
			
			// disable non-attack input
			acceptInput = false;
			animator.SetBool("movementInput", false);
			// initiate attack animation
			animator.SetTrigger("attack");
			// moveforward with attack 
			if (facingRight) {
				body.AddForce(new Vector2(attackMovement,0), ForceMode2D.Impulse);
			} else {
				body.AddForce(new Vector2(-attackMovement,0), ForceMode2D.Impulse);
			}
			
			// set startTime
			attackStartTime = Time.time;
			createIceParticles (20, iceParticleLife);
		}
    }

	void createIceParticles(int numberOfParticles, float particleLife) {
		for (int i = 0; i < numberOfParticles; i++) {
			float offset = 0.2f;
			if (!facingRight) {
				offset = offset * -1;
			}
			Vector2 pos = new Vector2 (transform.position.x+offset+offset*10*Random.value,transform.position.y+Random.value);
			Rigidbody2D particle = (Rigidbody2D)Instantiate (iceParticle, pos, transform.rotation);
			particle.AddForce (new Vector2 (offset*Random.value*30,2+Random.value), ForceMode2D.Impulse);
			Destroy (particle.gameObject, particleLife);
		}
	}
 }
