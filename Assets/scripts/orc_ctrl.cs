using UnityEngine;
using System.Collections;

public enum mvt {stayWithin, stayAt};
public enum mv { march, random };


// controls unit movement
public class orc_ctrl : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

	// general movement
	float dir_x = 0;
	float dir_y = 1.0f;

    // bounds for legal playing area
	float min_x = 0.1f;
	float max_x = 15.7f;
	float min_y = 0.5f;
	float max_y = 23.5f;

	int stride = 30;        // makes random movement more natural by increasing chance to keep your current direction
	int courseStay = 0;     // prevent reversing dir after wall hit just reversed dir

    // see if we have hit the edge of our legal playing area
    void check_edge() {
		if (transform.position.x < min_x) {
			dir_x = 1.0f;
			dir_y = 0;
			courseStay = stride;
		} else if (transform.position.x > max_x) {
			dir_x = -1.0f;
			dir_y = 0;
			courseStay = stride;
		}
		
		if (transform.position.y < min_y) {
			dir_x = 0;
			dir_y = 1.0f;
			courseStay = stride;
		} else if (transform.position.y > max_y) {
			dir_x = 0;
			dir_y = -1.0f;
			courseStay = stride;
		}
	}
	
	// move random, unless courseStay is set (e.g. we just hit a boundary)
	void mv_random() {
		float chg_x, chg_y;

		// don't change direction if we just hit the wall
		if (courseStay > 0) {
			courseStay--;
		} else {
            // stride tends to keep an orc going in the same direction
            // only occasionally changing direction
			chg_x = Random.Range(-1, stride);
			chg_y = Random.Range(-1, stride);

			if (chg_x < 2.0)
				dir_x = chg_x;
			if (chg_y < 2.0)
				dir_y = chg_y;
		}
		
		check_edge();
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// march straight to other side of the game screen
	void mv_march() {
		// running into boundaries should set up march directions if only march is set
		check_edge();
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// stay within a certain range of target
	void mvt_stayWithin() {
		if (Vector2.Distance(transform.position, myStats.target.transform.position) <= myStats.targetRange)
            // we are as close to the target as we want to be
			return;

        // get X and Y distance to target
        float diffX = transform.position.x - myStats.target.transform.position.x;
        float diffY = transform.position.y - myStats.target.transform.position.y;

        // get the x and y direction to move in
        if (diffX < 0) {
			dir_x = 1.0f;
		} else if (diffX > 0) {
			dir_x = -1.0f;
		} else {
			dir_x = 0;
		}
		if (diffY < 0) {
			dir_y = 1.0f;
		} else if (diffY > 0) {
			dir_y = -1.0f;
		} else {
			dir_y = 0;
		}

        // Normalize movement to 1 for whichever is the larger gap, x or y.
        // Set the distance we move for the closer coordinate (x or y) as
        // to be an appropriate percent of the further coordinate.
        // So, for example, if the x coord is 10 away, and the y coord is 20 away,
        // we will move 0.5 in the x direction, and 1.0 in the y direction.
        diffX = Mathf.Abs(diffX);
        diffY = Mathf.Abs(diffY);
        if (diffX > diffY) {
            dir_y = dir_y * (diffY / diffX);
        } else {
            dir_x = dir_x * (diffX / diffY);
        }

        check_edge();

        // move this object
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// stay exactly at a given range from target
    // not yet implemented obviously
	void mvt_stayAt() {
		print("mvt_stayAt()");
	}
	
    // compute movement when unit dolesn't have a target
	void do_mv() {
		if (myStats.moveStrat == mv.random)
			mv_random();
		else
			mv_march();
	}
	
    // compute movement when unit has a target
	void do_mvt() {
		switch (myStats.moveStratTarget) {
		case mvt.stayWithin:
			mvt_stayWithin();
			break;
		case mvt.stayAt:
			mvt_stayAt();
			break;
		}
	}
	
	// Use this for initialization
	void Start () {
        // brown team starts at the top of the screen and moves down
		if (myStats.teamName == "brown") {
            dir_y = -1.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // we have a target, exeucte move-with-target strategy
		if (myStats.target != null) {
            do_mvt();
            return;
		}


        // move
        do_mv();
    }
}
