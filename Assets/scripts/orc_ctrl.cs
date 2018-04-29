using UnityEngine;
using System.Collections;

public enum mvt {stayWithin, stayAt};
public enum mv { march, random };
public enum mv_dir { n, ne, e, se, s, sw, w, nw };


// controls unit movement and sprite animation
public class orc_ctrl : MonoBehaviour {

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;

    public Animator animator;

    // general movement
    public float dir_x = 0;
	public float dir_y = 1.0f;
    public mv_dir prevDir = mv_dir.n;


    // bounds for legal playing area
	float min_x = 0.1f;
	float max_x = 15.7f;
	float min_y = 0.5f;
	float max_y = 23.5f;

    int strideMin = 50;     // makes random movement more natural by staying in a certain direction for a while
    int strideMax = 100;        
	public int courseStay = 0;     // prevent reversing dir after wall hit just reversed dir

    void checkForDirChange() {
        if (!animator) {
            return;
        }

        mv_dir currDir;
        bool north = false, south = false, east = false, west = false;

        // normalize to larger
        if (Mathf.Abs(dir_x) > Mathf.Abs(dir_y)) {
            // no divide by zero
            if (dir_x == 0.0f) {
                dir_x = 0.1f;
            }

            dir_x = dir_x / Mathf.Abs(dir_x);
            dir_y = dir_y / Mathf.Abs(dir_x);
        } else {
            // no divide by zero
            if (dir_y == 0.0f) {
                dir_y = 0.1f;
            }

            dir_x = dir_x / Mathf.Abs(dir_y);
            dir_y = dir_y / Mathf.Abs(dir_y);
        }

        if (dir_x > 0.5f) {
            east = true;
        } else if (dir_x < -0.5f) {
            west = true;
        }

        if (dir_y > 0.5f) {
            north = true;
        } else if (dir_y < -0.5f) {
            south = true;
        }

        if (east) {
            if (south) {
                currDir = mv_dir.se;
            } else if (north) {
                currDir = mv_dir.ne;
            } else {
                currDir = mv_dir.e;
            }
        } else if (west) {
            if (south) {
                currDir = mv_dir.sw;
            }
            else if (north) {
                currDir = mv_dir.nw;
            }
            else {
                currDir = mv_dir.w;
            }
        } else if (north) {
            currDir = mv_dir.n;
        } else {
            currDir = mv_dir.s;
        }

        if (currDir != prevDir) {
            prevDir = currDir;
            switch (currDir) {
                case mv_dir.n:
                    animator.SetTrigger("face n");
                    break;
                case mv_dir.ne:
                    animator.SetTrigger("face ne");
                    break;
                case mv_dir.e:
                    animator.SetTrigger("face e");
                    break;
                case mv_dir.se:
                    animator.SetTrigger("face se");
                    break;
                case mv_dir.s:
                    animator.SetTrigger("face s");
                    break;
                case mv_dir.sw:
                    animator.SetTrigger("face sw");
                    break;
                case mv_dir.w:
                    animator.SetTrigger("face w");
                    break;
                case mv_dir.nw:
                    animator.SetTrigger("face nw");
                    break;
            }
        }
    }

    // see if we have hit the edge of our legal playing area
    void check_edge() {
		if (transform.position.x < min_x) {
			dir_x = 1.0f;
			dir_y = 0;
			courseStay = strideMax;
		} else if (transform.position.x > max_x) {
			dir_x = -1.0f;
			dir_y = 0;
			courseStay = strideMax;
		}
		
		if (transform.position.y < min_y) {
			dir_x = 0;
			dir_y = 1.0f;
			courseStay = strideMax;
		} else if (transform.position.y > max_y) {
			dir_x = 0;
			dir_y = -1.0f;
			courseStay = strideMax;
		}
	}
	
	// move random, unless courseStay is set (e.g. we just hit a boundary)
	void mv_random() {
		// don't change direction until our stride is over
		if (courseStay > 0) {
			courseStay--;
		} else {
            // stride is over, pick new direction and stride
			dir_x = Random.Range(-1.0f, 1.0f);
			dir_y = Random.Range(-1.0f, 1.0f);

            courseStay = Random.Range(strideMin, strideMax);
		}
		
		check_edge();
        checkForDirChange();
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// march straight to other side of the game screen
	void mv_march() {
		// running into boundaries should set up march directions if only march is set
		check_edge();
        checkForDirChange();
		transform.Translate(Time.deltaTime * dir_x * myStats.speed, Time.deltaTime * dir_y * myStats.speed, 0);
	}
	
	// stay within a certain range of target
	void mvt_stayWithin() {
		if (Vector2.Distance(transform.position, myStats.target.transform.position) <= myStats.targetRange) {
            checkForDirChange();

            // we are as close to the target as we want to be
            if (animator) {
                animator.SetBool("walking", false);
            }
            
            return;
        }

        if (animator) {
            animator.SetBool("walking", true);
        }

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

        checkForDirChange();
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
	

    void Awake() {
        animator = GetComponent<Animator>();

        if ((animator != null) && (!orcTeam.animationOn)) {
            animator.enabled = false;
            animator = null;
        }
    }

	// Use this for initialization
	void Start () {
        // brown team starts at the top of the screen and moves down
        if (myStats.teamName == "brown") {
            dir_y = -1.0f;
            prevDir = mv_dir.s;

            if (animator) {
                animator.SetTrigger("face s");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        // we have a target, exeucte move-with-target strategy
		if (myStats.target != null) {
            courseStay = 0;
            do_mvt();
            return;
		}

        if (animator) {
            animator.SetBool("walking", true);
        }
        
        // move
        do_mv();
    }
}
