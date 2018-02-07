using UnityEngine;
using System.Collections;

//
// orc warrior
// - if no target
//		- get visible enemies
//		- set target to nearest enemy
// - set targetRange to melee
public class orc_warrior : MonoBehaviour {

	// script references
	orc_stats myStats = null;
	targetting myTrg = null;
	orc_melee_attack my_melee_attack = null;
	
	GameObject armySpawnerObj;
	armySpawner armySpawner;

	public void apply_damage(int damage) {
		// could have just been killed
		if (myStats.health < 0) return;
		
		myStats.health -= damage;
		
		if (myStats.health < 0) {
			armySpawner.numAlive--;
			armySpawner.updateNumAlive();
			
			if (armySpawner.numAlive == 0) {
				print(myStats.enemyTeam + " WON");
			}
			
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
		myTrg = GetComponent<targetting>();
		my_melee_attack = GetComponent<orc_melee_attack>();
		
		armySpawnerObj = GameObject.Find(gameObject.tag + " spawner");
		armySpawner = (armySpawner) armySpawnerObj.GetComponent(typeof(armySpawner));
		
		float initDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
		InvokeRepeating("warrior_impl", 3f, 1.5f + initDelay);
	}
	
	GameObject findTarget() {
		return myTrg.findNearestEnemy();
	}
	
	// Update is called once per frame
	void warrior_impl () {
		if (myStats.target == null) {
            // get visible enemies
            //print(myStats.teamName + " looking for " + myStats.enemyTeam);
            myStats.enemies = Physics2D.OverlapCircleAll(transform.position, myStats.visibleRange,
            					1 << LayerMask.NameToLayer(myStats.enemyTeam));
            
            //print("LOOKED FOR TARGETS " + myStats.enemies.Length);
            myStats.target = findTarget();

            //print(gameObject.GetInstanceID() + " killing " + myStats.target.GetInstanceID());

            if (myStats.target == null)
            {
                //print("NO TARGET");
                // didn't see anyone to fight
                return;
            }
		}

        myStats.hadTarget = orc_stats.justKilledTargetDelay;

		if (Vector3.Distance(transform.position, myStats.target.transform.position) < range.melee) {
			my_melee_attack.sword_hit(myStats.target);
		}
	}
}
