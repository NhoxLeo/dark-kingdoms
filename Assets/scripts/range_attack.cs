using UnityEngine;
using System.Collections;

public class range_attack : MonoBehaviour {

	//script references
	orc_stats myStats = null;

	public void bow_hit(GameObject enemy) {
		int damage;
		
		damage = Random.Range(myStats.ranged_dmg_min, myStats.ranged_dmg_max);
		
		enemy.SendMessage("apply_damage", damage);
	}

	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
	}
}
