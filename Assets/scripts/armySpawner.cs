using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum unitType {orc_warrior, archer, none};

public class armySpawner : MonoBehaviour {

    public GameObject orcObj;
    public GameObject archerObj;
    public int numAlive;
    public Text numAliveText;

    public const int MAX_UNITS = 2000;
    const int ROW_SIZE = 100;
    const float X_START_BOTTOM = 14.9f;
    const float Y_START_BOTTOM = 1.5f;
    const float X_START_TOP = 1.1f;
    const float Y_START_TOP = 22.5f;
    const float X_RAND = 0.1f;
    float x_rand_pos;


    orcTeam myTeam;

    public void updateNumAlive() {
        numAliveText.text = numAlive.ToString();
    }

    //
    // front row orcs
    // back row archers
    //
    void dynamic_row_setup(unitType[] army, int numOrcs, int numArchers) {
        int i;

        // zero out all positions
        for (i = 0; i < MAX_UNITS; i++)
            army[i] = unitType.none;

        if ((numOrcs == 0) && (numArchers == 0))
            return;

        // setup archers
        for (i = 0; i < numArchers; i++) {
            army[i] = unitType.archer;
        }

        // setup orcs
        for (i = 0; i < numOrcs; i++) {
            army[numArchers + i] = unitType.orc_warrior;
        }
    }

    // Use this for initialization
    void Start() {
        float x_start, y_start;
        orc_stats newOrc;
        GameObject go1, temp;
        int numOrcs, numArchers, i;
        float spawnIntervalX;
        float spawnIntervalY;
        Vector3 spawnPoint, actualSpawn;
        unitType[] armyArray = new unitType[MAX_UNITS];

        if (gameObject.tag == "brown") {
            // top team
            x_start = X_START_TOP;
            y_start = Y_START_TOP;
            spawnIntervalX = 0.14f;
            spawnIntervalY = -0.3f;
            spawnPoint = new Vector3(x_start, y_start, 0);

            go1 = GameObject.Find("brown team");
        }
        else {
            // bottom team
            x_start = X_START_BOTTOM;
            y_start = Y_START_BOTTOM;
            spawnIntervalX = -0.14f;
            spawnIntervalY = 0.3f;
            spawnPoint = new Vector3(x_start, y_start, 0);

            go1 = GameObject.Find("red team");
        }

        myTeam = (orcTeam)go1.GetComponent(typeof(orcTeam));
        numOrcs = myTeam.get_numOrcs();
        numArchers = myTeam.get_numArchers();

        dynamic_row_setup(armyArray, numOrcs, numArchers);

        // set these here in case row setup fudged the numbers
        numAlive = numOrcs + numArchers;
        updateNumAlive();

        for (i = 0; i < MAX_UNITS; i++, spawnPoint.x += spawnIntervalX) {
            if ((i % ROW_SIZE) == 0) {
                spawnPoint.y += spawnIntervalY;
                spawnPoint.x = x_start;
            }

            if (armyArray[i] == unitType.none) continue;

            // add a small bit of randomness to the x position so the armies look more
            // natural as they charge at each other
            x_rand_pos = Random.Range(-1.0f * X_RAND, X_RAND);
            actualSpawn = spawnPoint;
            actualSpawn.x += x_rand_pos;

            if (armyArray[i] == unitType.archer) {
                temp = Instantiate(archerObj, actualSpawn, Quaternion.identity) as GameObject;
            }
            else {
                temp = Instantiate(orcObj, actualSpawn, Quaternion.identity) as GameObject;
            }
            newOrc = temp.GetComponent<orc_stats>();
            newOrc.teamName = gameObject.tag;
            if (gameObject.tag == "brown") {
                newOrc.enemyTeam = "red";
            }
            else {
                newOrc.enemyTeam = "brown";
            }
        }
    }
}