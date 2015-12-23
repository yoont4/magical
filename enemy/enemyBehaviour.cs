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

	// Use this for initialization
	void Start () {
        
	}

    void FixedUpdate() {
        int rand = Random.Range(0, 100);
        //passiveHop(rand);
        aggroHop(rand);




        dist = target.position.x - transform.position.x;

        if (dist < lookAtDist) {
            lookAt();
        }
        if (dist < attackRange) {
            chase();
        }
    }

    // look at target
    void lookAt() {
        
        if (target.position.x < transform.position.x && transform.localScale.x > 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else if (target.position.x > transform.position.x && transform.localScale.x <0) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
   
    // chases the target
    void chase() {

        if (dist > .75) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
        } else if (dist < -.75) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
        }
     }

    //when not chasing target random hops
    void passiveHop(int num) {
        if (num == 69 && onGround) {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    //when chasing target more frequent hops, hops vary, two heights
    void aggroHop(int num) {
        if (num == 25 && onGround) {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }else if (num == 1 && onGround) {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight * 2), ForceMode2D.Impulse);
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
}
