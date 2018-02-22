using UnityEngine;
using System.Collections;

// archer
public class archer : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;
	public targetting myTrg;
	public range_attack my_ranged_attack;
	
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
        myStats.targetRange = orc_stats.rangedRange;
        myStats.visibleRange = orc_stats.rangedRange;

        float initDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
		InvokeRepeating("archer_impl", 3f, 2.5f + initDelay);
    }
	
    // Update is called once per frame
    void archer_impl () {
		if (myStats.target == null) {
            // get visible enemies
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

            myStats.target = myTrg.findNearestEnemy();
			
			if (myStats.target == null)
				// didn't see anyone to fight
				return;	
		}

        myStats.hadTarget = orc_stats.justKilledTargetDelay;

        if (Vector2.Distance(transform.position, myStats.target.transform.position) < orc_stats.rangedRange) {
			my_ranged_attack.bowHit(myStats.target);
		}
	}
}
