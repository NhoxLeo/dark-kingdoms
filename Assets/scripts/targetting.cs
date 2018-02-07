using UnityEngine;
using System.Collections;

public class range {
	public const float melee = 0.3f;
	public const float ranged = 2.0f;
}

public class targetting : MonoBehaviour {

	// script references
	orc_stats myStats = null;
	
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
        
		if (targets == null)
			return null;
        
		if (targets.Length == 1)
			return (targets[0].gameObject);
        
		nearestDistance = Vector3.Distance(transform.position, targets[0].transform.position);
		for (int i = 1; i < targets.Length; i++) {
            if (targets[i] == null)
                continue;

            //print("Me " + gameObject.GetInstanceID() + "   target " + targets[i].gameObject.GetInstanceID());
			if ((temp = Vector3.Distance(transform.position, targets[i].transform.position)) < nearestDistance) {
				nearestDistance = temp;
				nearestIndex = i;
			}
		}
		
		return (targets[nearestIndex].gameObject);
	}
	
	// Use this for initialization
	void Start () {
		myStats = GetComponent<orc_stats>();
	}
}
