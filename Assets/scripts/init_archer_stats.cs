using UnityEngine;
using System.Collections;

public class init_archer_stats : MonoBehaviour {

	// script references
	orc_stats myStats = null;

	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();

        float speedVar = Random.Range(0.0f, myStats.speedVariance);
        myStats.speed += speedVar;

        myStats.max_health = 25;
		myStats.health = myStats.max_health;
        myStats.targetRange = range.ranged;
    }
}
