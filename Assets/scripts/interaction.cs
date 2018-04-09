using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// Holds all methods that can get called by another army unit.
//
public class interaction : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

    GameObject armySpawnerObj;
    armySpawner armySpawner;

    // called to deal damage to this unit
    public void applyDamage(int damage) {
        damage -= myStats.armor;
        if (damage <= 0) {
            return;
        }

        // We might have multiple targets calling this simultaneously so
        // return if someone just killed me so we don't subtract extra from numAlive.
        // Wonder if we need to control access to this function to control
        // multi-thread calls?
        if (myStats.health < 0)
            return;

        myStats.health -= damage;

        // are we dead?
        if (myStats.health <= 0) {
            armySpawner.numAlive--;
            armySpawner.updateNumAlive();

            // is my whole team dead?
            if (armySpawner.numAlive == 0) {
                print(myStats.enemyTeam + " WON");
            }

            Destroy(gameObject);
        }
    }

    void Start() {
        armySpawnerObj = GameObject.Find(gameObject.tag + " spawner");
        armySpawner = (armySpawner)armySpawnerObj.GetComponent(typeof(armySpawner));
    }
}
