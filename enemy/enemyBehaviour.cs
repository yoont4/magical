using UnityEngine;
using System.Collections;

public class enemyBehaviour : MonoBehaviour {

    public Vector2 moveDirection;
    public Transform target;
    public float dist;
    public float lookAtDist = 10;
    public float attackRange = 20;
    public float moveSpeed = 2;
    public float jumpHeight = 2;
    public bool onGround;

	private bool facingRight = false;

	// vars for hitstun
	private Rigidbody2D body;
	private float stunStart;
	private float stunTime;
	private bool stunned;

	// vars for hitstun flashing
	public Material flashMat;
	private Material originalMat;

	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
		originalMat = this.GetComponent<SpriteRenderer> ().material;
	}

    void FixedUpdate() {
		if (!stunned) {
			int rand = Random.Range (0, 100);
			//passiveHop(rand);
			aggroHop (rand);
			
			
			
			
			dist = target.position.x - transform.position.x;
			
			if (dist < lookAtDist) {
				lookAt ();
			}
			if (dist < attackRange) {
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

        if (dist > .75) {
			body.velocity = new Vector2(moveSpeed, body.velocity.y);
        } else if (dist < -.75) {
			body.velocity = new Vector2(-moveSpeed, body.velocity.y);
        }
     }

    //when not chasing target random hops
    void passiveHop(int num) {
        if (num == 69 && onGround) {
            //body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    //when chasing target more frequent hops, hops vary, two heights
    void aggroHop(int num) {
        if (num == 25 && onGround) {
            //body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }else if (num == 1 && onGround) {
            //body.velocity = new Vector2(0, 0);
            body.AddForce(new Vector2(0, jumpHeight * 2), ForceMode2D.Impulse);
        }
    }

    //detects if on ground
    void OnCollisionEnter2D(Collision2D col) {
        onGround = true;
    }

    // detects if in air
     void OnCollisionExit2D(Collision2D col) {
        onGround = false;
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
			render.material = flashMat;
			yield return new WaitForSeconds(0.1f);
			render.material = originalMat;
			yield return new WaitForSeconds(0.1f);
		}

	}
}
