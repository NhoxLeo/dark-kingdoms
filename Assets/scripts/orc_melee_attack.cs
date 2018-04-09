using UnityEngine;
using System.Collections;

public class orc_melee_attack : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

	public AudioClip[] meleeBattle = new AudioClip[8];

	public void swordHit(GameObject enemy) {
		int damage;
        interaction interEnemy = enemy.GetComponent<interaction>();
        //int sndfx;

        damage = Random.Range(myStats.melee_dmg_min, myStats.melee_dmg_max);

        //sndfx = Random.Range(0,8);
        //AudioSource.PlayClipAtPoint(meleeBattle[sndfx], transform.position);

        interEnemy.applyDamage(damage);
    }

	// Use this for initialization
	void Start () {
    }
}
