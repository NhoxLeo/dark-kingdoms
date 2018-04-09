using UnityEngine;
using System.Collections;

//
// orc warrior
// - if no target
//		- get visible enemies
//		- set target to nearest enemy
// - set targetRange to melee
public class orc_warrior : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;
	public targetting myTrg;
	public orc_melee_attack my_melee_attack;

	// Use this for initialization
	void Start () {
        // init unit-specific or archer-specific attributes
        float speedVar = Random.Range(0.0f, myStats.speedVariance);
        myStats.speed += speedVar;
        myStats.health = myStats.max_health;
        myStats.targetRange = orc_stats.meleeRange;
        myStats.visibleRange = orc_stats.meleeRange * 2.0f;

        float rndDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
        float baseDelay = (orc_stats.baseInitMultiplier * (1.0f - myStats.initiative)) + myStats.initiative;
        float totalDelay =  baseDelay + rndDelay;
        Debug.Log("warrior " + baseDelay + " " + totalDelay);
        InvokeRepeating("warrior_impl", 3f, totalDelay);
	}

    // Update is called once per frame
    void warrior_impl () {
        if (myStats.target == null) {
            myTrg.getTarget();
        }

        if (myStats.target == null) {
            // didn't see anyone to fight
            return;
        }

		if (Vector2.Distance(transform.position, myStats.target.transform.position) < orc_stats.meleeRange) {
			my_melee_attack.swordHit(myStats.target);
		}
	}
}
