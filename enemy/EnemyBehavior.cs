using UnityEngine;
using System.Collections;

public class EnemyBehavior : CreatureBehavior {

    public Vector2 moveDirection;
    public Transform target;
    public float dist;
    public float lookAtDist = 10;
	public float stoppingDist = 1;
    public float attackRange = 20;
    public float moveSpeed = 2;
    public float jumpHeight = 2;
    public bool onGround;

	// vars for hitstun
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		// grab manager reference in parent
		this.manager = this.GetComponentInParent<EnemyManager>();
		body = this.GetComponent<Rigidbody2D> ();
	}

    void FixedUpdate() {
		if (!stunned) {
            int rand = Random.Range(0, 100);
            dist = target.position.x - transform.position.x;
			
            if (dist > lookAtDist) {
                passiveHop (rand);
            }else if (dist < lookAtDist) {
				lookAt ();
			}
			if (dist < attackRange) {
                aggroHop (rand);
				chase ();
			}
		} else {
			// decelerate if stunned and on the ground
			if (onGround) {
				body.velocity = new Vector2 (body.velocity.x / 1.2f, body.velocity.y);
			}
			// once cooldown is over, enemy is no longer stunned
			if (Time.time - stunStart >= stunTime) {
				stunned = false;
			}
		}
    }

    // look at target
    void lookAt() {
        
		// sprite is default left-facing
		if (target.position.x < transform.position.x && facingRight) {
			transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
			facingRight = !facingRight;
        }
		else if (target.position.x > transform.position.x && !facingRight) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
			facingRight = !facingRight;
        }
    }
   
    // chases the target
    void chase() {

		if (dist > stoppingDist) {
			body.velocity = new Vector2 (moveSpeed, body.velocity.y);
		} else if (dist < -stoppingDist) {
			body.velocity = new Vector2 (-moveSpeed, body.velocity.y);
		} else if (onGround) { 
			// decelerate if within stopping distance and on ground
			body.velocity = new Vector2 (body.velocity.x / 1.2f, body.velocity.y);
		}
     }

    //when not chasing target random hops
    void passiveHop(int num) {
        if (num == 69 && onGround) {
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    //when chasing target more frequent hops, hops vary, two heights
    void aggroHop(int num) {
        if (num == 25 && onGround) {
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        } else if (num == 1 && onGround) {
            body.AddForce(new Vector2(0, jumpHeight * 2), ForceMode2D.Impulse);
        }
    }

    //detects if on ground
    void OnCollisionEnter2D(Collision2D col) {
        onGround = true;
    }

	void OnCollisionStay2D(Collision2D col) {
		onGround = true;
	}

    // detects if in air
     void OnCollisionExit2D(Collision2D col) {
        onGround = false;
    }


}
