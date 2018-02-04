using UnityEngine;
using System.Collections;

public class init_orc_warrior_stats : MonoBehaviour {

	// script references
	orc_stats myStats = null;
	
	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
		
		myStats.health = myStats.max_health;
		myStats.melee_dmg_min = 2;
		myStats.melee_dmg_max = 8;
	}
}
