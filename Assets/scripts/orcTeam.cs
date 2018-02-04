using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class orcTeam : MonoBehaviour {

	public Slider numOrcs;
	public Text numOrcsText;
	public Slider numArchers;
	public Text numArchersText;
	public int numAlive = 0;
	
	public int get_numOrcs() {
		return ((int)numOrcs.value);
	}
	
	public void set_numOrcsText() {
		int numOrcs = get_numOrcs();
	
		numOrcsText.text = numOrcs.ToString();
		numArchers.maxValue = armySpawner.MAX_UNITS - numOrcs;
	}
	
	public int get_numArchers() {
		return ((int)numArchers.value);
	}
	
	public void set_numArchersText() {
		int numArchers = get_numArchers();
		
		numArchersText.text = numArchers.ToString();
		numOrcs.maxValue = armySpawner.MAX_UNITS - numArchers;
	}
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		set_numOrcsText();
	}
}
