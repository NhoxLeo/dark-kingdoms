using UnityEngine;
using System.Collections;

public class range_attack : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

    public Animator animator;

    public void bowHit(GameObject enemy) {
		int damage;
        interaction interEnemy = enemy.GetComponent<interaction>();

        damage = Random.Range(myStats.ranged_dmg_min, myStats.ranged_dmg_max);

        if (animator) {
            animator.SetTrigger("attack");
        }
        interEnemy.applyDamage(damage);
    }

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
}
