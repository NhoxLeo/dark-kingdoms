using UnityEngine;
using System.Collections;

public class orc_stats : MonoBehaviour {

    public const float maxVisRange = 5.0f;
    public const float meleeRange = 0.3f;
    public const float rangedRange = 2.0f;

    public static int justKilledTargetDelay = 20;                   // number of frames to wait after target is cleared before moving
                                                                    // prevents movement control from moving orc away from battle before a new target is aquired

    // orc stats
    public int max_health;
	public float initiative;        // action delay = actionDealy + rnd(0, 1.0 - initiative)
	public float speed;             // base movement speed
    public float speedVariance;     // how much individual speed may vary
	public int melee_dmg_min;
	public int melee_dmg_max;
	public int ranged_dmg_min;
	public int ranged_dmg_max;
	public string teamName = "ronin";
	public string enemyTeam = "losers";
	public mv moveStrat = new mv(0.0f, 100.0f);         // a mix of possible non-combat movement strategies   (random, march)
	public mvt moveStratTarget;
	public float visibleRange;
    public float targetRange;

    // dynamic stats - may change per orc during game
    public int health = 0;
	public GameObject target = null;
    public int hadTarget = 0;                           //  has the unit recently targetted someone?  used to keep them from
                                                        // wandering away from combat right after killing their target
	
	public Collider2D[] friends = null;
    public Collider2D[] enemies = null;
    public bool blooded = false;                        // has the orc damaged anyone? used to determine if we can start expanding visibility or not
}
