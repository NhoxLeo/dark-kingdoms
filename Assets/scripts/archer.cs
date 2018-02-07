using UnityEngine;
using System.Collections;

// archer
public class archer : MonoBehaviour {

	// script references
	orc_stats myStats = null;
	targetting myTrg = null;
	range_attack my_ranged_attack = null;
	
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
		my_ranged_attack = GetComponent<range_attack>();
		
		armySpawnerObj = GameObject.Find(gameObject.tag + " spawner");
		armySpawner = (armySpawner) armySpawnerObj.GetComponent(typeof(armySpawner));
		
		float initDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
		InvokeRepeating("archer_impl", 3f, 1.5f + initDelay);
	}
	
	GameObject findTarget() {
		return myTrg.findNearestEnemy();
	}
	
	// Update is called once per frame
	void archer_impl () {
		if (myStats.target == null) {
            // get visible enemies
            myStats.enemies = Physics2D.OverlapCircleAll(transform.position, myStats.visibleRange,
                                                        1 << LayerMask.NameToLayer(myStats.enemyTeam));

            myStats.target = findTarget();
			
			if (myStats.target == null)
				// didn't see anyone to fight
				return;	
		}

        myStats.hadTarget = orc_stats.justKilledTargetDelay;

        if (Vector3.Distance(transform.position, myStats.target.transform.position) < range.ranged) {
			my_ranged_attack.bow_hit(myStats.target);
		}
	}
}
