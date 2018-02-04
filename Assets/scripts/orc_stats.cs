using UnityEngine;
using System.Collections;

public class orc_stats : MonoBehaviour {

	// orc stats
	public int max_health = 40;
	public float initiative = 0.7f;			// action delay = actionDealy + rnd(0, 1.0 - initiative)
	public float speed = 0.5f;
	public int melee_dmg_min = 1;
	public int melee_dmg_max = 6;
	public int ranged_dmg_min = 0;
	public int ranged_dmg_max = 0;
	public string teamName = "ronin";
	public string enemyTeam = "losers";
	public mv moveStrat = new mv(0.0f, 100.0f);
	public mvt moveStratTarget = mvt.stayWithin;
	public float visibleRange = 2.0f;
	
	// dynamic stats - may change during game
	public int health = 0;
	public int weap_dmg = 0;
	public int armor = 0;
	public GameObject target = null;
	public float targetRange = 0.0f;
	public Collider2D[] friends = null;
	public Collider2D[] enemies = null;
}
