using UnityEngine;
using System.Collections;

public class init_orc_warrior_stats : MonoBehaviour {

	// script references
	orc_stats myStats = null;
	
	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();

        float speedVar = Random.Range(0.0f, myStats.speedVariance);
        myStats.speed += speedVar;

        myStats.health = myStats.max_health;
        myStats.targetRange = range.melee;
    }
}
