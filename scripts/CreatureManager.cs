using UnityEngine;
using System.Collections;

/**
 * Abstract creature manager class that stores all basic stats and conditions of a creature.
 */
public abstract class CreatureManager : MonoBehaviour {

	public int totalHealth;
	public int health;
	public int level;
	public int exp;		// the exp so far for this level
	public int expCap;	// the amount of exp of this level
	public int totalExp;	// the total exp so far
	public int attack;
	public int defense;
	public string IDENTIFIER;	// denotes "PLAYER", "ENEMY", "NPC", etc.

	// experience functions
	public abstract void gainExperience (int experience);

	// leveling functions
	public abstract void gainLevel (int expSpillover);
			
}
