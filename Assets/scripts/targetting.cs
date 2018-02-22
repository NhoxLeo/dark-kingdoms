using UnityEngine;
using System.Collections;


public class targetting : MonoBehaviour {

    // dynamic visibility settings
    public const float addToVis = 1.0f;             // distance to expand visibility each time
    public const int visTargThreshold = 5;          // need less than this many targets to expand visibility

    // references to other scripts on this unit's gameObject
    public orc_stats myStats;
	
	public GameObject findNearestEnemy() {
        
		// myStats may not be initialized yet
		if (myStats == null)
			return null;
        
		if (myStats.enemies.Length == 0)
			return null;
        
		return findNearest(myStats.enemies);
	}
	
	GameObject findNearest(Collider2D [] targets) {
		int nearestIndex = 0;
		float nearestDistance = 0.0f, temp;
        Vector3 currentPos = transform.position;

        if (targets == null)
			return null;
        //Debug.Log(targets.Length);
		if (targets.Length == 1)
			return (targets[0].gameObject);
        
		nearestDistance = Vector2.Distance(currentPos, targets[0].transform.position);
		for (int i = 1; i < targets.Length; i++) {
            if (targets[i] == null)
                continue;

            //print("Me " + gameObject.GetInstanceID() + "   target " + targets[i].gameObject.GetInstanceID());
			if ((temp = Vector2.Distance(currentPos, targets[i].transform.position)) < nearestDistance) {
				nearestDistance = temp;
				nearestIndex = i;
			}
		}
		
		return (targets[nearestIndex].gameObject);
	}
	
	// Use this for initialization
	void Start () {
	}
}
