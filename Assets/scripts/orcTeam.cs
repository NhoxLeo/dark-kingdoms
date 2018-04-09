using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class orcTeam : MonoBehaviour {

	public Slider numOrcs;
	public Text numOrcsText;
	public Slider numArchers;
	public Text numArchersText;
	
    // get number of melee orcs from melee orc slider
	public int get_numOrcs() {
        
        return ((int)numOrcs.value);
	}
	
    // update Text for number of orcs and calculater possible archers
	public void set_numOrcsText() {
		int numOrcs = get_numOrcs();
        
		numOrcsText.text = numOrcs.ToString();
		numArchers.maxValue = armySpawner.MAX_UNITS - numOrcs;
	}

    // get number of archers from archer slider
    public int get_numArchers() {
        
        return ((int)numArchers.value);
	}

    // update Text for number of archers and calculater possible orcs
    public void set_numArchersText() {
		int numArchers = get_numArchers();
        
        numArchersText.text = numArchers.ToString();
		numOrcs.maxValue = armySpawner.MAX_UNITS - numArchers;
	}
	
    // set initial values for unit select sliders
    // currently 75% orcs, 25% archers
    void initSliders() {
        numOrcs.maxValue = armySpawner.MAX_UNITS * 0.75f;
        numOrcs.value = numOrcs.maxValue;

        numArchers.maxValue = armySpawner.MAX_UNITS * 0.25f;
        numArchers.value = numArchers.maxValue;
    }

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
        initSliders();
	}
}
