using UnityEngine;
using System.Collections;

public class init_archer_stats : MonoBehaviour {

	// script references
	orc_stats myStats = null;

	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
		
		myStats.max_health = 25;
		myStats.health = myStats.max_health;
		myStats.melee_dmg_max = 4;
		myStats.ranged_dmg_min = 1;
		myStats.ranged_dmg_max = 6;
	}
}
