using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	// creature to spawn vars
	public Transform creatureContainer;
	private EnemyBehavior creature;
	// controller vars
	public Transform target;
	public _TestManager manager;
	public float spawnRate;
	public int spawnCount;
	public float spawnWidth;

	void Awake() {
		creature = creatureContainer.GetComponentInChildren<EnemyBehavior> ();
		// override spawn rate if too small
		if (spawnRate <= 0.15f) {
			spawnRate = 0.25f;
		}

		InvokeRepeating ("spawn", spawnRate, spawnRate);
	}

	void spawn() {
		Vector2 spawnLocation = new Vector2 (transform.position.x+Random.value*spawnWidth, transform.position.y);
		Transform newContainer = (Transform)Instantiate (creatureContainer, spawnLocation, transform.rotation);
		EnemyBehavior newCreature = newContainer.GetComponentInChildren<EnemyBehavior> ();
		newCreature.target = target;
		newCreature.manager = manager;

		spawnCount--;
		if (spawnCount <= 0) {
			Destroy (this.gameObject);
		}
	}
}
