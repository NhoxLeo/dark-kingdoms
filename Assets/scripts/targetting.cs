using UnityEngine;
using System.Collections;


public class targetting : MonoBehaviour {

    // dynamic visibility settings
    public const float addToVis = 1.0f;             // distance to expand visibility each time
    public const int visTargThreshold = 5;          // need less than this many targets to expand visibility

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;
	
    // find closest enemy to this unit
	public GameObject findNearestEnemy() {
		// myStats may not be initialized yet
		if (myStats == null)
			return null;
        
		if (myStats.enemies.Length == 0)
			return null;
        
		return findNearest(myStats.enemies);
	}
	
    // find nearest out of an array of colliders
	GameObject findNearest(Collider2D [] targets) {
		int nearestIndex = 0;
		float nearestDistance = 0.0f, temp;
        Vector3 currentPos = transform.position;

        if (targets == null)
			return null;

        // turn off march mode and engage dynamic visibility once we have engaged in battle, i.e. found someone to fight
        if (!myStats.blooded) {
            myStats.blooded = true;
            myStats.moveStrat = mv.random;
        }
        

        if (targets.Length == 1)
			return (targets[0].gameObject);
        
		nearestDistance = Vector2.Distance(currentPos, targets[0].transform.position);
		for (int i = 1; i < targets.Length; i++) {
            if (targets[i] == null)
                continue;

            //print("Me " + gameObject.GetInstanceID() + "   target " + targets[i].gameObject.GetInstanceID());
			if ((temp = Vector2.Distance(currentPos, targets[i].transform.position)) < nearestDistance) {
				nearestDistance = temp;
				nearestIndex = i;
			}
		}
		
		return (targets[nearestIndex].gameObject);
    }

    public void getTarget() {
        // get visible enemies
        myStats.enemies = Physics2D.OverlapCircleAll(transform.position, myStats.visibleRange,
                                                    1 << LayerMask.NameToLayer(myStats.enemyTeam));

        // Dynamic Visibility
        // If we aren't getting enough targets, increase visible range up to max visible range.
        // We only do this once a unit is blooded (has damaged a unit) otherwise visible range
        // will be increased while the unit is marching to battle, since he won;t see any
        // targets until he reaches the battle.
        if (myStats.blooded && (myStats.enemies.Length < targetting.visTargThreshold)) {
            if (myStats.visibleRange < orc_stats.maxVisRange) {
                myStats.visibleRange += targetting.addToVis;

                if (myStats.visibleRange > orc_stats.maxVisRange) {
                    myStats.visibleRange = orc_stats.maxVisRange;
                }
            }
        }

        myStats.target = findNearestEnemy();
    }

	// Use this for initialization
	void Start () {
	}
}
