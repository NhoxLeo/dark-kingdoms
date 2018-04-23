using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// Holds all methods that can get called by another army unit.
//
public class interaction : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

    bool amDead = false;

    GameObject armySpawnerObj;
    armySpawner armySpawner;

    // called to deal damage to this unit
    public void applyDamage(int damage) {
        if (amDead) {
            return;
        }

        damage -= myStats.armor;
        if (damage <= 0) {
            return;
        }
        
        myStats.health -= damage;

        // are we dead?
        if (myStats.health <= 0) {
            amDead = true;

            armySpawner.numAlive--;
            armySpawner.updateNumAlive();

            // is my whole team dead?
            if (armySpawner.numAlive == 0) {
                print(myStats.enemyTeam + " WON " + armySpawner.numAlive);
            }

            Destroy(gameObject);
        }
    }

    void Start() {
        armySpawnerObj = GameObject.Find(gameObject.tag + " spawner");
        armySpawner = (armySpawner)armySpawnerObj.GetComponent(typeof(armySpawner));
    }
}
