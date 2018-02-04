using UnityEngine;
using System.Collections;

public enum mvt {stayWithin, stayAt};

public struct mv {
	public float random;
	public float march;
	
	public mv(float random, float march) {
		this.random = random;
		this.march = march;
	}
};

public class orc_ctrl : MonoBehaviour {

	// script references
	orc_stats myStats = null;

	// general movement
	int dir_x = 0;
	int dir_y = 1;
	float min_x = 0.5f;
	float max_x = 15.5f;
	float min_y = 0.5f;
	float max_y = 11.5f;
	int stride = 30;
	int courseStay = 0;		// prevent reversing dir after wall hit just reversed dir
	
	void check_edge() {
		if (transform.position.x < min_x) {
			dir_x = 1;
			dir_y = 0;
			courseStay = stride;
		} else if (transform.position.x > max_x) {
			dir_x = -1;
			dir_y = 0;
			courseStay = stride;
		}
		
		if (transform.position.y < min_y) {
			dir_x = 0;
			dir_y = 1;
			courseStay = stride;
		} else if (transform.position.y > max_y) {
			dir_x = 0;
			dir_y = -1;
			courseStay = stride;
		}
	}
	
	// move random, unless courseStay is set (e.g. we just hit a boundary)
	void mv_random() {
		int chg_x, chg_y;

		// don't change direction if we just hit the wall
		if (courseStay > 0) {
			courseStay--;
		} else {
			chg_x = Random.Range(-1, stride);
			chg_y = Random.Range(-1, stride);

			if (chg_x < 2)
				dir_x = chg_x;
			if (chg_y < 2)
				dir_y = chg_y;;
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
        //print(myStats.teamName + myStats.enemyTeam);
        //print("ME" + transform.position + "THEM " + myStats.target.transform.position);
        //print(Vector3.Distance(transform.position, myStats.target.transform.position));

		if (Vector3.Distance(transform.position, myStats.target.transform.position) <= myStats.targetRange)
			return;

        if (transform.position.x < myStats.target.transform.position.x) {
			dir_x = 1;
		} else if (transform.position.x > myStats.target.transform.position.x) {
			dir_x = -1;
		} else {
			dir_x = 0;
		}
		
		if (transform.position.y < myStats.target.transform.position.y) {
			dir_y = 1;
		} else if (transform.position.y > myStats.target.transform.position.y) {
			dir_y = -1;
		} else {
			dir_y = 0;
		}
		
		check_edge();
        
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// stay exactly at a given range from target
	void mvt_stayAt() {
		print("mvt_stayAt()");
	}
	
	void do_mv() {
		float random = Random.Range(0.0f, myStats.moveStrat.random);
		float march = Random.Range(0.0f, myStats.moveStrat.march);
		float choice = Mathf.Max(random, march);
		
		if (choice == random)
			mv_random();
		else
			mv_march();
	}
	
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
		myStats = GetComponent<orc_stats>();
		
		if (myStats.teamName == "brown")
			dir_y = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (myStats.target != null) {
			do_mvt();
			return;
		}
		do_mv();
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		print("COLLISION");
	}
}
