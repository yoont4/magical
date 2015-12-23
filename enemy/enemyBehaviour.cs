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
	
	// Update is called once per frame
	void Update () {

        dist = target.position.x - transform.position.x;

	    if (dist < lookAtDist) {
            lookAt();
        }if (dist < attackRange) {
            //hop();
            chase();
        }
	}

    void FixedUpdate() {
        int rand = (int) Mathf.Round(Random.value * 100f);
        passiveHop(rand);
        //aggroHop(rand);
    }

    void lookAt() {
        
        if (target.position.x < transform.position.x && transform.localScale.x > 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else if (target.position.x > transform.position.x && transform.localScale.x <0) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    void chaseOld() {
        if (dist > 0.1) {
            if (target.position.x < transform.position.x) {

                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);

            } else if (target.position.x > transform.position.x) {

                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);

            }
        }
    }

    void chase() {

        if (dist > .75) {
            Debug.Log("1");
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
        } else if (dist < -.75) {
            Debug.Log("2");
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
        }
     }
    void passiveHop(int num) {
        if (num == 69 && onGround) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void aggroHop(int num) {
        if (num == 25 && onGround) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }else if (num == 72 && onGround) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight * 2), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        onGround = true;
    }
}
