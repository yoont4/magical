using UnityEngine;
using System.Collections;

public class DirectionalOutput : MonoBehaviour {
	public Rigidbody2D castingItem; // a reference to the prefab to be cast
	public PhysicsMaterial2D inertMaterial;	// a reference to inert material that is applied before magic release
    public float control;           // how "magentized" the object is
    public float lifeTime;          // how long the prefab lasts after releasing the mouse

    private Rigidbody2D newItem;    // copy of the cast item
	private Magic newMagic;			// copy of the cast item's magic properties
	private PhysicsMaterial2D originalMaterial;	//a reference to the physics material attached to the original casting item
    private float originalGravity; 
	private Vector2 lastPos;        // used to track mouse position

	private BoxCollider2D newCollider;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

            // instantiate cast item
			newItem = (Rigidbody2D)Instantiate (castingItem, lastPos, transform.rotation);
			newMagic = newItem.GetComponent<Magic>();
			if (newMagic != null) {
				newMagic.active = false;
			}
			// make the collider inert before casting 
			newCollider = newItem.GetComponent<BoxCollider2D> ();
			originalMaterial = newCollider.sharedMaterial;
			newCollider.sharedMaterial = inertMaterial;

            // store original item's gravity scale before disabling gravity
            originalGravity = newItem.gravityScale;
            newItem.gravityScale = 0;
		} else if (Input.GetMouseButton (0)) {
			lastPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            newItem.velocity = (lastPos - newItem.position) * control;
            
		} else if (Input.GetMouseButtonUp (0)) {
            // reapply gravity
            newItem.gravityScale = originalGravity;
			// reapply physics material
			newCollider.sharedMaterial = originalMaterial;
			Cast (newItem);

			// reactivate the magic
			if (newMagic != null) {
				newMagic.active = true;
			}


            // destroys initial non active cast item, creates active version of the item
            // sets all settings of the non active to active
            //Destroy(newItem.gameObject);
            //newItemActive = (Rigidbody2D)Instantiate(castingItemActive, lastPos, transform.rotation);
            //newItemActive.velocity = newItem.velocity;
            //newItemActive.gravityScale = originalGravity;
			//Cast (newItemActive);
		}
	}

	void Cast (Rigidbody2D item) {
        // set cast item life time
        Destroy(item.gameObject, lifeTime);
	}
}
