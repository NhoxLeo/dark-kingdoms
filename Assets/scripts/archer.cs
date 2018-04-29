using UnityEngine;
using System.Collections;

// archer
public class archer : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;
	public targetting myTrg;
	public range_attack my_ranged_attack;
    public orc_ctrl myCtrl;
	
	// Use this for initialization
	void Start () {		
        // init unit-specific or archer-specific attributes
        float speedVar = Random.Range(0.0f, myStats.speedVariance);
        myStats.speed += speedVar;
        myStats.health = myStats.max_health;
        myStats.targetRange = orc_stats.rangedRange;
        myStats.visibleRange = orc_stats.rangedRange;

        float rndDelay = Random.Range(0.0f, 1.0f - myStats.initiative);
        float baseDelay = (orc_stats.baseInitMultiplier * (1.0f - myStats.initiative)) + myStats.initiative;
        float totalDelay = baseDelay + rndDelay;
 
        InvokeRepeating("archer_impl", 3f, totalDelay);
    }
	
    // Update is called once per frame
    void archer_impl () {
        if (myStats.target == null) {
            myTrg.getTarget();
        }

        if (myStats.target == null) {
            // didn't see anyone to fight
            return;
        }

        if (Vector2.Distance(transform.position, myStats.target.transform.position) < orc_stats.rangedRange) {
            my_ranged_attack.bowHit(myStats.target);
		}
	}
}
