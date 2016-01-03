using UnityEngine;
using System.Collections;

public class EnemyManager : CreatureManager {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * TODO: for now, enemies cannot gain experience
	 */
	public override void gainExperience (int experience) {
		// nothing
	}

	/**
	 * TODO: For now, enemies cannot gain levels
	 */
	public override void gainLevel(int expSpillover) {
		// nothing
	}
}
