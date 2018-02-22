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

    // 
    public void applyDamage(int damage) {
        // we might have multiple targets calling this simultaneously
        // return if someone just killed me so we don't subtract extra from numAlive
        if (myStats.health < 0) return;

        myStats.health -= damage;

        if (myStats.health < 0) {
            armySpawner.numAlive--;
            armySpawner.updateNumAlive();

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
