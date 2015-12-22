using UnityEngine;
using System.Collections;

public class directionalOutput : MonoBehaviour {
	public Rigidbody2D castingItem; // a reference to the prefab to be cast
    public float control;           // how "magentized" the object is
    public float lifeTime;          // how long the prefab lasts after releasing the mouse

    private Rigidbody2D newItem;    // copy of the cast item
    private float originalGravity; 
	private Vector2 lastPos;        // used to track mouse position
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

            // instantiate cast item
			newItem = (Rigidbody2D)Instantiate (castingItem, lastPos, transform.rotation);

            // store original item's gravity scale before disabling gravity
            originalGravity = newItem.gravityScale;
            newItem.gravityScale = 0;
		} else if (Input.GetMouseButton (0)) {

			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            newItem.velocity = (lastPos - newItem.position) * control;
            
		} else if (Input.GetMouseButtonUp (0)) {
            // reapply gravity
            newItem.gravityScale = originalGravity;
			Cast (newItem);
		}
	}

	void Cast (Rigidbody2D item) {
        // set cast item life time
        Destroy(item.gameObject, lifeTime);
	}
}
