using UnityEngine;
using System.Collections;

public class orc_stats : MonoBehaviour {

    public static int justKilledTargetDelay = 20;                   // number of frames to wait after target is cleared before moving
                                                                    // prevents movement control from moving orc away from battle before a new target is aquired

    // orc stats
    public int max_health = 40;
	public float initiative = 0.7f;			// action delay = actionDealy + rnd(0, 1.0 - initiative)
	public float speed = 0.4f;              // base movement speed
    public float speedVariance = 0.1f;      // how much individual speed may vary
	public int melee_dmg_min = 2;
	public int melee_dmg_max = 8;
	public int ranged_dmg_min = 1;
	public int ranged_dmg_max = 6;
	public string teamName = "ronin";
	public string enemyTeam = "losers";
	public mv moveStrat = new mv(0.0f, 100.0f);                     // a mix of possible non-combat movement strategies   (random, march)
	public mvt moveStratTarget = mvt.stayWithin;
	public float visibleRange = 20.0f;
	
	// dynamic stats - may change during game
	public int health = 0;
	public GameObject target = null;
    public int hadTarget = 0;
	public float targetRange = range.melee;
	public Collider2D[] friends = null;
    public Collider2D[] enemies = null;
}
