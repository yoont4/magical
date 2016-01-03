using UnityEngine;
using System.Collections;

/**
 * Manages all of the characters stats, inventory, experience, skills, etc.
 * Used to store character "state" between scene loads, saves, etc.
 * 
 * Stats:	(tentative)
 * 	MaxHP
 * 	Level
 * 	current level Exp
 * 	total level Exp
 * 	Attack
 * 	Defense
 */
public class PlayerManager : CreatureManager {

	public Canvas levelUpCanvas;
	private PlayerController controller;

	// Use this for initialization
	void Awake () {
		IDENTIFIER = "PLAYER";
		this.expCap = calculateExpCap (this.level);

	}

	void Start() {
		// grab player controller reference
		this.controller = this.GetComponentInChildren<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void gainExperience (int experience) {
		this.totalExp += experience;
		this.exp += experience;
		int expSpillover = this.exp - this.expCap;
		if (expSpillover >= 0) {
			gainLevel (expSpillover);
		}
	}

	public override void gainLevel(int expSpillover) {
		Debug.Log ("level up");
		// increase level
		this.level++;
		// calculate experience spillover and new exp cap
		this.exp = expSpillover;
		this.expCap = calculateExpCap (level);

		// spawn level up text
		Vector2 levelUpPosition = new Vector2(this.controller.transform.position.x, this.controller.transform.position.y + 1f);
		Canvas newLevelUp = (Canvas)Instantiate(levelUpCanvas, levelUpPosition, this.controller.transform.rotation);
	}

	private int calculateExpCap(int level) {
		// magic numbersssss
		return 100 * level + (int)(63 * Mathf.Pow (level, 2.3f));
	}
}
