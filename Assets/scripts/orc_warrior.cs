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
	
	GameObject armySpawnerObj;
	armySpawner armySpawner;

	// Use this for initialization
	void Start () {
        armySpawnerObj = GameObject.Find(gameObject.tag + " spawner");
		armySpawner = (armySpawner) armySpawnerObj.GetComponent(typeof(armySpawner));

        // init unit-specific or archer-specific attributes
        float speedVar = Random.Range(0.0f, myStats.speedVariance);
        myStats.speed += speedVar;
        myStats.health = myStats.max_health;
        myStats.targetRange = orc_stats.meleeRange;
        myStats.visibleRange = orc_stats.meleeRange * 2.0f;

        float initDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
		InvokeRepeating("warrior_impl", 3f, 2.5f + initDelay);
	}

    // Update is called once per frame
    void warrior_impl () {
		if (myStats.target == null) {
            // get visible enemies
            //print(myStats.teamName + " looking for " + myStats.enemyTeam);
            
            myStats.enemies = Physics2D.OverlapCircleAll(transform.position, myStats.visibleRange,
            					1 << LayerMask.NameToLayer(myStats.enemyTeam));
            
            if (myStats.blooded && (myStats.enemies.Length < targetting.visTargThreshold)) {
                if (myStats.visibleRange < orc_stats.maxVisRange) {
                    myStats.visibleRange += targetting.addToVis;
                    if (myStats.visibleRange > orc_stats.maxVisRange) {
                        myStats.visibleRange = orc_stats.maxVisRange;
                    }
                }
                    
            }

            //print("LOOKED FOR TARGETS " + myStats.enemies.Length);
            myStats.target = myTrg.findNearestEnemy();

            //print(gameObject.GetInstanceID() + " killing " + myStats.target.GetInstanceID());

            if (myStats.target == null)
            {
                //print("NO TARGET");
                // didn't see anyone to fight
                return;
            }
		}

        myStats.hadTarget = orc_stats.justKilledTargetDelay;

		if (Vector2.Distance(transform.position, myStats.target.transform.position) < orc_stats.meleeRange) {
			my_melee_attack.swordHit(myStats.target);
		}
	}
}
