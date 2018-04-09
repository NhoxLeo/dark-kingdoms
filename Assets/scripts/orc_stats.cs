using UnityEngine;
using System.Collections;

// holds stats for a unit
public class orc_stats : MonoBehaviour {

    public const float maxVisRange = 5.0f;                          // maximum visible range
    public const float meleeRange = 0.2f;                           // range for melee combat
    public const float rangedRange = 2.0f;                          // range for ranged combat
    public const float baseInitMultiplier = 5.0f;                    // used to calulate total initiative and invokeRepeating delay

    // orc stats
    public int max_health;          // when a unit is at full health
    public int armor;
	public float initiative;        // action delay = actionDealy + rnd(0, 1.0 - initiative)
	public float speed;             // base movement speed
    public float speedVariance;     // how much individual speed may vary
	public int melee_dmg_min;
	public int melee_dmg_max;
	public int ranged_dmg_min;
	public int ranged_dmg_max;
	public string teamName;                             // used to differentiate thje two team, mostly for layer masking for
                                                        // overLapCircelAll()
	public string enemyTeam;
	public mv moveStrat;                                // non-combat movement strategies   (random, march)
	public mvt moveStratTarget;                         // movement strategy when we have a target
	public float visibleRange;                          // how far away can we select a target?
    public float targetRange;                           // how close will we move to a target when we are using mvt_stayWithin()

    // dynamic stats - may change per orc during game
    public int health = 0;                              // this unit's health
	public GameObject target = null;                    // this unit's target, what he is attacking
	
	public Collider2D[] friends = null;                 // currently not used, could be used in fiture by healers / buffers
    public Collider2D[] enemies = null;                 // holds the list of enemy targets within visible range
    public bool blooded = false;                        // has the orc found an enemy target? used to determine if we can start expanding visibility or not
                                                        // and to switch from march mode to random mode movement
}
