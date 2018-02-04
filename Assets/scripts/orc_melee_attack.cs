using UnityEngine;
using System.Collections;

public class orc_melee_attack : MonoBehaviour {

	//script references
	orc_stats myStats = null;

	public AudioClip[] meleeBattle = new AudioClip[8];

	public void sword_hit(GameObject enemy) {
		int damage;
		//int sndfx;
		
		damage = Random.Range(myStats.melee_dmg_min, myStats.melee_dmg_max);
		
		//sndfx = Random.Range(0,8);
		//AudioSource.PlayClipAtPoint(meleeBattle[sndfx], transform.position);
		
		enemy.SendMessage("apply_damage", damage);
	}

	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
	}
}
