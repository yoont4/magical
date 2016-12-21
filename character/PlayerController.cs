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

	public AudioSource[] attackSounds;
	public float[] attackTime;    // how long 1 attack takes (movement check)
    public float[] attackDelays;    // how long after each attack the next attack is accepted (attack check)
	public float attackMovement;    // how far forward the attack moves you
	private float attackStartTime;  // stores a ref to the last time an attack was triggered
    private float attackDelay;      // the currently chosen attack delay
	private int attackNumber;		// stores which attack player is on: -1 means no attack

	// vfx vars
	public Rigidbody2D iceParticle;
	public float iceParticleLife;
	public int iceParticleCount;
	public Animator icePillarExtension;

    public static bool isAcceptingInput = true;
    // blockMovementInput is frequently updated internally using special logic, please use blockInput
    private bool acceptMovementInput = true;

	public LayerMask groundLayer;
	public bool onGround;
	public bool onFirstJump;

	private Rigidbody2D body;
    [HideInInspector] public Animator animator;
    [HideInInspector] public float fallSpeed;

	// Use this for initialization
	void Start () {
		// grabs manager reference on instantiation
		this.manager = this.GetComponentInParent<PlayerManager> ();

		this.body = this.GetComponent<Rigidbody2D> ();
        this.animator = this.GetComponent<Animator>();
		this.attackSounds = this.GetComponentsInChildren<AudioSource> ();
		this.attackNumber = -1;
	}
	void Awake() {
		// 

	}

	// Update is called once per frame
    /**
     *  Input listening for the main character
     *  [It's way too cluttered right now, needs to be abstracted out later]
     * 
     * 
     **/
	void Update () {
        if (!isAcceptingInput) { 
            return; 
        }

        if (acceptMovementInput) {
            // attackNumber -1 represents none attack state
			attackNumber = -1;

            fallSpeed = body.velocity.y;
        
            // control falling state
            if (fallSpeed < -0.1f) {
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

		    // control jump state and ground attack state
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
			} else if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L)) && Input.GetKey(KeyCode.W)) {  //ground_attack_up check
                groundAttackUp();
            } else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L)) {	// ground_attack check
				groundAttack();
			}
        } else {
            // if no input is accepted, clear input states and decelerate player
            animator.SetBool("movementInput", false);
            body.velocity = new Vector2(body.velocity.x / stopSpeed, body.velocity.y);

            // check for ground_attack_up
            if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L)) && Input.GetKey(KeyCode.W)) {  //ground_attack_up check
                groundAttackUp();
            } else {				// every other attack check
				// detect ground attack input
				if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L)) {
					groundAttack();
				}
	        }

            // check if attack animation is over before enabling movement
			if (Time.time - attackStartTime >= attackTime[attackNumber]) {
				acceptMovementInput = true;
			}

        }
	}

	void jump(float jumpHeight) {
		// reset vertical velocity and then jumping force
		//body.transform.Translate(new Vector2(0, jumpSpring));
		body.velocity = new Vector2 (body.velocity.x, 1 );
		body.AddForce (new Vector2 (0, jumpHeight), ForceMode2D.Impulse);
		onGround = false;
	}

    void flip(Rigidbody2D body) {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // trigger the pivot animation only if exceeding the pivot speed
        // TODO
        animator.SetTrigger("runPivot");
    }

	void groundAttack() {
		// check that input is within attack delay limit and player is on ground
		if (onGround && Time.time - attackStartTime >= attackDelay) {
            if (attackNumber >= 3) {
                attackNumber = 0;   // all other attacks reset ground attacks to the first 1
            } else {
			    attackNumber = (attackNumber + 1) % 3;  // loop ground attacks
            }

			Debug.Log (attackNumber + " : " + attackSounds.Length + "->" + attackSounds [attackNumber].name);
			
			// disable non-attack input
			acceptMovementInput = false;
			animator.SetBool("movementInput", false);
			// initiate attack animation and events
			animator.SetTrigger("attack");
			
			// set startTime
			attackStartTime = Time.time;
            // set post-attack delay
            attackDelay = attackDelays[attackNumber];
			createIceParticles (iceParticleCount, iceParticleLife);
		}
    }

    void groundAttackUp() {
        // check that input is within attack delay limit and player is on ground
        if (onGround && Time.time - attackStartTime >= attackDelay) {
            attackNumber = 3;  
            Debug.Log("ground_attack_up");

            // disable non-attack input
            acceptMovementInput = false;
            animator.SetBool("movementInput", false);
            // initiate UP attack animation/events
            animator.SetTrigger("attackUp");

            // set startTime
            attackStartTime = Time.time;
            // set post-attack delay
            attackDelay = attackDelays[attackNumber];
        }
    }

	/**
	 * Used by the animator to trigger movement and sound
	 */
	void groundAttackEvent(int attackNumber) {
		// play sfx of attack
		attackSounds [attackNumber].Play ();

		// shift character in the attack direction
		if (attackNumber <= 2) {
			groundAttackMovement (attackMovement);
		} 
	}

	void groundAttackMovement(float attackMovement) {
		if (facingRight) {
			body.AddForce(new Vector2(attackMovement,0), ForceMode2D.Impulse);
		} else {
			body.AddForce(new Vector2(-attackMovement,0), ForceMode2D.Impulse);
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

	/**
	 * Used to extend the ice pillar sprite off the screen
	 * ^triggered by the ground attack up animation via animation event
	 **/
	void extendIcePillar() {
		Vector2 extensionPos;
		// properly rotate sprite depending on player direction
		Quaternion extensionRotation = transform.rotation;
		if (facingRight) {
			extensionRotation.y = 180;
		} 

		// create pillars going up the screen
		for (int i = 0; i < 6; i++) {
			extensionPos = new Vector2 (transform.position.x, transform.position.y + (2 * (i+1)));
			Animator pillar = (Animator)Instantiate (icePillarExtension, extensionPos, extensionRotation);
			Destroy (pillar.gameObject, 0.4f);	// destroyed after the last frame is played
		}
	}


 }
