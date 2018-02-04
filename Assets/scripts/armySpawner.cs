using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum unitType {orc_warrior, archer};

public class armySpawner : MonoBehaviour {

	public GameObject orcObj;
	public GameObject archerObj;
	public int numAlive;
	public Text numAliveText;
	
	public const int MAX_UNITS = 200;
	const int ROW_SIZE = 100;
	const float X_START_BOTTOM = 15.0f;
	const float Y_START_BOTTOM = 1.0f;
	const float X_START_TOP = 1.0f;
	const float Y_START_TOP = 11.0f;
	
	orcTeam myTeam;
	
	struct unit {
		public bool used;
		public unitType type;
	}
	
	public void updateNumAlive() {
		numAliveText.text = numAlive.ToString();
	}
	
	//
	// front row orcs
	// back row archers
	//
	void dynamic_row_setup(unit[] army, int numOrcs, int numArchers) {
		int i, startThisRow;
		
		// zero out all positions
		for (i = 0; i < MAX_UNITS; i++)
			army[i].used = false;
		
		if ((numOrcs == 0) && (numArchers == 0))
			return;
		
		if (numOrcs > ROW_SIZE)
			numOrcs = ROW_SIZE;
		
		if (numArchers > ROW_SIZE)
			numArchers = ROW_SIZE;
		
		// setup archers
		startThisRow = (0 * ROW_SIZE) + ((ROW_SIZE - numArchers) / 2);
		for (i = 0; i < numArchers; i++) {
			army[startThisRow + i].used = true;
			army[startThisRow + i].type = unitType.archer;
		}
		
		// setup orcs
		startThisRow = (1 * ROW_SIZE) + ((ROW_SIZE - numOrcs) / 2);
		for (i = 0; i < numOrcs; i++) {
			army[startThisRow + i].used = true;
			army[startThisRow + i].type = unitType.orc_warrior;
		}
	}
	
	// Use this for initialization
	void Start () {
		float x_start, y_start;
		orc_stats newOrc;
		GameObject go1, temp;
		int numOrcs, numArchers, i;
		float spawnIntervalX;
		float spawnIntervalY;
		Vector3 spawnPoint ;
		unit[] armyArray = new unit[MAX_UNITS];

        if (gameObject.tag == "brown")
            {
                // top team
                x_start = X_START_TOP;
                y_start = Y_START_TOP;
                spawnIntervalX = 0.14f;
                spawnIntervalY = -0.3f;
                spawnPoint = new Vector3(x_start, y_start, 0);

                go1 = GameObject.Find("brown team");
            }
        else
            {
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

        for (i = 0; i < MAX_UNITS; i++, spawnPoint.x += spawnIntervalX)
        {
            if ((i % ROW_SIZE) == 0)
            {
                spawnPoint.y += spawnIntervalY;
                spawnPoint.x = x_start;
            }
            
            if (armyArray[i].used == false) continue;
            
            if (armyArray[i].type == unitType.archer)
                temp = Instantiate(archerObj, spawnPoint, Quaternion.identity) as GameObject;
            else
                temp = Instantiate(orcObj, spawnPoint, Quaternion.identity) as GameObject;
            newOrc = temp.GetComponent<orc_stats>();
            newOrc.teamName = gameObject.tag;
            if (gameObject.tag == "brown")
            {
                newOrc.enemyTeam = "red";
                //print("RED " + temp.GetInstanceID());
            }
            else
            {
                newOrc.enemyTeam = "brown";
                //print("BROWN " + temp.GetInstanceID());
            }
            //temp.layer = LayerMask.NameToLayer(newOrc.enemyTeam);
        }
	}
}
